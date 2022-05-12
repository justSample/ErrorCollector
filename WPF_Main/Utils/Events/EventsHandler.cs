using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Main.Utils.Events
{
    public static class EventsHandler
    {
        public static event Action OnUpdateError;
        public static event Action OnUpdateInstruction;
        public static event Action OnUpdateBindingInstruction;

        public static void RaiseUpdateBindingInstruction()
        {
            OnUpdateBindingInstruction?.Invoke();
        }

    }
}
