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
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

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
        public Form1()
        {
            InitializeComponent();

            Form1Resources.AppendFont("Robotto", Properties.Resources.RobotoRegular);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //text.Font = Properties.Resources.GetFont(Resources.FontResources.Roboto_Regular);
            Font = Form1Resources.NewFont("Robotto", Font.Size);
            //form1_Menu_OpenMenuItem.Font = myFont;
            //form1_Menu_OpenMenuItem.Font = myFont;

        }
    }
}
