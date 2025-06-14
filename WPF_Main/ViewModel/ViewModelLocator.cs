/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WPF_Main"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace WPF_Main.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ErrorViewModel>();
            SimpleIoc.Default.Register<InstructionAdderViewModel>();
            SimpleIoc.Default.Register<InstructionViewerViewModel>();
            SimpleIoc.Default.Register<InstructionBindingViewModel>();
            SimpleIoc.Default.Register<ProgramAdderViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public ErrorViewModel ErrorMain
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ErrorViewModel>();
            }
        }

        public InstructionAdderViewModel InstructionAdder
        {
            get
            {
                return ServiceLocator.Current.GetInstance<InstructionAdderViewModel>();
            }
        }

        public InstructionViewerViewModel InstructionViewer
        {
            get
            {
                return ServiceLocator.Current.GetInstance<InstructionViewerViewModel>();
            }
        }

        public InstructionBindingViewModel InstructionBinding
        {
            get
            {
                return ServiceLocator.Current.GetInstance<InstructionBindingViewModel>();
            }
        }

        public ProgramAdderViewModel ProgramAdder
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProgramAdderViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}