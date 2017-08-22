using System;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AIE_CS
{
    class Form1Resources
    {
        private static PrivateFontCollection m_pc_fonts = new PrivateFontCollection();
        private static Dictionary<string, int> m_index_fonts = new Dictionary<string, int>();

        public static void AppendFont(string name, byte[] data)
        {
            int length = data.Length;

            System.IntPtr intPtr = Marshal.AllocCoTaskMem(length);

            Marshal.Copy(data, 0, intPtr, length);

            m_pc_fonts.AddMemoryFont(intPtr, length);

            m_index_fonts.Add(name, m_pc_fonts.Families.Length - 1);
        }

        public static Font NewFont(string name, float size)
        {
            return new Font(m_pc_fonts.Families[m_index_fonts[name]], size);
        }
    }
}
