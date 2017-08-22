using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

//https://www.buildbox.com/
//https://material.io/icons/#ic_clear
//https://www.youtube.com/watch?v=YZOiZ-gcdis
//https://www.youtube.com/watch?v=b_J1bqSneMA
//http://geekswithblogs.net/kobush/articles/CustomBorderForms.aspx
//http://geekswithblogs.net/kobush/articles/CustomBorderForms3.aspx


namespace AIE_CS
{
    public partial class Form1 : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private static Color prevColor;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form1Resources.AppendFont("Robotto", Properties.Resources.RobotoRegular);
            //text.Font = Properties.Resources.GetFont(Resources.FontResources.Roboto_Regular);
            Font = Form1Resources.NewFont("Robotto", Font.Size);
            //form1_Menu_OpenMenuItem.Font = myFont;
            //form1_Menu_OpenMenuItem.Font = myFont;
            prevColor = form1_AppClose.BackColor;

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void form1_AppTitleName_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void form1_AppTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void form1_AppClose_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Close();
            }
        }

        private void form1_AppClose_MouseEnter(object sender, EventArgs e)
        {
            form1_AppClose.BackColor = Color.FromArgb(255, 128, 128);
        }

        private void form1_AppClose_MouseLeave(object sender, EventArgs e)
        {
            form1_AppClose.BackColor = prevColor;
        }
    }
}
