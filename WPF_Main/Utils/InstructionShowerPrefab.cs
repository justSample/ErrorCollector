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
    public class InstructionShowerPrefab
    {
        //Наименование ошибки. Добавлено для название кнопки
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

        public InstructionShowerPrefab(Instructions instruction)
        {
            Name = instruction.Name;
            _idInstruction = instruction.Id;

            //Пошел на такой шаг, потому что просто через лямбду выражение он тупо не запоминал. Пришлось через делегат работать
            //Хотя может и работает, но нужно эксперементировать и узнавать информацию
            _showDialog = () =>
            {
                var inst = new InstructionViewer();

                inst.SetInstruction(instruction);

                inst.ShowDialog();
            };

        }

        /// <summary>
        /// Отвязывает от ошибки привязанную инструкцию
        /// </summary>
        /// <param name="error">Ошибка, от которой нужно отвязаться, хотя можно было передовать только id :/</param>
        private void UnBind(Errors error)
        {
            using(error_collectorContext context = new error_collectorContext())
            {
                //Ищем инструкцию, которую нужно отвязать по его id
                var toDelete = context.ErrorsInstructions.Where(x => x.IdError == error.Id && x.IdInstruction == _idInstruction).Single();

                context.ErrorsInstructions.Remove(toDelete);

                context.SaveChanges();

                Events.EventsHandler.RaiseUpdateBindingInstruction();

                MsgBox.Successfully("Отвязка успешно завершена!");

            }
        }

    }
}
