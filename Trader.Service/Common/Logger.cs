using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Trader.Service.Common
{
    public class Logger : ITradeLogger
    {
        private readonly ConcurrentQueue<string> _queue = new ConcurrentQueue<string>();
        private readonly List<string> _log = new List<string>();
        private bool _stop;
        private readonly string _fileName;
        private readonly Action<string> _messageAction;

        public Logger(string fileName, bool overwrite, Action<string> messageAction = null)
        {
            _fileName = fileName;
            _messageAction = messageAction;
            if (overwrite)
            {
                Clear();
            }

            Task.Run(() => Run());
        }

        public void Log(string message)
        {
            message = $"{DateTime.Now:dd/MM/yyyy hh:mm:ss.fff} : {message}{Environment.NewLine}";
            _queue.Enqueue(message);
        }

        public void Clear()
        {
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
        }

        public void Stop()
        {
            _stop = true;
            File.AppendAllLines(_fileName, _log);
        }

        private void Run()
        {
            while (!_stop)
            {
                if (_queue.TryDequeue(out var result))
                {
                    if(_messageAction != null)
                    {
                        _messageAction.Invoke(result);
                    }

                    File.AppendAllText(_fileName, result);
                }

                Thread.Sleep(1);  
            }
        }
    }
}
