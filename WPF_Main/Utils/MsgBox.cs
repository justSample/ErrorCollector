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

        public static void Error(string desc, int numError)
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
