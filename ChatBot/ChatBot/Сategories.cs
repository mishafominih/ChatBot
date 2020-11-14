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
        private Stack<string> history = new Stack<string>();
        private List<string> listNode;
        private int countButtonEnter = 5;
        private Graff graff;

        public Сategories(Graff graff)
        {
            this.graff = graff;
            listNode = graff.GetVarians();
            CreateWorkButton();
            InitializeComponent();
        }

        private int CountX(int x)
        {
            var countX = 1;
            for(var i = 1; x / (i + 1) - i >= 0; i++)
                countX = x / i;
            return countX;
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

        private void CreateWorkButton()
        {
            CreateDynamicButton(listNode);
            var next = newButton(new Point(830, 340), new Size(100, 50), "ДАЛЕЕ", ContinueClick);
            var back = newButton(new Point(70, 340), new Size(100, 50), "НАЗАД", BackClick);
            Controls.AddRange(new Control[]{next, back});
        }

        private void BackClick(object e)
        {
            if(history.Count != 0)
                history.Pop();
            graff.BackStep();
            listNode = graff.GetVarians();
            ContinueClick(e);
        }

        private void ContinueClick(object e)
        {
            if (listNode is null) return;
            if (CountClick * countButtonEnter > listNode.Count)
                CountClick = 0;
            if(history.Count > 1)
                DeleteButton();
            CreateDynamicButton(listNode.Skip(CountClick * countButtonEnter).ToList());
            CountClick++;
        }

        public void CreateDynamicButton(List<string> MyList)
        {
            if (MyList is null || MyList.Count == 0) return;
            int dx, dy;
            var countButtonInLine = 5;
            var step = MyList.Count > countButtonEnter ? countButtonEnter : MyList.Count;
            var countX = Math.Min(CountX(step), countButtonInLine);
            var countY = Math.Min(MyList.Count, countButtonEnter) / countX;
            var size = new Size(900 / (countX + 1), 255 / (countY + 1));
            dx = size.Width / countX;
            dy = size.Height / countY;
            var point = new Point(50 + dx / 2, 400 + dy / 2);
            dx += size.Width;
            dy += size.Height;
            for (int i = 0; i < step; i++)
            {
                var button = newButton(point, size, MyList[i], NextStep);
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

        private void NextStep(object e)
        {
            var text = ((Button)e).Text;
            history.Push(text);
            graff.NextStep(text);
            listNode = graff.GetVarians();
            DeleteButton();
            CreateDynamicButton(listNode);
            CountClick = 1;
        }

        private void DeleteButton()
        {
            foreach (var e in buttons)
                e.Dispose();
            buttons.Clear();
        }
    }
}
