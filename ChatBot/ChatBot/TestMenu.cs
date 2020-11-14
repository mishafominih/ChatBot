using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatBot
{
    public partial class TestMenu : Form
    {
        private int countButtonEnter = 8;
        private List<Button> buttons = new List<Button>();

        public TestMenu(List<string> listQuery)
        {
            CreateDynamicButton(listQuery);
            InitializeComponent();
        }

        private Button newButton(Point point, Size size, string text, Action<object> action)
        {
            var button = new Button();
            button.Location = point;
            button.Text = text;
            button.Size = size;
            button.Click += (e, s) => action(e);
            return button;
        }

        public void CreateDynamicButton(List<string> MyList)
        {
            if (MyList is null || MyList.Count == 0) return;
            int dx, dy;
            var countButtonInLine = 4;
            var step = MyList.Count > countButtonEnter ? countButtonEnter : MyList.Count;
            var countX = Math.Min(CountX(step), countButtonInLine);
            var countY = Math.Min(MyList.Count, countButtonEnter) / countX + 1;
            var size = new Size(900 / (countX + 1), 255 / (countY + 1));
            dx = size.Width / countX;
            dy = size.Height / countY;
            var point = new Point(50 + dx / 2, 410 + dy / 2 + size.Height);
            dx += size.Width;
            dy += size.Height;
            for (int i = 0; i < step; i++)
            {
                var button = newButton(point, size, MyList[i], EnterQuery);
                point.X += dx;
                if (point.X + size.Width >= 950)
                {
                    point.X = 50 + size.Width / (2 * countX);
                    point.Y += dy;
                }
                buttons.Add(button);
                Controls.Add(button);
                Controls.SetChildIndex(button, i);
            }
        }

        private void EnterQuery(object obj)
        {
            var text = ((Button)obj).Text;
            foreach( var e in Controls)
            {
                if (e is TextBox)
                {
                    ((TextBox)e).Text = text;
                    break;

                }
            }
        }

        private int CountX(int x)
        {
            var countX = 1;
            for (var i = 1; x / (i + 1) - i >= 0; i++)
                countX = x / i;
            return countX;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
