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

        private Programs _errorName;
        public Programs ErrorName
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

        private Programs _causeError;
        public Programs CauseError
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

        private Programs _solutionError;
        public Programs SolutionError
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

        private Programs _commentError;
        public Programs CommentError
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

        private RelayCommand _setImage;
        public RelayCommand SetImage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    using(OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Title = "Выбор изображений";
                        openFileDialog.Filter = "Jpg (*.jpg)|*.jpg|Png (*.png)|*.png |All files(*.*)|*.*";
                        openFileDialog.Multiselect = true;

                        if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                        if (BufferToSave != null) BufferToSave = null;

                        BufferToSave = GetByteImages(openFileDialog.FileNames);
                        Images = new ObservableCollection<Sql_Image>(GetImages(BufferToSave));
                    }
                });
            }

        }

        private byte[] BufferToSave;

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


    }
}
