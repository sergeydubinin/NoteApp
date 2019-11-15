using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    /// <summary>
    /// Класс, содержащий список всех заметок, 
    /// созданных в приложении
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Список заметок
        /// </summary>
        private List<Note> _note;

        /// <summary>
        /// Возвращает и задает список всех заметок
        /// </summary>
        public List<Note> Note
        {
            get
            {
                return _note;
            }

            set
            {
                _note = value;
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Project()
        {
            Note = new List<Note>();
        }
    }
}
