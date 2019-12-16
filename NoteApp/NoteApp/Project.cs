using System;
using System.Collections.Generic;
using NoteApp;

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
        /// Текущая заметка
        /// </summary>
        public int CurrentNote = 0;

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

        /// <summary>
        /// Метод сортировки заметок по дате изменения
        /// </summary>
        public List<Note> SortNotes(List<Note> noteList = null)
        {
            var sortingList = noteList ?? Note;
            sortingList.Sort(delegate (Note x, Note y)
            {
                if (x.LastChangeTime == null && y.LastChangeTime == null) return 0;
                else if (x.LastChangeTime == null) return 1;
                else if (y.LastChangeTime == null) return -1;
                else return y.LastChangeTime.CompareTo(x.LastChangeTime);
            });
            return sortingList;
        }

        /// <summary>
        /// Метод сортировки заметок по категориям
        /// </summary>
        public List<Note> FindCategory(string value)
        {
            List<Note> categoryList = new List<Note>();
            CategoryNotes result;
            foreach (var note in Note)
            {
                if (Enum.TryParse(value, out result) && result == note.CategoryNotes)
                {
                    categoryList.Add(note);
                }
            }

            return categoryList;
        }
    }
}
