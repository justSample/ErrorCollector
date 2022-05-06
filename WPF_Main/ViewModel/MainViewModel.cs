using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPF_Main.Models;
using WPF_Main.Utils;
using WPF_Main.View;

namespace WPF_Main.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        //Scaffold-DbContext -provider MySql.Data.EntityFrameworkCore -connection "server=localhost;user=root;password=root;database=error_collector;" -OutputDir Models -f
        //Если будут ????? вместо российских символов, то юзаем это: Charset=utf8;
        //"server=localhost;user=root;password=root;database=error_collector;Charset=utf8;"

        public Users User { get; set; }

        public ObservableCollection<Programs> Programs { get; set; }

        public ObservableCollection<InstructionPrefab> BtnInstructions { get; set; }

        private Programs _selectedProgram;
        public Programs SelectedProgram
        {
            get => _selectedProgram;
            
            set
            {
                if (_selectedProgram == value) return;

                _selectedProgram = value;
                RaisePropertyChanged(nameof(SelectedProgram));
                using (error_collectorContext context = new error_collectorContext())
                {
                    var listErrors = context.Errors.Where(e => e.IdProgram == _selectedProgram.Id).ToList();
                    Errors = new ObservableCollection<Errors>(listErrors);
                    _bufferErrors = listErrors;
                }

            }
        }

        private ObservableCollection<Errors> _errors;
        public ObservableCollection<Errors> Errors
        {
            get => _errors;
            
            set
            {
                _errors = value;
                RaisePropertyChanged(nameof(Errors));
            }
        }

        private Errors _selectedError;
        public Errors SelectedError
        {
            get => _selectedError;
            
            set
            {
                if (_selectedError == value || value == null) return;

                _selectedError = value;

                using (error_collectorContext context = new error_collectorContext())
                {
                    Errors error = context.Errors.Where(e => e.Id == SelectedError.Id).First();

                    NameUserCreate = error.IdUserCreatedNavigation.Name;

                    byte[] data = error.Images;
                    if (data != null)
                        Images = new ObservableCollection<Sql_Image>(ByteOperation.GetImages(data));

                    int[] idsInstructions = context.ErrorsInstructions.Where(x => x.IdError == error.Id).Select(x => x.IdInstruction).ToArray();

                    List<Instructions> instructions = new List<Instructions>();

                    for (int i = 0; i < idsInstructions.Length; i++)
                    {
                        instructions.Add(context.Instructions.Where(x => x.Id == idsInstructions[i]).Single());
                    }

                    InitInstructions(instructions.ToArray());

                    instructions.Clear();
                }

                RaisePropertyChanged(nameof(SelectedError));

                
                GC.Collect(GC.MaxGeneration);
            }
        }

        private string _nameUserCreate;
        public string NameUserCreate
        {
            get => _nameUserCreate;
            
            set
            {
                if(value == _nameUserCreate) return;

                _nameUserCreate = value;

                RaisePropertyChanged(nameof(NameUserCreate));
            }
        }

        private ObservableCollection<Sql_Image> _images;
        public ObservableCollection<Sql_Image> Images
        {
            get => this._images;
            
            set
            {
                if(_images == value) return;   
                _images = value;
                RaisePropertyChanged(nameof(Images));
            }
        }

        private string _searchErrorText;
        public string SearchErrorText
        {

            get => _searchErrorText;

            set
            {
                if(value == _searchErrorText) return;

                _searchErrorText = value;
                Search();
            }
        }


        private List<Errors> _bufferErrors;



        public MainViewModel()
        {
            using(error_collectorContext context = new error_collectorContext())
            {
                User = context.Users.FirstAsync().Result;
                Programs = new ObservableCollection<Programs>(context.Programs.ToList());
                BtnInstructions = new ObservableCollection<InstructionPrefab>();
            }
            
        }


        public RelayCommand OpenWindowCreateError
        {
            get
            {
                return new RelayCommand(() =>
                {
                    View.ErrorAdder viewError = new View.ErrorAdder();
                    viewError.ShowDialog();
                });
            }
        }

        public RelayCommand DeleteError
        {
            get
            {
                return new RelayCommand(() =>
                {

                    if (SelectedError == null) return;

                    DialogResult result = Utils.MsgBox.Question($"Вы хотите удалить: {SelectedError.Name}?");

                    if (result == DialogResult.No) return;

                    using (error_collectorContext context = new error_collectorContext())
                    {
                        var error = context.Errors.Where(e => e.Id == SelectedError.Id).First();
                        context.Errors.Remove(error);
                        context.SaveChanges();
                        var listErrors = context.Errors.Where(e => e.IdProgram == _selectedProgram.Id).ToList();
                        Errors = new ObservableCollection<Errors>(listErrors);
                    }
                });
            }
        }

        public RelayCommand EditError
        {
            get
            {
                return new RelayCommand(() =>
                {
                    View.ErrorAdder viewError = new View.ErrorAdder(SelectedError);

                    viewError.ShowDialog();
                });
            }
        }

        public RelayCommand AddNewInstruction
        {
            get => new RelayCommand(() =>
            {
                new View.InstructionAdderWindow().ShowDialog();
            });
        }

        public RelayCommand BindInstruction
        {
            get => new RelayCommand(() =>
            {

            });
        }

        private void InitInstructions(Instructions[] instructions)
        {
            BtnInstructions.Clear();
            for (int i = 0; i < instructions.Length; i++)
            {
                BtnInstructions.Add(new InstructionPrefab(instructions[i]));
            }
        }

        private void Search()
        {
            Errors = new ObservableCollection<Errors>(_bufferErrors.Where(e => e.Name.ToLower().Contains(SearchErrorText.ToLower())));
        }

    }
}