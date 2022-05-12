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

        public Action CloseAction;

        private ObservableCollection<Instructions> _instructions;
        public ObservableCollection<Instructions> Instructions
        {
            get => _instructions;

            set
            {
                _instructions = value;
                RaisePropertyChanged(nameof(Instructions));
            }
        }

        private Instructions _selectedInstruction;
        public Instructions SelectedInstruction
        {
            get => _selectedInstruction;

            set
            {
                _selectedInstruction = value;
                RaisePropertyChanged(nameof(SelectedInstruction));
            }
        }

        private string _searchErrorText;
        public string SearchErrorText
        {

            get => _searchErrorText;

            set
            {
                if (value == _searchErrorText) return;

                _searchErrorText = value;
                Search();
            }
        }

        public int IdError { get; set; }

        private List<Instructions> _bufferInstructions;

        public InstructionBindingViewModel()
        {
            LoadInstruction();
        }

        public RelayCommand BindingInstruction
        {
            get => new RelayCommand(() =>
            {
                using (error_collectorContext context = new error_collectorContext())
                {
                    if(SelectedInstruction == null)
                    {
                        MsgBox.Error("Вы не выбрали инструкцию!");
                        return;
                    }

                    if(context.ErrorsInstructions.Where(x => x.IdError == IdError && x.IdInstruction == SelectedInstruction.Id).Any())
                    {
                        MsgBox.Error("Такая инструкция уже добавлена!");
                        return;
                    }

                    context.ErrorsInstructions.Add(new ErrorsInstructions() { IdError = IdError, IdInstruction = SelectedInstruction.Id });

                    context.SaveChanges();

                    MsgBox.Successfully("Успешная привязка!");

                    Utils.Events.EventsHandler.RaiseUpdateBindingInstruction();
                }
            });
        }

        public RelayCommand Close 
        {
            get => new RelayCommand(() =>
            {
                CloseAction?.Invoke();
            });
        }

        
        private void LoadInstruction()
        {
            using (error_collectorContext context = new error_collectorContext())
            {
                Instructions = new ObservableCollection<Instructions>(context.Instructions.ToArray());
                _bufferInstructions = new List<Instructions>(Instructions);
            }
        }

        private void Search()
        {
            Instructions = new ObservableCollection<Instructions>(_bufferInstructions.Where(e => e.Name.ToLower().Contains(SearchErrorText.ToLower())));
        }

    }
}
