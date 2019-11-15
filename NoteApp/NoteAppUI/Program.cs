using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoteApp;

namespace NoteAppUI
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Project pr = new Project();
            Note sd = new Note();
            sd.Name = "Перваяя заметка";
            sd.NoteText = "Текст первой заметки";
            pr.Note.Add(sd);
            ProjectManager.SaveToFile(pr);
            Project ss = ProjectManager.LoadFromFile();
            MessageBox.Show(Convert.ToString(ss.Note[0].Name) + "\n" + 
                (Convert.ToString(ss.Note[0].CategoryNotes)) + "\n" +
                (Convert.ToString(ss.Note[0].NoteText)) + "\n" +
                (Convert.ToString(ss.Note[0].TimeOfCreation)) + "\n" +
                (Convert.ToString(ss.Note[0].LastChangeTime)));
        }
    }
}
