using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Main.Models;

namespace WPF_Main.Utils
{
    public class Instruction
    {

        public int IndexPage { get; set; }

        public string Header { get; set; }

        public string Description { get; set; }

        public Sql_Image Image { get; set; } = new Sql_Image();

    }
}
