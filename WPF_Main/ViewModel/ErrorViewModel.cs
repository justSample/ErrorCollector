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
    public class ErrorViewModel : ViewModelBase
    {

        private string _btnAddName;
        public string BtnAddName
        {
            get { return _btnAddName; }
            set
            {
                _btnAddName = value;
                RaisePropertyChanged(nameof(BtnAddName));
            }
        }

        public Action CloseAction { get; set; }

        private Action ErrorAction { get; set; }



        private Programs _seletedProgram;
        public Programs SelectedProgram
        {
            get
            {
                return _seletedProgram;
            }

            set
            {
                if (value == _seletedProgram) return;
                _seletedProgram = value;
                RaisePropertyChanged(nameof(SelectedProgram));
            }
        }

        private string _errorName;
        public string ErrorName
        {
            get
            {
                return _errorName;
            }

            set
            {
                if(value == _errorName) return;

                _errorName = value;
                RaisePropertyChanged(nameof(ErrorName));
            }
        }

        private string _causeError;
        public string CauseError
        {
            get
            {
                return _causeError;
            }

            set
            {
                if(value ==_causeError) return;

                _causeError = value;

                RaisePropertyChanged(nameof(CauseError));
            }
        }

        private string _solutionError;
        public string SolutionError
        {
            get
            {
                return _solutionError;
            }

            set
            {
                if( value == _solutionError) return;

                _solutionError = value;

                RaisePropertyChanged(nameof(SolutionError));
            }
        }

        private string _commentError;
        public string CommentError
        {
            get
            {
                return _commentError;
            }

            set
            {
                if( _commentError == value) return;

                _commentError = value;

                RaisePropertyChanged(nameof(CommentError));
            }
        }

        private ObservableCollection<Programs> _programs;
        public ObservableCollection<Programs> Programs
        {
            get
            {
                return _programs;
            }

            set
            {
                if (value == null || _programs == value) return;

                _programs = value;
                RaisePropertyChanged(nameof(Programs));

            }
        }

        private ObservableCollection<Sql_Image> _images;
        public ObservableCollection<Sql_Image> Images
        {
            get
            {
                return this._images;
            }

            set
            {
                if (_images == value) return;
                _images = value;
                RaisePropertyChanged(nameof(Images));
            }
        }

        private Utils.Buffer ImageBuffer;

        public RelayCommand SetImage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    using(OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Title = "Выбор изображений";
                        openFileDialog.Filter = "Png (*.png)|*.png|Jpg (*.jpg)|*.jpg|All files(*.*)|*.*";
                        openFileDialog.Multiselect = true;

                        if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                        ClearBuffer();


                        ImageBuffer.Data = ByteOperation.GetByteImages(openFileDialog.FileNames);
                        Images = new ObservableCollection<Sql_Image>(ByteOperation.GetImages(ImageBuffer.Data));
                    }
                });
            }

        }

        public RelayCommand SetImageFromBuffer
        {
            get
            {
                return new RelayCommand(() => 
                {
                    ClearBuffer();

                    ImageBuffer.Data = ByteOperation.GetByteFromBuffer();

                    if(ImageBuffer.Data == null)
                    {
                        MsgBox.Error("В буфере данных не найдено изображения!\nПопробуйте указать фотографию вручную по пути!");
                        return;
                    }

                    Images = new ObservableCollection<Sql_Image>(ByteOperation.GetImages(ImageBuffer.Data));

                });
            }
        }

        //FIX ME: UserId
        public RelayCommand AddError
        {
            get
            {
                return new RelayCommand(() =>
                {
                    ErrorAction?.Invoke();
                });
            }
        }

        private int _idError = -1;
        
        //Есть ли утечка памяти?
        public RelayCommand CloseWindow
        {
            get 
            { 
                return new RelayCommand(() => 
                {
                    CloseAction?.Invoke();
                }); 
            }
        }

        public ErrorViewModel()
        {
            using (error_collectorContext context = new error_collectorContext())
            {
                Programs = new ObservableCollection<Programs>(context.Programs.ToArray());
                BtnAddName = "Добавить ошибку";
                ErrorAction = new Action(AddErrorMethod);
            }
        }

        public void InitEdit(Errors error)
        {
            if(error == null) return;

            SelectedProgram = Programs.Where(p => p.Id == error.IdProgram).First();
            ErrorName = error.Name;
            CauseError = error.Cause;
            SolutionError = error.Solution;
            CommentError = error.Comment;

            ClearBuffer();
            ImageBuffer.Data = error.Images;
            Images = new ObservableCollection<Sql_Image>(ByteOperation.GetImages(error.Images));

            BtnAddName = "Изменить ошибку";
            ErrorAction = new Action(ChangeErrorMethod);
            _idError = error.Id;
        }

        private void AddErrorMethod()
        {
            if (!IsEverythingFilled()) return;

            using (error_collectorContext context = new error_collectorContext())
            {
                context.Errors.Add(new Errors()
                {
                    IdProgram = SelectedProgram.Id,
                    IdUserCreated = 1,
                    Name = ErrorName,
                    DateCreated = DateTime.Now,
                    Cause = CauseError,
                    Solution = SolutionError,
                    Comment = CommentError,
                    Images = ImageBuffer.Data
                });

                context.SaveChanges();

                MessageBox.Show("Ошибка добавлена!", "Хорошее сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetEmpty();
            }
        }

        private void ChangeErrorMethod()
        {
            if (!IsEverythingFilled()) return;

            using (error_collectorContext context = new error_collectorContext())
            {
                var error = context.Errors.Where(x => x.Id == _idError).First();

                error.Name = ErrorName;
                error.Cause = CauseError;
                error.Solution = SolutionError;
                error.Comment = CommentError;
                error.Images = ImageBuffer.Data;
                BtnAddName = "Изменить ошибку";
                ErrorAction = new Action(ChangeErrorMethod);

                context.Update(error);
                context.SaveChanges();

                MessageBox.Show("Ошибка изменена!", "Хорошее сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void SetEmpty()
        {
            SelectedProgram = null;
            ErrorName = string.Empty;
            CauseError = string.Empty;
            SolutionError = string.Empty;
            CommentError = string.Empty;
            Images = null;
        }

        private void ClearBuffer()
        {
            if(ImageBuffer != null)
            {
                ImageBuffer.Data = null;
            }
            ImageBuffer = null;
            GC.Collect();

            ImageBuffer = new Utils.Buffer();
        }

        /// <summary>
        /// Проверка на заполнение всех полей
        /// </summary>
        /// <returns></returns>
        private bool IsEverythingFilled()
        {

            if(SelectedProgram == null)
            {
                MessageBox.Show("Программа не выбрана", "Код ошибки: 1", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(ErrorName))
            {
                MessageBox.Show("Название ошибки пустое", "Код ошибки: 1", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(CauseError))
            {
                MessageBox.Show("Причина ошибки пустое", "Код ошибки: 1", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(SolutionError))
            {
                MessageBox.Show("Решение ошибки пустое", "Код ошибки: 1", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

    }
}
