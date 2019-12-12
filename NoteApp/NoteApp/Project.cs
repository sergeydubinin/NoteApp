using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<int> RealIndexes = new List<int>();

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

        public List<Note> SortWithSelectionCategory(int category)
        {
            var sortNotes = new List<Note>();

            SortNotes(sortNotes);

            //если выбрана категория All
            if (category == 0)
            {
                RealIndexes.Clear();

                for (int i = 0; i < Note.Count; i++)
                {
                    sortNotes.Add(Note[i]);
                    RealIndexes.Add(i);
                }
            }
            //если другая категория
            else
            {
                RealIndexes.Clear();

                for (int i = 0; i < Note.Count; i++)
                {
                    if ((int)Note[i].CategoryNotes == category - 1)
                    {
                        sortNotes.Add(Note[i]);
                        RealIndexes.Add(i);
                    }
                }
            }
            return sortNotes;
        }
    }
}
