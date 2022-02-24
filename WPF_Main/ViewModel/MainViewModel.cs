using GalaSoft.MvvmLight;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
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

        public Users User { get; set; }

        public ObservableCollection<Programs> Programs { get; set; }

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
    }
}