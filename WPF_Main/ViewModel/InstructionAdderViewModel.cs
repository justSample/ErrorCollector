using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Main.Models;

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

        private Sql_Image _sql_image;
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
