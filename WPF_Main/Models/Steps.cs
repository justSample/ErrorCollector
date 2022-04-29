using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WPF_Main.Models
{
    public partial class Steps
    {
        public int Id { get; set; }
        public int IdInstructions { get; set; }
        public int Number { get; set; }
        public string Header { get; set; }
        public string ActionDescription { get; set; }
        public byte[] Images { get; set; }

        public virtual Instructions IdInstructionsNavigation { get; set; }
    }
}
