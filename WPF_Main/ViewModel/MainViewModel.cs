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

        private Programs _selectedProgram;
        public Programs SelectedProgram
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
                    Errors = new ObservableCollection<Errors>(listErrors);
                }

            }
        }

        private ObservableCollection<Errors> _errors;
        public ObservableCollection<Errors> Errors
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

        private Errors _selectedError;

        public Errors SelectedError
        {
            get
            {
                return _selectedError;
            }

            set
            {
                if (_selectedError == value || value == null) return;

                _selectedError = value;

                using (error_collectorContext context = new error_collectorContext())
                {
                    var listErrors = context.Errors.Where(e => e.Id == SelectedError.Id).ToList();

                    SelectedError.IdUserCreatedNavigation = context.Users.Where(u => u.Id == SelectedError.IdUserCreated).First();

                    byte[] data = listErrors.Select(s => s.Images).First();
                    if (data != null)
                        Images = new ObservableCollection<Sql_Image>(GetImages(data));

                }

                RaisePropertyChanged(nameof(SelectedError));

                

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
                    
                    if(result == DialogResult.No) return;

                    using(error_collectorContext context = new error_collectorContext())
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
                    
                });
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
                Programs = new ObservableCollection<Programs>(context.Programs.ToList());

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

                using (MemoryStream ms = new MemoryStream(dataImage))
                {
                    ms.Position = 0;
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);


                }

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