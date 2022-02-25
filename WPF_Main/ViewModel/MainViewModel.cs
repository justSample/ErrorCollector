using GalaSoft.MvvmLight;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPF_Main.Models;

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

        public User User { get; set; }

        public ObservableCollection<Program> Programs { get; set; }

        private Program _selectedProgram;
        public Program SelectedProgram
        {
            get
            {
                return _selectedProgram;
            }

            set
            {
                if (_selectedProgram == value) return;

                _selectedProgram = value;
                RaisePropertyChanged(nameof(SelectedProgram));
                using (error_collectorContext context = new error_collectorContext())
                {
                    var listErrors = context.Errors.Where(e => e.IdProgram == _selectedProgram.Id).ToList();
                    Errors = new ObservableCollection<Error>(listErrors);
                }

            }
        }

        private ObservableCollection<Error> _errors;
        public ObservableCollection<Error> Errors
        {
            get
            {
                return _errors;
            }

            set
            {
                _errors = value;
                RaisePropertyChanged(nameof(Errors));
            }
        }

        private Error _selectedError;

        public Error SelectedError
        {
            get
            {
                return _selectedError;
            }

            set
            {
                if (_selectedError == value) return;

                _selectedError = value;

                RaisePropertyChanged(nameof(SelectedError));

                using (error_collectorContext context = new error_collectorContext())
                {
                    var listErrors = context.Errors.Where(e => e.Id == _selectedError.Id).ToList();

                    byte[] data = listErrors.Select(s => s.Images).First();
                    if (data != null)
                        Images = new ObservableCollection<Sql_Image>(GetImages(data));

                }

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
                if(_images == value) return;   
                _images = value;
                RaisePropertyChanged(nameof(Images));
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            using(error_collectorContext context = new error_collectorContext())
            {
                User = context.Users.FirstAsync().Result;
                Programs = new ObservableCollection<Program>(context.Programs.ToList());

            }
            
        }

        private Sql_Image[] GetImages(byte[] data)
        {

            if(data == null) return null;

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

                BitmapImage image = new BitmapImage();

                using (MemoryStream ms = new MemoryStream(dataImage))
                {
                    ms.Position = 0;

                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = ms;
                    image.EndInit();
                }

                _images.Add(new Sql_Image() { Data = dataImage });

            }

            return _images.ToArray();

        }


    }
}