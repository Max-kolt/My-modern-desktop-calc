using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModernCalcApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximumSize = new Size(350, 447);
        }

        string oper;
        int countNum = 0, countOper = 0;

        private void Operation_Click(object sender, EventArgs e)
        {
            if (countNum != 0 && countOper == 0 && (sender as Button).Text != "=")
            {
                x = Convert.ToDouble(OutputBox.Text);
                oper = (sender as Button).Text;
                OutputBox.Text = oper;
                countNum = 0;
                countOper++;
            }
            if (countOper > 0 && countNum != 0)
            {
                y = Convert.ToDouble(OutputBox.Text);
                if (y == 0) { MessageBox.Show("Делить на ноль Нельзя!!!"); return; }
                switch (oper)
                {
                    case "/": x = x / y; break;
                    case "*": x = x * y; break;
                    case "+": x = x + y; break;
                    case "-": x = x - y; break;
                    case "mod": x = x % y; break;
                    default: return;
                }

                oper = (sender as Button).Text;
                if (oper == "=") { OutputBox.Text = x.ToString(); countOper = 0; }
                else { OutputBox.Text = oper; countNum = 0; }
            }
        }

        string writedown;
        double x = 0, y = 0;
        bool otric = false, zap = true;

        private void btnPlusMinus_Click(object sender, EventArgs e)
        {
            if (countNum > 0)
            {
                OutputBox.Text = Convert.ToString(Convert.ToDouble(OutputBox.Text)*-1);
            }
        }

        private void MemoryBtn_Click(object sender, EventArgs e)
        {
            Button btnClick = (Button)sender;
            if (btnClick.Tag.ToString() == "read") MemBox.Text = OutputBox.Text;
            else if (btnClick.Tag.ToString() == "clear") MemBox.Text = "";
            else if (btnClick.Tag.ToString() == "send" && MemBox.Text.Length > 0) { OutputBox.Text = MemBox.Text; countNum++; }
            else if (btnClick.Tag.ToString() == "plus" && MemBox.Text.Length > 0) MemBox.Text = Convert.ToString(Convert.ToDouble(OutputBox.Text) + Convert.ToDouble(MemBox.Text));
            else if (btnClick.Tag.ToString() == "minus" && MemBox.Text.Length > 0) MemBox.Text = Convert.ToString(Convert.ToDouble(MemBox.Text) - Convert.ToDouble(OutputBox.Text));
        }

        private void checkModeEng_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkModeEng.Checked)
            {
                this.Size = new Size(506, 447);
                this.MaximumSize = new Size(506, 447);
                this.MinimumSize = new Size(506, 447);
            }
            else
            {
                this.Size = new Size(350, 447);
                this.MaximumSize = new Size(350, 447);
                this.MinimumSize = new Size(350, 447);
            }
        }

        private void Eng1Oper_Click(object sender, EventArgs e)
        {
            Button btnSend = (Button) sender;
            switch (btnSend.Text)
            {
                case "√":
                    if (Convert.ToDouble(this.OutputBox.Text) >= 0) this.OutputBox.Text = Convert.ToString(Math.Sqrt(Convert.ToDouble(this.OutputBox.Text)));
                    break;

                case "x^2":
                    this.OutputBox.Text = Convert.ToString(Math.Pow(Convert.ToDouble(this.OutputBox.Text), 2));
                    break;

                case "lg":
                    if (Convert.ToDouble(this.OutputBox.Text) > 0) this.OutputBox.Text = Convert.ToString(Math.Log10(Convert.ToDouble(this.OutputBox.Text)));
                    break;

                case "ln":
                    if (Convert.ToDouble(this.OutputBox.Text) > 0) this.OutputBox.Text = Convert.ToString(Math.Log(Convert.ToDouble(this.OutputBox.Text)));
                    break;

                case "cos":
                    if (Convert.ToDouble(this.OutputBox.Text) <= 1 && Convert.ToDouble(this.OutputBox.Text) >= -1) this.OutputBox.Text = Convert.ToString(Math.Cos(Convert.ToDouble(this.OutputBox.Text)));
                    break;
            }
           
        }

        private void btnInsertPi_Click(object sender, EventArgs e)
        {
            string info = (sender as Button).Text;

            if (info == "Pi") this.OutputBox.Text = Math.PI.ToString();
            else if (info == "e") this.OutputBox.Text = Math.E.ToString();
            countNum++;

        }

        private void btnDock_Click(object sender, EventArgs e)
        {
            zap = true;
            foreach (char i in OutputBox.Text)
            {
                if (i == ',') zap = false;
            }
            if (zap && countNum != 0)
            {
                OutputBox.Text = OutputBox.Text + ",";
                countNum += 1;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if ((sender as Button) == btnClearAll)
            {
                countNum = 0;
                countOper = 0;
                OutputBox.Text = "0";
                x = 0;
                y = 0;
            }
            else if((sender as Button) == btnClearCh && countNum != 0)
            {
                countNum--;
                OutputBox.Text = OutputBox.Text.Substring(0, OutputBox.Text.Length-1);
                if (countNum == 0) OutputBox.Text = "0";
            }
        }

        private void Operation_NumberInput(object sender, EventArgs e)
        {
            writedown = (sender as Button).Text;
            if (countNum == 0)
            {
                if (oper == "=") oper = ""; 
                OutputBox.Text = "";
                
            }
            OutputBox.Text += writedown;
            countNum++;
        }
    }
}
