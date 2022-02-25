using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WPF_Main.Models
{
    public partial class User
    {
        public User()
        {
            Errors = new HashSet<Error>();
            Instructions = new HashSet<Instruction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Error> Errors { get; set; }
        public virtual ICollection<Instruction> Instructions { get; set; }
    }
}
