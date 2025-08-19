using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trader.Service.Common;
using Trader.Service.Gate;
using Trader.Service.Kucoin;

namespace Trader.Forms.App
{
    public partial class MainForm : Form
    {
        private GateService _gateService;
        private const string _gateLogName = "log_gate.txt";
        private delegate void SafeCallDelegate(RichTextBox textBox, string text);

        private KucoinService _kucoinService;
        private const string _kucoinLogName = "log_kucoin.txt";

        public MainForm()
        {
            InitializeComponent();
        }

        private async void buttonGateStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (_gateService?.IsRunning == true)
                {
                    //buttonGateStart.Text = "Start";
                    _gateService.Stop();
                }
                else
                {
                    buttonGateStart.Text = "Stop";
                    var symbol = textBoxGateSymbol.Text.Trim();
                    var price = decimal.Parse(textBoxGateLimitPrice.Text);
                    var amount = decimal.Parse(textBoxGateAmount.Text);
                    
                    if (_gateService == null)
                    {
                        var logger = new Logger(_gateLogName, true, (x) =>
                        {
                            WriteTextSafe(richTextBoxGateLog, x);
                        });

                        _gateService = new GateService(Configurations.Instance.GateSettings, logger);
                    }

                    _ = Task.Run(async () =>
                      {
                          await _gateService.GrabListing(symbol, price, Math.Round(amount / price, 4));
                          buttonGateStart.Invoke((MethodInvoker)delegate
                          {
                              buttonGateStart.Text = "Start";
                          });
                      });
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message, Text);
            }
        }

        private void WriteTextSafe(RichTextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
            {
                
                var d = new SafeCallDelegate(WriteTextSafe);
                textBox.Invoke(d, new object[] { textBox, text });
            }
            else
            {
                textBox.AppendText(text);
                textBox.ScrollToCaret();
            }
        }

        private void textBoxGateAmount_TextChanged(object sender, EventArgs e)
        {
            SetQtyLabel(labelGateTokenQty, textBoxGateLimitPrice, textBoxGateAmount);
        }

        private void SetQtyLabel(Label label, TextBox priceTextBox, TextBox amountTextBox)
        {
            label.Text = string.Empty;
            try
            {
                var price = decimal.Parse(priceTextBox.Text);
                var amount = decimal.Parse(amountTextBox.Text);
                labelGateTokenQty.Text = Math.Round(amount / price, 4).ToString();
            }
            catch(Exception)
            {
            }
        }

        private void buttonGateClear_Click(object sender, EventArgs e)
        {
            richTextBoxGateLog.Clear();
        }

        private async void buttonKStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (_kucoinService?.IsRunning == true)
                {
                    _kucoinService.Stop();
                }
                else
                {
                    buttonKStart.Text = "Stop";
                    var symbol = textBoxKSymbol.Text.Trim();
                    var amount = decimal.Parse(textBoxKAmount.Text);
                    var maxBuyPrice = decimal.Parse(textBoxKMaxBuyPrice.Text);
                    var buyPrice = decimal.Parse(textBoxKBuyPrice.Text);

                    if (_kucoinService == null)
                    {
                        var logger = new Logger(_kucoinLogName, true, (x) =>
                        {
                            WriteTextSafe(richTextBoxKLog, x);
                        });

                        _kucoinService = new KucoinService(Configurations.Instance.KucoinSettings, logger);
                    }

                    _ = Task.Run(async () =>
                    {
                        await _kucoinService.GrabListing(new KucoinOrderInfo
                        {
                            Symbol = symbol,
                            BuyAmount = amount,
                            LastPrice = buyPrice,
                            MaxBuyPrice = maxBuyPrice,
                        });
                        buttonKStart.Invoke((MethodInvoker)delegate
                        {
                            buttonKStart.Text = "Start";
                        });
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Text);
            }
        }

        private void buttonKClear_Click(object sender, EventArgs e)
        {
            richTextBoxKLog.Clear();
        }

        private void textBoxKBuyPrice_KeyUp(object sender, KeyEventArgs e)
        {
            SetQtyLabel(labelGateTokenQty, textBoxKBuyPrice, textBoxKAmount);
        }
    }
}
