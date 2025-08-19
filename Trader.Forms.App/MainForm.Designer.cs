
namespace Trader.Forms.App
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageKucoin = new System.Windows.Forms.TabPage();
            this.buttonKClear = new System.Windows.Forms.Button();
            this.richTextBoxKLog = new System.Windows.Forms.RichTextBox();
            this.buttonKStart = new System.Windows.Forms.Button();
            this.textBoxKAmount = new System.Windows.Forms.TextBox();
            this.textBoxKSymbol = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPageGate = new System.Windows.Forms.TabPage();
            this.buttonGateClear = new System.Windows.Forms.Button();
            this.labelGateTokenQty = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBoxGateLog = new System.Windows.Forms.RichTextBox();
            this.buttonGateStart = new System.Windows.Forms.Button();
            this.textBoxGateLimitPrice = new System.Windows.Forms.TextBox();
            this.textBoxGateAmount = new System.Windows.Forms.TextBox();
            this.textBoxGateSymbol = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxKBuyPrice = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxKMaxBuyPrice = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelKQty = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPageKucoin.SuspendLayout();
            this.tabPageGate.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageKucoin);
            this.tabControl1.Controls.Add(this.tabPageGate);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(913, 668);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageKucoin
            // 
            this.tabPageKucoin.Controls.Add(this.labelKQty);
            this.tabPageKucoin.Controls.Add(this.label9);
            this.tabPageKucoin.Controls.Add(this.textBoxKMaxBuyPrice);
            this.tabPageKucoin.Controls.Add(this.label8);
            this.tabPageKucoin.Controls.Add(this.textBoxKBuyPrice);
            this.tabPageKucoin.Controls.Add(this.label7);
            this.tabPageKucoin.Controls.Add(this.buttonKClear);
            this.tabPageKucoin.Controls.Add(this.richTextBoxKLog);
            this.tabPageKucoin.Controls.Add(this.buttonKStart);
            this.tabPageKucoin.Controls.Add(this.textBoxKAmount);
            this.tabPageKucoin.Controls.Add(this.textBoxKSymbol);
            this.tabPageKucoin.Controls.Add(this.label5);
            this.tabPageKucoin.Controls.Add(this.label6);
            this.tabPageKucoin.Location = new System.Drawing.Point(4, 29);
            this.tabPageKucoin.Name = "tabPageKucoin";
            this.tabPageKucoin.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageKucoin.Size = new System.Drawing.Size(905, 635);
            this.tabPageKucoin.TabIndex = 1;
            this.tabPageKucoin.Text = "Kucoin";
            this.tabPageKucoin.UseVisualStyleBackColor = true;
            // 
            // buttonKClear
            // 
            this.buttonKClear.Location = new System.Drawing.Point(772, 66);
            this.buttonKClear.Name = "buttonKClear";
            this.buttonKClear.Size = new System.Drawing.Size(94, 29);
            this.buttonKClear.TabIndex = 17;
            this.buttonKClear.Text = "Clear";
            this.buttonKClear.UseVisualStyleBackColor = true;
            this.buttonKClear.Click += new System.EventHandler(this.buttonKClear_Click);
            // 
            // richTextBoxKLog
            // 
            this.richTextBoxKLog.Location = new System.Drawing.Point(10, 114);
            this.richTextBoxKLog.Name = "richTextBoxKLog";
            this.richTextBoxKLog.Size = new System.Drawing.Size(883, 500);
            this.richTextBoxKLog.TabIndex = 16;
            this.richTextBoxKLog.Text = "";
            // 
            // buttonKStart
            // 
            this.buttonKStart.Location = new System.Drawing.Point(772, 20);
            this.buttonKStart.Name = "buttonKStart";
            this.buttonKStart.Size = new System.Drawing.Size(94, 29);
            this.buttonKStart.TabIndex = 15;
            this.buttonKStart.Text = "Start";
            this.buttonKStart.UseVisualStyleBackColor = true;
            this.buttonKStart.Click += new System.EventHandler(this.buttonKStart_Click);
            // 
            // textBoxKAmount
            // 
            this.textBoxKAmount.Location = new System.Drawing.Point(291, 20);
            this.textBoxKAmount.Name = "textBoxKAmount";
            this.textBoxKAmount.Size = new System.Drawing.Size(95, 27);
            this.textBoxKAmount.TabIndex = 14;
            this.textBoxKAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxKBuyPrice_KeyUp);
            // 
            // textBoxKSymbol
            // 
            this.textBoxKSymbol.Location = new System.Drawing.Point(78, 21);
            this.textBoxKSymbol.Name = "textBoxKSymbol";
            this.textBoxKSymbol.Size = new System.Drawing.Size(90, 27);
            this.textBoxKSymbol.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(174, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Amount(USDT):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Symbol:";
            // 
            // tabPageGate
            // 
            this.tabPageGate.Controls.Add(this.buttonGateClear);
            this.tabPageGate.Controls.Add(this.labelGateTokenQty);
            this.tabPageGate.Controls.Add(this.label4);
            this.tabPageGate.Controls.Add(this.richTextBoxGateLog);
            this.tabPageGate.Controls.Add(this.buttonGateStart);
            this.tabPageGate.Controls.Add(this.textBoxGateLimitPrice);
            this.tabPageGate.Controls.Add(this.textBoxGateAmount);
            this.tabPageGate.Controls.Add(this.textBoxGateSymbol);
            this.tabPageGate.Controls.Add(this.label3);
            this.tabPageGate.Controls.Add(this.label2);
            this.tabPageGate.Controls.Add(this.label1);
            this.tabPageGate.Location = new System.Drawing.Point(4, 29);
            this.tabPageGate.Name = "tabPageGate";
            this.tabPageGate.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGate.Size = new System.Drawing.Size(905, 635);
            this.tabPageGate.TabIndex = 0;
            this.tabPageGate.Text = "Gate.io";
            this.tabPageGate.UseVisualStyleBackColor = true;
            // 
            // buttonGateClear
            // 
            this.buttonGateClear.Location = new System.Drawing.Point(767, 66);
            this.buttonGateClear.Name = "buttonGateClear";
            this.buttonGateClear.Size = new System.Drawing.Size(94, 29);
            this.buttonGateClear.TabIndex = 10;
            this.buttonGateClear.Text = "Clear";
            this.buttonGateClear.UseVisualStyleBackColor = true;
            this.buttonGateClear.Click += new System.EventHandler(this.buttonGateClear_Click);
            // 
            // labelGateTokenQty
            // 
            this.labelGateTokenQty.AutoSize = true;
            this.labelGateTokenQty.Location = new System.Drawing.Point(379, 70);
            this.labelGateTokenQty.Name = "labelGateTokenQty";
            this.labelGateTokenQty.Size = new System.Drawing.Size(0, 20);
            this.labelGateTokenQty.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(262, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Qty of token:";
            // 
            // richTextBoxGateLog
            // 
            this.richTextBoxGateLog.Location = new System.Drawing.Point(27, 114);
            this.richTextBoxGateLog.Name = "richTextBoxGateLog";
            this.richTextBoxGateLog.Size = new System.Drawing.Size(849, 500);
            this.richTextBoxGateLog.TabIndex = 7;
            this.richTextBoxGateLog.Text = "";
            // 
            // buttonGateStart
            // 
            this.buttonGateStart.Location = new System.Drawing.Point(767, 20);
            this.buttonGateStart.Name = "buttonGateStart";
            this.buttonGateStart.Size = new System.Drawing.Size(94, 29);
            this.buttonGateStart.TabIndex = 6;
            this.buttonGateStart.Text = "Start";
            this.buttonGateStart.UseVisualStyleBackColor = true;
            this.buttonGateStart.Click += new System.EventHandler(this.buttonGateStart_Click);
            // 
            // textBoxGateLimitPrice
            // 
            this.textBoxGateLimitPrice.Location = new System.Drawing.Point(595, 21);
            this.textBoxGateLimitPrice.Name = "textBoxGateLimitPrice";
            this.textBoxGateLimitPrice.Size = new System.Drawing.Size(108, 27);
            this.textBoxGateLimitPrice.TabIndex = 5;
            this.textBoxGateLimitPrice.TextChanged += new System.EventHandler(this.textBoxGateAmount_TextChanged);
            // 
            // textBoxGateAmount
            // 
            this.textBoxGateAmount.Location = new System.Drawing.Point(379, 21);
            this.textBoxGateAmount.Name = "textBoxGateAmount";
            this.textBoxGateAmount.Size = new System.Drawing.Size(95, 27);
            this.textBoxGateAmount.TabIndex = 4;
            this.textBoxGateAmount.TextChanged += new System.EventHandler(this.textBoxGateAmount_TextChanged);
            // 
            // textBoxGateSymbol
            // 
            this.textBoxGateSymbol.Location = new System.Drawing.Point(86, 21);
            this.textBoxGateSymbol.Name = "textBoxGateSymbol";
            this.textBoxGateSymbol.Size = new System.Drawing.Size(149, 27);
            this.textBoxGateSymbol.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(495, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Limit Price:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(262, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Amount(USDT):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Symbol:";
            // 
            // textBoxKBuyPrice
            // 
            this.textBoxKBuyPrice.Location = new System.Drawing.Point(470, 21);
            this.textBoxKBuyPrice.Name = "textBoxKBuyPrice";
            this.textBoxKBuyPrice.Size = new System.Drawing.Size(95, 27);
            this.textBoxKBuyPrice.TabIndex = 19;
            this.textBoxKBuyPrice.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxKBuyPrice_KeyUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(392, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 20);
            this.label7.TabIndex = 18;
            this.label7.Text = "Buy Price:";
            // 
            // textBoxKMaxBuyPrice
            // 
            this.textBoxKMaxBuyPrice.Location = new System.Drawing.Point(671, 24);
            this.textBoxKMaxBuyPrice.Name = "textBoxKMaxBuyPrice";
            this.textBoxKMaxBuyPrice.Size = new System.Drawing.Size(95, 27);
            this.textBoxKMaxBuyPrice.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(572, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 20);
            this.label8.TabIndex = 20;
            this.label8.Text = "Max Buy Price:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(370, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 20);
            this.label9.TabIndex = 9;
            this.label9.Text = "Qty of token:";
            // 
            // labelKQty
            // 
            this.labelKQty.AutoSize = true;
            this.labelKQty.Location = new System.Drawing.Point(471, 75);
            this.labelKQty.Name = "labelKQty";
            this.labelKQty.Size = new System.Drawing.Size(15, 20);
            this.labelKQty.TabIndex = 22;
            this.labelKQty.Text = "-";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 668);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trader";
            this.tabControl1.ResumeLayout(false);
            this.tabPageKucoin.ResumeLayout(false);
            this.tabPageKucoin.PerformLayout();
            this.tabPageGate.ResumeLayout(false);
            this.tabPageGate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGate;
        private System.Windows.Forms.TabPage tabPageKucoin;
        private System.Windows.Forms.TextBox textBoxGateLimitPrice;
        private System.Windows.Forms.TextBox textBoxGateAmount;
        private System.Windows.Forms.TextBox textBoxGateSymbol;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBoxGateLog;
        private System.Windows.Forms.Button buttonGateStart;
        private System.Windows.Forms.Label labelGateTokenQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonGateClear;
        private System.Windows.Forms.Button buttonKClear;
        private System.Windows.Forms.RichTextBox richTextBoxKLog;
        private System.Windows.Forms.Button buttonKStart;
        private System.Windows.Forms.TextBox textBoxKAmount;
        private System.Windows.Forms.TextBox textBoxKSymbol;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxKMaxBuyPrice;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxKBuyPrice;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelKQty;
        private System.Windows.Forms.Label label9;
    }
}

