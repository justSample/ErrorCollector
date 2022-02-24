using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WPF_Main.Models
{
    public partial class Instructions
    {
        public Instructions()
        {
            Errors = new HashSet<Errors>();
            Steps = new HashSet<Steps>();
        }

        public int Id { get; set; }
        public int IdUserCreated { get; set; }
        public string Name { get; set; }

        public virtual Users IdUserCreatedNavigation { get; set; }
        public virtual ICollection<Errors> Errors { get; set; }
        public virtual ICollection<Steps> Steps { get; set; }
    }
}
