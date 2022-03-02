using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Main.Utils
{
    public class Buffer 
    {

        private byte[] _data;
        public byte[] Data
        {
            get
            {
                return _data;
            }
            set
            {

               _data = value;
            }
        }



    }
}
