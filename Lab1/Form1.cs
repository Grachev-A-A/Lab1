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
        // Перечисление направлений движения лейбла
        enum Direction { Right, Down, Left, Up };

        // Создание переменной направления движения и инициализация её движением направо
        Direction direction = Direction.Right;


        public Form1() {
            InitializeComponent();
        }

        // Все действия по перемещению текста производятся в методе таймера!
        private void timer1_Tick(object sender, EventArgs e) { 
           // Набор действий в зависимости от направления
            switch (direction) {
                case Direction.Right: //Если направо:
                    label1.Top = 0; //Приклеить к верхнему краю. Помогает при изменении размеров
                    label1.Left += 10; //Передвинуть вправо на 10
                    if (label1.Right > ClientRectangle.Width) { //Если правый край лейбла за пределами окна:
                        direction = Direction.Down; // Изменит направление на "Вниз"
                        label1.Left = ClientRectangle.Width - label1.Width; // Сдвинуть лейбл так, чтобы он помещался на экране
                        /*
                         * Здесь используется присвоение значения координат левому краю
                         * (хотя было бы удобней правому)
                         * т. к. присваивать значения можно только левому и верхнему краям лейбла. 
                         * Правый и нижний могут использоваться только для получения значений
                         * (например, для проверки, как в if'е).
                         */
                    }
                    /*
                     * Присвоение цвета.
                     * 
                     * Расположено строго после(!) проверки на выход за границы окна.
                     * 
                     * Цвета рассчитываются так: для неизменяемого значения (255 или 0 у исходного и
                     * конечного цветов направления) в соответствующем цвете (R, G, или B) ставится константное значение,
                     * если значение идёт от 0 к 255:
                     *      - Если координата (X или Y), по которой движется лейбл, возрасает, то
                     *      берётся эта координата, умноженная на 255, делится на (размер окна 
                     *      по данной координате - размер лейбла по данной координате;
                     *      - Если убывает, то дробь из прошлого варианта отнимается от 255
                     * если от 255 к 0, то наоборот.
                     * 
                     * 
                     * При подобной реализации у программы возможны вылеты при быстром догоняющем лейбл сужении окна.
                     * К сожалению, избавление от этого очень сильно цсложнит код.
                     */
                    label1.ForeColor = Color.FromArgb(255, 255 - label1.Left * 255 / (ClientRectangle.Width - label1.Width), 0);
                    // в данном случае R=255, G=(255 -> 0) (X=(0 -> 255)), B=0.
                    break;
                case Direction.Down: //если направо
                    label1.Left = ClientRectangle.Width - label1.Width; //Приклеить к правому краю
                    label1.Top += 10; //движение
                    if (label1.Top > ClientRectangle.Height - label1.Height) { // Если верхняя грань ниже, чем позволяет высота лейбла
                                                                                // то есть нижняя грань вышла за нижнюю границу окна
                        direction = Direction.Left; // изменить направление
                        label1.Top = ClientRectangle.Height - label1.Height; //поднять на допустимую высоту
                    }
                    //Цвета: R (255 -> 0), G (0 -> 255), B (0 -> 255), X (0 -> 255)
                    label1.ForeColor = Color.FromArgb(255 - label1.Top * 255 / (ClientRectangle.Height - label1.Height), label1.Top * 255 / (ClientRectangle.Height - label1.Height), label1.Top * 255 / (ClientRectangle.Height - label1.Height));
                    break;
                case Direction.Left: // аналогично
                    label1.Left -= 10;
                    label1.Top = ClientRectangle.Height - label1.Height;
                    if (label1.Left < 0) {
                        direction = Direction.Up;
                        label1.Left = 0;
                    }
                    // R = 0, G=255, B = (255 -> 0), X = (255 -> 0)
                    label1.ForeColor = Color.FromArgb(0, 255, label1.Left * 255 / (ClientRectangle.Width - label1.Width));
                    break;
                case Direction.Up: //аналогично
                    label1.Top -= 10;
                    label1.Left = 0;
                    if (label1.Bottom < label1.Height) {
                        direction = Direction.Right;
                        label1.Top = 0;
                    }
                    // R= (0 -> 255), G = 255, B = 0, Y = (255 -> 0)
                    label1.ForeColor = Color.FromArgb(255 - label1.Top * 255 / (ClientRectangle.Height - label1.Height), 255, 0);
                    break;
            }
        }
        // Создание и показ второй формы происходит в методе, вызываемом при загрузке первой
        private void Form1_Shown(object sender, EventArgs e) {
            Form2 form2 = new Form2(); // Объявление переменной и создание её объекта
            form2.form1 = this; //передача во вторую форму ссылки на первую для возможности остановить лейбл
            form2.Show(); //показ второй формы
            form2.Left = ClientRectangle.Width + 15; // Сдвиг, чтобы формы не перекрывали друг друга. НЕ ОБЯЗАТЕЛЬНО!
           
        }
    }
}
