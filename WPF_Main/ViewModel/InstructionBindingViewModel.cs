using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WPF_Main.Models;
using WPF_Main.Utils;
namespace WPF_Main.ViewModel
{
    public class InstructionBindingViewModel : ViewModelBase
    {

        public ObservableCollection<Instructions> Instructions { get; set; }

        public InstructionBindingViewModel()
        {
            using(error_collectorContext context = new error_collectorContext())
            {

                Instructions = new ObservableCollection<Instructions>(context.Instructions.ToArray());

            }
        }

    }
}
