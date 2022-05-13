using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WPF_Main.Models;
using WPF_Main.Utils;
using WPF_Main.View;

namespace WPF_Main.ViewModel
{
    public class InstructionAdderViewModel : ViewModelBase
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
                if(_instructions[_currentIndex].Image.Data == value.Data) return;

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

        private List<BufferInstruction> _instructions;

        public InstructionAdderWindow Window { get; set; }

        public InstructionAdderViewModel()
        {
            _instructions = new List<BufferInstruction>
            {
                new BufferInstruction() { IndexPage = 1 }
            };
        }



        public RelayCommand LoadImageFromPath
        {
            get => new RelayCommand(() =>
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Выбор изображения";
                    openFileDialog.Filter = "Jpg (*.jpg)|*.jpg|Png (*.png)|*.png|All files(*.*)|*.*";
                    openFileDialog.Multiselect = false;

                    if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                    CurrentImage = ByteOperation.GetImage(ByteOperation.GetByteImage(openFileDialog.FileName));
                }
            });
        }

        public RelayCommand LoadImageFromBuffer
        {
            get => new RelayCommand(() =>
            {
                try
                {
                    CurrentImage = ByteOperation.GetImage(ByteOperation.GetByteFromBuffer());
                }
                catch
                {
                    MsgBox.Error("В буфере данных не найдено изображения!\nПопробуйте указать фотографию вручную по пути!");
                }
            });
        }

        public RelayCommand SaveInstruction
        {
            get => new RelayCommand(() =>
            {
                using (error_collectorContext context = new error_collectorContext())
                {
                    ModelWindow_QuestionTextBox msg = new ModelWindow_QuestionTextBox();
                    bool IsAdd = (bool)msg.ShowDialog();

                    if (!IsAdd) return;

                    string instructionName = msg.InstructionName;

                    context.Instructions.Add(new Instructions() { IdUserCreated=1, Name=instructionName, DateCreated=DateTime.Now, DateChange = DateTime.Now });

                    context.SaveChanges();

                    int index = context.Instructions.Where(x => x.Name == instructionName).Select(x => x.Id).First();

                    for (int i = 0; i < _instructions.Count; i++)
                    {
                        Steps step = new Steps()
                        {
                            IdInstructions = index,
                            Number = _instructions[i].IndexPage,
                            Header = _instructions[i].Header,
                            ActionDescription = _instructions[i].Description,
                            Images = _instructions[i].Image.Data
                        };

                        context.Steps.Add(step);
                    }

                    context.SaveChanges();

                    MsgBox.Successfully("Всё успешно добавлено!");

                }
            });
        }

        public RelayCommand NotSaveAndClose
        {
            get => new RelayCommand(() =>
            {
                Window.DialogResult = false;
            });
        }

        public RelayCommand BackPage
        {
            get => new RelayCommand(() =>
            {

                if((_currentIndex - 1) > -1)
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

        public RelayCommand NewPage
        {
            get => new RelayCommand(() =>
            {
                
                int index = _instructions.Count + 1;
                _instructions.Add(new BufferInstruction() { IndexPage = index });
                _currentIndex = _instructions.Count - 1;
                ChangePage();

            });
        }

        public RelayCommand DeletePage
        {
            get => new RelayCommand(() =>
            {
                if(_instructions.Count - 1 <= 0)
                {
                    MsgBox.Warning("Нельзя удалить последнюю страницу");
                    return;
                }

                var result = MsgBox.Question("Вы точно хотите удалить страницу? Вся информация будет потеряна");

                if (result != DialogResult.Yes) return;

                _instructions.RemoveAt(_currentIndex);

                for (int i = _currentIndex; i < _instructions.Count; i++)
                {
                    _instructions[i].IndexPage = (i + 1);
                }

                if (_currentIndex >= _instructions.Count)
                    _currentIndex -= 1;

                ChangePage();

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
