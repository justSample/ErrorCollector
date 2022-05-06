using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WPF_Main.Utils
{
    public static class MsgBox
    {

        public static void Warning(string desc)
        {
            MessageBox.Show(desc, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Номер ошибки:
        /// 100 - Ошибка на стороне пользователя
        /// 200 - Ошибка на стороне программы
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="numError"></param>
        public static void Error(string desc, int numError = 100)
        {
            MessageBox.Show(desc, $"Ошибка: {numError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Successfully(string desc)
        {
            MessageBox.Show(desc, "Хорошее сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Question(string desc)
        {
            return MessageBox.Show(desc, "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

    }
}
