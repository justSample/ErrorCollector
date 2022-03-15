using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Main.View
{
    /// <summary>
    /// Логика взаимодействия для ErrorAdder.xaml
    /// </summary>
    public partial class ErrorAdder : Window
    {
        private WindowState _state = WindowState.Normal;
        private double _width;
        private double _height;

        private double _defaultWidth;
        private double _defaultHeight;

        public ErrorAdder()
        {
            InitializeComponent();
            ((WPF_Main.ViewModel.ErrorViewModel)DataContext).CloseAction = new Action(this.Close);

            _defaultWidth = this.Width;
            _defaultHeight = this.Height;

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    this.DragMove();
                }
            }

            if (e.ClickCount == 2)
            {
                SetFullScreen();
            }


        }

        private void btnCloseWindow_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnFullScreen_click(object sender, RoutedEventArgs e)
        {
            SetFullScreen();
        }

        private void SetFullScreen()
        {
            switch (_state)
            {
                case WindowState.Normal:
                    _width = this.Width;
                    _height = this.Height;
                    this.Width = SystemParameters.WorkArea.Width;
                    this.Height = SystemParameters.WorkArea.Height;
                    this.Top = SystemParameters.WorkArea.Top;
                    this.Left = SystemParameters.WorkArea.Left;
                    _state = WindowState.Maximized;
                    break;
                case WindowState.Maximized:
                    this.Width = _defaultWidth;
                    this.Height = _defaultHeight;
                    CenterWindowOnScreen();
                    _state = WindowState.Normal;
                    this.WindowState = WindowState.Normal;
                    break;
            }
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void btnHideWindow_click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal || this.WindowState == WindowState.Maximized)
            {
                _state = this.WindowState;
                this.WindowState = WindowState.Minimized;
            }
            else
            {
                this.WindowState = _state;
            }
        }
    }

    
}
