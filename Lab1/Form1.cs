using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1 {
    public partial class Form1 : Form {

        enum Direction { Right, Down, Left, Up };
        Direction direction = Direction.Right;


        public Form1() {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e) { 
           
            switch (direction) {
                case Direction.Right:
                    label1.Top = 0;
                    label1.Left += 10;
                    if (label1.Right > ClientRectangle.Width) {
                        direction = Direction.Down;
                        label1.Left = ClientRectangle.Width - label1.Width;
                    }
                    label1.ForeColor = Color.FromArgb(255, 255 - label1.Left * 255 / (ClientRectangle.Width - label1.Width), 0);
                    break;
                case Direction.Down:
                    label1.Left = ClientRectangle.Width - label1.Width;
                    label1.Top += 10;
                    if (label1.Top > ClientRectangle.Height - label1.Height) {
                        direction = Direction.Left;
                        label1.Top = ClientRectangle.Height - label1.Height;
                    }
                    label1.ForeColor = Color.FromArgb(255 - label1.Top * 255 / (ClientRectangle.Height + label1.Height), label1.Top * 255 / (ClientRectangle.Height + label1.Height), label1.Top * 255 / (ClientRectangle.Height + label1.Height));
                    break;
                case Direction.Left:
                    label1.Left -= 10;
                    label1.Top = ClientRectangle.Height - label1.Height;
                    if (label1.Left < 0) {
                        direction = Direction.Up;
                        label1.Left = 0;
                    }
                    label1.ForeColor = Color.FromArgb(0, 255, label1.Left * 255 / (ClientRectangle.Width + label1.Width));
                    break;
                case Direction.Up:
                    label1.Top -= 10;
                    label1.Left = 0;
                    if (label1.Bottom < label1.Height) {
                        direction = Direction.Right;
                        label1.Top = 0;
                    }
                    label1.ForeColor = Color.FromArgb(255 - label1.Top * 255 / (ClientRectangle.Height - label1.Height), 255, 0);
                    break;
            }
        }

        private void Form1_Shown(object sender, EventArgs e) {
            Form2 form2 = new Form2();
            form2.form1 = this;
            form2.Show();
            form2.Left = ClientRectangle.Width + 15; // Сдвиг, чтобы формы не перекрывали друг друга
           
        }
    }
}
