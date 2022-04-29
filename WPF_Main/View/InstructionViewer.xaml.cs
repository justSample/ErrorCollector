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
using WPF_Main.Models;
using WPF_Main.ViewModel;

namespace WPF_Main.View
{
    /// <summary>
    /// Логика взаимодействия для InstructionViewer.xaml
    /// </summary>
    public partial class InstructionViewer : Window
    {
        public InstructionViewer()
        {
            InitializeComponent();
            ((InstructionViewerViewModel)DataContext).Window = this;
        }

        public void SetInstruction(Instructions instruction) => ((InstructionViewerViewModel)DataContext).SetInstruction(instruction);
        
    }
}
