using Keyboard_Typing_Design.Properties;
using Keyboard_Typing_System;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Keyboard_Typing_Design
{
    public partial class fmResultsScreen : Form
    {
        public fmResultsScreen()
        {
            InitializeComponent();
            CustomWindow(Color.FromArgb(68, 30, 89), Color.White, Color.FromArgb(68, 30, 89), Handle);
            SetResults();
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


        void SetResults()
        {
            int Words = clsSystem.Success;

            lbAll.Text = clsSystem.All.ToString();
            lbCorrect.Text = clsSystem.Success.ToString();
            lbWrong.Text = clsSystem.Failed.ToString();

            lbPercentageCorrect.Text = clsSystem.GetAccuracy().ToString("00.00") + " %";
            lbPercentageWrong.Text = clsSystem.GetWrong().ToString("00.00") + " %";

            cpbCorrect.Value = (int)clsSystem.GetAccuracy();
            cpbWrong.Value = (int)clsSystem.GetWrong();

            lbWordsPerMinute.Text = clsSystem.Success.ToString() + " WPM";
            lbAccuracy.Text = clsSystem.GetAccuracy().ToString("00.00") + " %";

            
            if (Words < 5)
            {
                lbStatus.Text = clsSystem.Status[0];
                pbStatus.Image = Image.FromFile("Status/" + clsSystem.Status[0] + ".png");

                return;
            }

            pb1.Image = Resources.star__1_;

            if (Words < 10)
            {
                lbStatus.Text = clsSystem.Status[1];
                pbStatus.Image = Image.FromFile("Status/" + clsSystem.Status[1] + ".png");
                return;
            }

            pb2.Image = Resources.star__1_;

            if (Words < 30)
            {
                lbStatus.Text = clsSystem.Status[2];
                pbStatus.Image = Image.FromFile("Status/" + clsSystem.Status[2] + ".png");
                return;
            }

            pb3.Image = Resources.star__1_;

            if (Words < 50)
            {
                lbStatus.Text = clsSystem.Status[3];
                pbStatus.Image = Image.FromFile("Status/" + clsSystem.Status[3] + ".png");
                return;
            }

            pb4.Image = Resources.star__1_;

            if (Words < 60)
            {
                lbStatus.Text = clsSystem.Status[4];
                pbStatus.Image = Image.FromFile("Status/" + clsSystem.Status[4] + ".png");
                return;
            }

            pb5.Image = Resources.star__1_;

            lbStatus.Text = clsSystem.Status[5];
            pbStatus.Image = Image.FromFile("Status/" + clsSystem.Status[5] + ".png");


        }

       
    }
}
