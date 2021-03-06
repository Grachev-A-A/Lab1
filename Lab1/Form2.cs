﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1 {
    public partial class Form2 : Form {

        public Form1 form1; // переменная, хранящая ссылку на первую форму

        public Form2() {
            InitializeComponent();
            
        }

        //метод, отключающий движение
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            form1.timer1.Enabled = comboBox1.SelectedIndex == 0; // Свойству Enabled таймера первой формы нужно присваивать логическое значение в зависимости от выбора пользователя
            /*
             * У меня в задание за это отвечает Комбобокс, у вас, скорее всего, что-то другое.
             * Соответственно У вас метод будет другой и реализация другая.
             * самый универсальный вариант - присваивать свойству его же инвертированное значение:
             * form1.timer1.Enabled = !form1.timer1.Enabled;
             */
        }

        private void Form2_Shown(object sender, EventArgs e) {
            comboBox1.SelectedIndex = 0; // небольшой костыль эксклюзивно для комбобокса
        }

        /*
         * Далее код оставлен без пояснений, т к он только для восьмого варианта
         * плюс там используется сложноватая механика генерации случайных строк
         * плюс там идёт оперирование массивами с длиной = 0
         * и я сам не понимаю, как это может работать.
         * Но работает!
         */

        private void button2_Click(object sender, EventArgs e) {
            if(textBox1.Lines.Length == 0) {
                MessageBox.Show("В поле нет ни одной строки!");
                return;
            }

            string[] tmp = new string[textBox1.Lines.Length - 1];
            for (int i = 0; i < tmp.Length; i++) {
                tmp[i] = textBox1.Lines[i + 1];
            }
            textBox1.Lines = tmp;
        }

        private void button1_Click(object sender, EventArgs e) {
            Random rnd = new Random();
            string src = "AaBbCcDdEeFfGgHhIiGgKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz1234567890АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщъЫыьЭэЮюЯя";
            string str = "";
            for (int i = 0; i < rnd.Next(1, 33); i++) {
                str += src[rnd.Next(src.Length)];
            }
            textBox1.AppendText(str + "\n");
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            label1.Text = $"Количество строк: {(textBox1.Lines.Length == 0 ? 0 : textBox1.Lines.Length -1)}";
        }
    }
}
