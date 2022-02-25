using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WPF_Main.Models
{
    public partial class Error
    {
        public int Id { get; set; }
        public int IdProgram { get; set; }
        public int IdUserCreated { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public byte[] Images { get; set; }

        public virtual Program IdProgramNavigation { get; set; }
        public virtual User IdUserCreatedNavigation { get; set; }
    }
}
