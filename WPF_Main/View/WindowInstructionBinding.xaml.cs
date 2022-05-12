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
using WPF_Main.ViewModel;

namespace WPF_Main.View
{
    /// <summary>
    /// Логика взаимодействия для WindowInstructionBinding.xaml
    /// </summary>
    public partial class WindowInstructionBinding : Window
    {
        public WindowInstructionBinding()
        {
            InitializeComponent();
            InitEvents();
        }

        public WindowInstructionBinding(int idError)
        {
            InitializeComponent();
            ((InstructionBindingViewModel)this.DataContext).IdError = idError;
            InitEvents();
        }

        private void InitEvents()
        {
            ((InstructionBindingViewModel)this.DataContext).CloseAction = this.Close;
        }
    }
}
