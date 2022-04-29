using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Main.Utils;

namespace WPF_Main.View
{
    /// <summary>
    /// Логика взаимодействия для ModelWindow_QuestionTextBox.xaml
    /// </summary>
    public partial class ModelWindow_QuestionTextBox : Window
    {

        public string InstructionName { get; set; }

        public ModelWindow_QuestionTextBox()
        {
            InitializeComponent();
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBox_NameInstruction.Text))
            {
                MsgBox.Warning("Нельзя без названия инструкции");
                return;
            }

            InstructionName = txtBox_NameInstruction.Text;
            DialogResult = true;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
