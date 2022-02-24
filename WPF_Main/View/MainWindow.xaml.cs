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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            listBoxCheck.ItemsSource = new string[] { "Alo", "Bro", "Ошибка в Рике EFFFF", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha", "haha" };

            combo.ItemsSource = new string[] { "Bra", "A", "B", "C"};

        }
    }
}
