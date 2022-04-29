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

namespace WPF_Main.ViewModel
{
    public class InstructionAdderViewModel : ViewModelBase
    {
        private int _countPages = 1;
        public int CountPages
        {
            get => _countPages;

            set
            {
                if (_countPages == value) return;

                _countPages = value;

                RaisePropertyChanged(nameof(CountPages));
            }
        }

        private string _nameContentPage;
        public string NameContentPage
        {
            get => _nameContentPage;

            set
            {
                if(value == _nameContentPage) return;

                _nameContentPage = value;

                RaisePropertyChanged(nameof(NameContentPage));
            }
        }

        private Sql_Image _sql_image = new Sql_Image();
        public Sql_Image CurrentImage
        {
            get => _sql_image;

            set
            {
                if(_sql_image.Data == value.Data) return;

                _sql_image = value;

                RaisePropertyChanged(nameof(CurrentImage));

            }
        }

        private string _description;
        public string Description
        {
            get => _description;

            set
            {
                if (_description == value) return;

                _description = value;

                RaisePropertyChanged(nameof(Description));
            }
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

                    CurrentImage = ByteOperation.GetImages(ByteOperation.GetByteImage(openFileDialog.FileName)).First();
                }
            });
        }

        public RelayCommand LoadImageFromBuffer
        {
            get => new RelayCommand(() =>
            {

            });
        }

        public RelayCommand SaveInstruction
        {
            get => new RelayCommand(() =>
            {

            });
        }

        public RelayCommand NotSaveAndClose
        {
            get => new RelayCommand(() =>
            {

            });
        }

        public RelayCommand BackPage
        {
            get => new RelayCommand(() =>
            {

            });
        }

        public RelayCommand NextPage
        {
            get => new RelayCommand(() =>
            {

            });
        }


        public InstructionAdderViewModel()
        {

        }

    }
}
