using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIECS
{
    public partial class Form1 : Form
    {
        // variables
        float m_viewAngle;
        float m_viewSpeed;
        Vector3 m_viewPos;
        Vector3 m_viewRot;
        Matrix4 m_viewRotM4;
        List<OBJMesh> m_meshes = new List<OBJMesh>();
        const float m_viewSpeed_MIN = 0.002f;
        const float m_viewSpeed_MAX = 0.020f;
        const float m_viewAngle_MIN = (float)(2.0 * Math.PI) * 0.002f;
        const float m_viewAngle_MAX = (float)(2.0 * Math.PI) * 0.020f;

        public Form1() { InitializeComponent(); }

        private void Form1_Load(object sender, EventArgs e)
        {
            // initialize objects
            timer1.Start();
            timer1.Interval = 16;
            openFileDialog1.Filter = "*.obj | *.obj";
            saveFileDialog1.Filter = "*.obj | *.obj";
            // initialize variables
            m_viewPos = Vector3.forward * -3;
            m_viewRot = new Vector3(0, 0, 0);
            m_viewRotM4 = new Matrix4();
            m_viewSpeed = m_viewSpeed_MAX;
            m_viewAngle = m_viewAngle_MAX;
            // renderer
            UpdateViewMatrix();
            OBJRenderer.SetModelMatrix(new Matrix4(1));
            OBJRenderer.Init(pictureBox1.Width, pictureBox1.Height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // repaint picturebox
            pictureBox1.Refresh();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // check keys [hot]
                case Keys.ControlKey:
                    // update variables
                    m_viewAngle = m_viewAngle_MAX;
                    m_viewSpeed = m_viewSpeed_MAX;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Vector3 rgt = (m_viewRotM4 * new Vector4(Vector3.right, 1)).xyz.normalize();
            //Vector3 fwd = (m_viewRotM4 * new Vector4(Vector3.forward, 1)).xyz.normalize();

            m_viewRot.x = 0;
            m_viewRot.y = 0;
            Vector3 rgt = new Vector3(m_viewRotM4.row1.data).normalize();
            Vector3 fwd = new Vector3(m_viewRotM4.row3.data).normalize();

            switch (e.KeyCode)
            {
                // check keys [hot]
                case Keys.ControlKey:
                    // update variables
                    m_viewAngle = m_viewAngle_MIN;
                    m_viewSpeed = m_viewSpeed_MIN;
                    break;
                // check keys [move]
                //case Keys.Up:    m_viewPos += fwd * m_viewSpeed; break;
                //case Keys.Down:  m_viewPos -= fwd * m_viewSpeed; break;
                //case Keys.Left:  m_viewPos -= rgt * m_viewSpeed; break;
                //case Keys.Right: m_viewPos += rgt * m_viewSpeed; break;
                case Keys.W: m_viewPos += fwd * m_viewSpeed; break;
                case Keys.S: m_viewPos -= fwd * m_viewSpeed; break;
                case Keys.A: m_viewPos -= rgt * m_viewSpeed; break;
                case Keys.D: m_viewPos += rgt * m_viewSpeed; break;
                // check keys [turn]
                case Keys.NumPad2: m_viewRot.x = m_viewAngle; break;
                case Keys.NumPad8: m_viewRot.x =-m_viewAngle; break;
                case Keys.NumPad4: m_viewRot.y =-m_viewAngle; break;
                case Keys.NumPad6: m_viewRot.y = m_viewAngle; break;
            }
            // update rotation
            m_viewRotM4 *= Matrix4.Rotate(Vector3.up, -m_viewRot.y) * Matrix4.Rotate(rgt, -m_viewRot.x);
            // update view matrix
            UpdateViewMatrix();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e) {}

        private void UpdateViewMatrix()
        {
            // update view matrix
            OBJRenderer.SetViewMatrix(m_viewRotM4 * Matrix4.Translate(-m_viewPos));
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                foreach (OBJMesh mesh in m_meshes)
                {
                    OBJRenderer.SetModelMatrix(mesh.transform.matrix);
                    OBJRenderer.Render(mesh, e.Graphics);
                }
            }
            catch { }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OBJMesh mesh = OBJFile.Open(openFileDialog1.FileName);
                mesh.OptimizeData();
                mesh.transform.matrix = new Matrix4(1);
                m_meshes.Add(mesh);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // clear meshes
            m_meshes.Clear();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OBJFile.Save(saveFileDialog1.FileName, m_meshes);
            }
        }
    }
}