using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WPF_Main.Models;

namespace WPF_Main.ViewModel
{
    public class ErrorViewModel : ViewModelBase
    {
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


                        ImageBuffer.Data = GetByteImages(openFileDialog.FileNames);
                        Images = new ObservableCollection<Sql_Image>(GetImages(ImageBuffer.Data));
                    }
                });
            }

        }

        public RelayCommand AddError
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (!IsEverythingFilled()) return;

                    using(error_collectorContext context = new error_collectorContext())
                    {
                        context.Errors.Add(new Errors() { 
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

                    }
                });
            }
        }

        private Utils.Buffer ImageBuffer;

        public ErrorViewModel()
        {
            using(error_collectorContext context = new error_collectorContext())
            {

               Programs = new ObservableCollection<Programs>(context.Programs.ToArray());

            }
        }

        private Sql_Image[] GetImages(byte[] data)
        {

            if (data == null) return null;

            List<Sql_Image> _images = new List<Sql_Image>();

            int countImages = BitConverter.ToInt32(data, 0);

            int indexInByteArr = 4;

            for (int i = 0; i < countImages; i++)
            {
                int countBytes = BitConverter.ToInt32(data, indexInByteArr);

                indexInByteArr += 4;

                byte[] dataImage = new byte[countBytes];

                Buffer.BlockCopy(data, indexInByteArr, dataImage, 0, countBytes);

                indexInByteArr += countBytes;

                _images.Add(new Sql_Image() { Data = dataImage });

            }

            return _images.ToArray();

        }

        private byte[] GetByteImages(string[] paths)
        {

            List<byte> vs = new List<byte>();

            byte[] dataCountImages = BitConverter.GetBytes(paths.Length);

            vs.AddRange(dataCountImages);

            foreach (var path in paths)
            {
                byte[] dataImage = File.ReadAllBytes(path);

                vs.AddRange(BitConverter.GetBytes(dataImage.Length));
                vs.AddRange(dataImage);
            }

            return vs.ToArray();
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
