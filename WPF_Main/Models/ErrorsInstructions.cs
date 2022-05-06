using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WPF_Main.Models
{
    public partial class ErrorsInstructions
    {
        public int Id { get; set; }
        public int IdError { get; set; }
        public int IdInstruction { get; set; }

        public virtual Errors IdErrorNavigation { get; set; }
        public virtual Instructions IdInstructionNavigation { get; set; }
    }
}
