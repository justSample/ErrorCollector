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

        public RelayCommand<Errors> UnBindInstruction
        {
            get => new RelayCommand<Errors>(UnBind);
        }

        private Action _showDialog;
        private int _idInstruction;

        public InstructionPrefab(Instructions instruction)
        {
            Name = instruction.Name;
            _idInstruction = instruction.Id;

            _showDialog = () =>
            {
                var inst = new InstructionViewer();

                inst.SetInstruction(instruction);

                inst.ShowDialog();
            };

        }

        private void UnBind(Errors error)
        {
            using(error_collectorContext context = new error_collectorContext())
            {
                var toDelete = context.ErrorsInstructions.Where(x => x.IdError == error.Id && x.IdInstruction == _idInstruction).Single();

                context.ErrorsInstructions.Remove(toDelete);

                context.SaveChanges();

                MsgBox.Successfully("Отвязка успешно завершена!");

                Events.EventsHandler.RaiseUpdateBindingInstruction();
            }
        }

    }
}
