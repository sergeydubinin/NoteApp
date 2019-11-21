using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    /// <summary>
    /// Класс, хранящий информацию о названии, категории,
    /// содержимом, времени создания и времени последнего
    /// изменения заметки
    /// </summary>
    public class Note : ICloneable
    {
        /// <summary>
        /// Название заметки
        /// </summary>
        private string _name = "Без названия";

        /// <summary>
        /// Категория заметки
        /// </summary>
        private CategoryNotes _categoryNotes;

        /// <summary>
        /// Текст заметки
        /// </summary>
        private string _noteText;

        /// <summary>
        /// Время создания заметки
        /// </summary>
        private DateTime _timeOfCreation;

        /// <summary>
        /// Время последнего изменения заметки
        /// </summary>
        private DateTime _lastChangeTime;

        /// <summary>
        /// Возвращает и задает название заметки
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                const int maxNumberSymbols = 50;
                if (value.Length > maxNumberSymbols)
                {
                    throw new ArgumentException("Название заметки должно быть не более 50 символов");
                }
                else
                {
                    _name = value;
                    _lastChangeTime = DateTime.Now;
                }
            }
        }

        /// <summary>
        /// Возвращает и задает категорию заметки
        /// </summary>
        public CategoryNotes CategoryNotes
        {
            get
            {
                return _categoryNotes;
            }

            set
            {
                _categoryNotes = value;
                _lastChangeTime = DateTime.Now;
            }
        }

        /// <summary>
        /// Возвращает и задает текст заметки
        /// </summary>
        public string NoteText
        {
            get
            {
                return _noteText;
            }

            set
            {
                _noteText = value;
                _lastChangeTime = DateTime.Now;
            }
        }

        /// <summary>
        /// Возвращает время создания заметки
        /// </summary>
        public DateTime TimeOfCreation
        {
            get
            {
                return _timeOfCreation;
            }

            set
            {
                _timeOfCreation = value;
            }
        }

        /// <summary>
        /// Возвращает и задает время последнего изменения
        /// заметки
        /// </summary>
        public DateTime LastChangeTime
        {
            get
            {
                return _lastChangeTime;
            }

            set
            {
                _lastChangeTime = value;
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Note()
        {
            _timeOfCreation = DateTime.Now;
        }

        /// <summary>
        /// Создание копии заметки
        /// </summary>
        /// <returns>Взвращает копию заметки</returns>
        public object Clone()
        {
            return new Note
            {
                Name = Name,
                CategoryNotes = CategoryNotes,
                NoteText = NoteText
            };
        }
    }
}
