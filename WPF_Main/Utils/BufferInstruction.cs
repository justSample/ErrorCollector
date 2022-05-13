using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Main.Models;

namespace WPF_Main.Utils
{
    /// <summary>
    /// Этот класс нужен для того, чтоб было удобнее хранить информацию
    /// </summary>
    public class BufferInstruction
    {
        /// <summary>
        /// Номер страницы
        /// </summary>
        public int IndexPage { get; set; }

        /// <summary>
        /// Заголовок страницы
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Описание этапа
        /// </summary>
        public string Description { get; set; }

        public Sql_Image Image { get; set; } = new Sql_Image();

        /// <summary>
        /// Применяем в этом классе значения из Steps
        /// </summary>
        /// <param name="step"></param>
        public void SetFromStep(Steps step)
        {
            //Чел, ну тут должно быть просто
            IndexPage = step.Number;
            Header = step.Header;
            Description = step.ActionDescription;
            Image.Data = step.Images;
        }

    }
}
