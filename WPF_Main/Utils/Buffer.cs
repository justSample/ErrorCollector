using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Main.Utils
{
    /// <summary>
    /// Нужен только для того, чтоб сохранять информацию в байтах. Если этого класса не будет, то чиститься не будет.
    /// </summary>
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
