using Keyboard_Typing_System;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Keyboard_Typing_Design
{
    public partial class fmKeyboardTyping : Form
    {
        public fmKeyboardTyping()
        {
            InitializeComponent();
            CustomWindow(Color.FromArgb(68, 30, 89), Color.White, Color.FromArgb(68, 30, 89), Handle);
            Reset();
        }

        private string ToBgr(Color c) => $"{c.B:X2}{c.G:X2}{c.R:X2}";

        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        const int DWWMA_CAPTION_COLOR = 35;
        const int DWWMA_BORDER_COLOR = 34;
        const int DWMWA_TEXT_COLOR = 36;
        public void CustomWindow(Color captionColor, Color fontColor, Color borderColor, IntPtr handle)
        {
            IntPtr hWnd = handle;
            //Change caption color
            int[] caption = new int[] { int.Parse(ToBgr(captionColor), System.Globalization.NumberStyles.HexNumber) };
            DwmSetWindowAttribute(hWnd, DWWMA_CAPTION_COLOR, caption, 4);
            //Change font color
            int[] font = new int[] { int.Parse(ToBgr(fontColor), System.Globalization.NumberStyles.HexNumber) };
            DwmSetWindowAttribute(hWnd, DWMWA_TEXT_COLOR, font, 4);
            //Change border color
            int[] border = new int[] { int.Parse(ToBgr(borderColor), System.Globalization.NumberStyles.HexNumber) };
            DwmSetWindowAttribute(hWnd, DWWMA_BORDER_COLOR, border, 4);

        }



        void Reset()
        {
            tbOrginalText.Clear();
            tbTypeText.Clear();
            btnTraining.Checked = false;
            lbTitle.Visible = false;
            lbTime.Visible = false;
            TestTimer.Stop();
            tbTypeText.BackColor= Color.White;
            pnKeyboard.Visible = false;
            gbTypeHere.Visible = false;
            btnSubmit.Enabled = false;
            btnTest.Checked = false;
            ResetColor();
            clsSystem.Reset();

        }

        private void btn_Click(object sender, EventArgs e)
        {
            

            if (sender == btnTest)
            {
                Reset();
                tbOrginalText.Text = clsSystem.GetText(clsSystem.enTextType.eTest);
                tbTypeText.MaxLength = tbOrginalText.Text.Trim().Length;
                btnTest.Checked = true;
                lbTitle.Visible = true;
                lbTime.Visible = true;
                pnKeyboard.Visible = true;
                gbTypeHere.Visible = true;
                TestTimer.Start();
                tbTypeText.Focus();
                return;
            }

            if (sender == btnTraining)
            {
                Reset();
                tbOrginalText.Text = clsSystem.GetText(clsSystem.enTextType.eTraining);
                tbTypeText.MaxLength = tbOrginalText.Text.Trim().Length;
                btnTraining.Checked = true;
                pnKeyboard.Visible = true;
                gbTypeHere.Visible = true;
                tbTypeText.Focus();
                return;
            }

            if (sender == btnReset)
            {
                Reset();
                return;
            }

            if (sender == btnSubmit)
            {
                fmResultsScreen Results = new fmResultsScreen();
                Results.ShowDialog();
                return;
            }
        }

        void HighLightKeyUsed(char Key)
        {
            if (Char.IsControl(Key))
            {
                lbbackspace.BackColor = Color.Aqua;
            }

            foreach (var Control in pnKeyboard.Controls)
            {
                if (Control is Label)
                {
                    Label lb = (Label)Control;

                    if (lb.Tag != null && Key.ToString().ToUpper() == lb.Tag.ToString())
                    {
                        lb.BackColor = Color.Aqua;
                        return;
                    }
                }
            }
        }

        void ResetColor()
        {
            foreach (var Control in pnKeyboard.Controls)
            {
                if (Control is Label)
                {
                    Label lb = (Label)Control;

                    if (lb.BackColor != Color.White)
                    {
                        lb.BackColor = Color.White;
                        return;
                    }
                }
            }
        }

        private void tbTypeText_KeyDown(object sender, KeyEventArgs e)
        {
            
            ResetColor();
        }

        private void tbTypeText_KeyPress(object sender, KeyPressEventArgs e)
        {
            HighLightKeyUsed(e.KeyChar);

            if (btnTraining.Checked)
            {
                

                int Counter = tbTypeText.TextLength;
                char CurrentCharacter = tbOrginalText.Text[Counter];
                

                if (tbTypeText.TextLength == 0 && e.KeyChar != CurrentCharacter)
                {
                    tbTypeText.BackColor = Color.Red;
                    return;
                }

                if (tbTypeText.Text == string.Empty)
                {
                    tbTypeText.BackColor = Color.White;
                    return;

                }


                if (e.KeyChar == (char)8)
                {
                    if (tbTypeText.Text.Substring(0, tbTypeText.Text.Length - 1) == tbOrginalText.Text.Substring(0, tbTypeText.Text.Length - 1))
                    {
                        tbTypeText.BackColor = Color.White;
                        return;
                    }
                }

                if (e.KeyChar != CurrentCharacter)
                {
                    tbTypeText.BackColor = Color.Red;
                    return;
                }

            }
            
            

        }

        private void tbTypeText_TextChanged(object sender, EventArgs e)
        {
            

            if (tbTypeText.Text == tbOrginalText.Text.Trim() && tbTypeText.Text != string.Empty)
            {
                if (btnTest.Checked)
                {
                    clsSystem.TestEnds(tbTypeText.Text.Trim());
                    btnSubmit.Enabled = true;
                }
                  
                MessageBox.Show("Well done" , "Sucess" , MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
        }

        private void TestTimer_Tick(object sender, EventArgs e)
        {
            clsSystem.CurrentTime--;

            lbTime.Text = clsSystem.CurrentTime.ToString();

            if (clsSystem.CurrentTime < 1)
            {
                TestTimer.Stop();
                MessageBox.Show("Test Ends" , "Time Ends" , MessageBoxButtons.OK, MessageBoxIcon.Information);
                clsSystem.TestEnds(tbTypeText.Text.Trim());
                btnSubmit.Enabled = true;
            }
        }
    }
}