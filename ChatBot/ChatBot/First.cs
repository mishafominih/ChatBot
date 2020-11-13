using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatBot
{
    public class First : Form
    {
        private Button button2;
        private Button button3;
        private Button button1;

        public First()
        {

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(First));
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Location = new System.Drawing.Point(293, 572);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(298, 91);
            this.button2.TabIndex = 1;
            this.button2.Text = "Выбор категорий";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(930, 572);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(296, 91);
            this.button1.TabIndex = 2;
            this.button1.Text = "Ввести запрос";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.Location = new System.Drawing.Point(615, 67);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(261, 106);
            this.button3.TabIndex = 3;
            this.button3.Text = "Чат-Бот";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // First
            // 
            this.ClientSize = new System.Drawing.Size(1482, 703);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Name = "First";
            this.ResumeLayout(false);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
