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
    public partial class Сategories : Form
    {
        private List<Button> buttons = new List<Button>(); 
        private int CountClick = 1;
        private List<string> list;

        public Сategories(Graff graff)
        {
            list = graff.GetVarians();
            CreateButton();
            InitializeComponent();
        }

        private int CountX(int x)
        {
            var countX = 1;
            for(var i = 1; x / (i + 1) - i >= 0; i++)
                countX = x / i;
            return countX;
        }

        private void CreateButton()
        {
            CreateDynamicButton(list);
            if (list.Count > 10)
            {
                var button = new Button();
                button.Location = new Point(70, 340);
                button.Size = new Size(100, 50);
                button.Text = "ДАЛЕЕ";
                button.Click += (e, s) => ContinueClick(e, s);
                Controls.Add(button);
            }
        }

        private void ContinueClick(object e, EventArgs s)
        {
            var but = new Button();
            but.Location = new Point(0, 0);
            but.Size = new Size(100, 100);
            Controls.Add(but);
            if (CountClick * 10 > list.Count)
                CountClick = 0;
            DeleteButton();
            CreateDynamicButton(list.Skip(CountClick * 10).ToList());
            CountClick++;
        }

        private void CreateDynamicButton(List<string> MyList)
        {
            int dx, dy;
            var step = MyList.Count > 10 ? 10 : MyList.Count;
            var countX = Math.Min(CountX(step), 5);
            var countY = Math.Min(MyList.Count, 10) / countX;
            var size = new Size(900 / (countX + 1), 255 / (countY + 1));
            dx = size.Width / countX;
            dy = size.Height / countY;
            var point = new Point(50 + dx / 2, 400 + dy / 2);
            dx += size.Width;
            dy += size.Height;
            for (int i = 0; i < step; i++)
            {
                var button = new Button();
                button.Location = point;
                button.Text = MyList[i];
                button.Size = size;
                point.X += dx;
                if (point.X + size.Width >= 950)
                {
                    point.X = 50 + size.Width / (2 * countX);
                    point.Y += dy;
                }
                buttons.Add(button);
                Controls.Add(button);
            }
        }

        private void DeleteButton()
        {
            foreach (var e in buttons)
                e.Dispose();
            buttons.Clear();
        }
    }
}
