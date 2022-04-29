using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPF_Main.Models;
using WPF_Main.Utils;
using WPF_Main.View;

namespace WPF_Main.Utils
{
    public class InstructionPrefab
    {

        public string Name { get; set; }

        public RelayCommand OpenInstruction
        {
            get => new RelayCommand(_showDialog);
        }

        private Action _showDialog;

        public InstructionPrefab(Instructions instruction)
        {
            Name = instruction.Name;
            _showDialog = () =>
            {
                var inst = new InstructionViewer();

                inst.SetInstruction(instruction);

                inst.ShowDialog();
            };
        }

    }
}
