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
    public class ProgramAdderViewModel : ViewModelBase
    {

        private string _nameProgram;
        public string NameProgram
        {
            get => _nameProgram;

            set
            {
                if (_nameProgram == value) return;
                _nameProgram = value;
                RaisePropertyChanged(nameof(NameProgram));
            }
        }

        private Programs _selectedProgram;
        public Programs SelectedProgram
        {
            get => _selectedProgram;

            set
            {
                if(value == _selectedProgram) return;
                _selectedProgram = value;
                RaisePropertyChanged(nameof(SelectedProgram));
            }
        }

        public ObservableCollection<Programs> Programs { get; set; }

        public ProgramAdderViewModel()
        {
            using(error_collectorContext context = new error_collectorContext())
            {
                Programs = new ObservableCollection<Programs>(context.Programs);
            }
        }


        public RelayCommand AddProgram
        {
            get => new RelayCommand(() =>
            {
                using(error_collectorContext context = new error_collectorContext())
                {
                    if (IsNullOrEmptyName())
                    {
                        return;
                    }
                }
            });
        }

        public RelayCommand ChangeProgram
        {
            get => new RelayCommand(() =>
            {
                using (error_collectorContext context = new error_collectorContext())
                {

                }
            });
        }

        public RelayCommand DeleteProgram
        {
            get => new RelayCommand(() =>
            {
                using (error_collectorContext context = new error_collectorContext())
                {

                }
            });
        }

        public RelayCommand Exit
        {
            get => new RelayCommand(() =>
            {

            });
        }

        private bool IsNullOrEmptyName()
        {
            return string.IsNullOrWhiteSpace(NameProgram);

        }

    }
}
