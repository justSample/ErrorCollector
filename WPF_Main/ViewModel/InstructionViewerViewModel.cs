using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Main.Models;
using WPF_Main.Utils;
using WPF_Main.View;

namespace WPF_Main.ViewModel
{
    public class InstructionViewerViewModel : ViewModelBase
    {

        public int CountPages
        {
            get => _instructions[_currentIndex].IndexPage;

            set
            {
                _instructions[_currentIndex].IndexPage = value;

                RaisePropertyChanged(nameof(CountPages));
            }
        }

        public string NameContentPage
        {
            get => _instructions[_currentIndex].Header;

            set
            {
                _instructions[_currentIndex].Header = value;

                RaisePropertyChanged(nameof(NameContentPage));
            }
        }

        public Sql_Image CurrentImage
        {
            get => _instructions[_currentIndex].Image;

            set
            {
                if (_instructions[_currentIndex].Image.Data == value.Data) return;

                _instructions[_currentIndex].Image = value;

                RaisePropertyChanged(nameof(CurrentImage));

            }
        }

        public string Description
        {
            get => _instructions[_currentIndex].Description;

            set
            {
                _instructions[_currentIndex].Description = value;

                RaisePropertyChanged(nameof(Description));
            }
        }

        private int _currentIndex = 0;

        private List<Instruction> _instructions;

        public InstructionViewer Window { get; set; }

        public InstructionViewerViewModel()
        {
            _instructions = new List<Instruction>();
        }

        public void SetInstruction(Instructions instruction)
        {
            using (error_collectorContext context = new error_collectorContext())
            {
                int idInst = context.Instructions.Where(x => x.Id == instruction.Id).Select(x => x.Id).Single();

                Steps[] steps = context.Steps.Where(x => x.IdInstructions == idInst).ToArray();

                for (int i = 0; i < steps.Length; i++)
                {
                    _instructions.Add(new Instruction());
                    _instructions[i].SetFromStep(steps[i]);
                }
            }

            ChangePage();
        }

        public RelayCommand Close
        {
            get => new RelayCommand(() =>
            {
                _instructions.Clear();
                _currentIndex = 0;
                Window.DialogResult = false;
            });
        }

        public RelayCommand BackPage
        {
            get => new RelayCommand(() =>
            {

                if ((_currentIndex - 1) > -1)
                {
                    _currentIndex -= 1;
                    ChangePage();
                }

            });
        }

        public RelayCommand NextPage
        {
            get => new RelayCommand(() =>
            {
                if ((_currentIndex + 1) < _instructions.Count)
                {
                    _currentIndex += 1;
                    ChangePage();
                }
            });
        }


        private void ChangePage()
        {
            RaisePropertyChanged(nameof(CountPages));
            RaisePropertyChanged(nameof(NameContentPage));
            RaisePropertyChanged(nameof(CurrentImage));
            RaisePropertyChanged(nameof(Description));
        }

    }
}
