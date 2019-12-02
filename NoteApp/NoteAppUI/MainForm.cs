using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NoteApp;

namespace NoteAppUI
{
    /// <summary>
    /// Главная форма приложения
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Создание объекта класса
        /// </summary>
        private Project noteList = new Project();

        /// <summary>
        /// Конструктор
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            NoteApp_Load();
        }

        /// <summary>
        /// Заполнение списка заметок
        /// </summary>
        public void FillListView(Project project)
        {
            if (NoteList.Items.Count > 0) NoteList.Items.Clear();
            foreach (Note note in project.Note)
            {
                NoteList.Items.Add(note.Name);
            }
        }

        /// <summary>
        /// Отображение выбранной заметки
        /// </summary>
        private void NoteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NoteList.SelectedItems.Count != 0)
            {
                NameLabel.Text = noteList.Note[NoteList.SelectedIndices[0]].Name;
                CategoryLabel.Text = noteList.Note[NoteList.SelectedIndices[0]].CategoryNotes.ToString();
                TextBox.Text = noteList.Note[NoteList.SelectedIndices[0]].NoteText;
                CreateTimePicker.Value = noteList.Note[NoteList.SelectedIndices[0]].TimeOfCreation;
                ChangeTimePicker.Value = noteList.Note[NoteList.SelectedIndices[0]].LastChangeTime;
            }
            else
            {
                NameLabel.Text = string.Empty;
                CategoryLabel.Text = string.Empty;
                TextBox.Text = string.Empty;
                CreateTimePicker.Value = DateTime.Now;
                ChangeTimePicker.Value = DateTime.Now;
            }

        }

        /// <summary>
        /// Метод добавления заметки
        /// </summary>
        public void AddNote()
        {
            NoteForm AddNote = new NoteForm();
            AddNote.CurrentNote = new Note();
            if (AddNote.ShowDialog() == DialogResult.OK)
            {
                noteList.Note.Add(AddNote.CurrentNote);
                FillListView(noteList);
                ProjectSave();
            }
        }

        /// <summary>
        /// Добавление заметки через меню
        /// </summary>
        private void AddNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNote();
        }

        /// <summary>
        /// Добавление заметки с помощью кнопки
        /// </summary>
        private void AddNoteButton_Click(object sender, EventArgs e)
        {
            AddNote();
        }

        /// <summary>
        /// Метод редактирования заметки
        /// </summary>
        public void EditNote()
        {
            if (NoteList.SelectedIndices.Count != 0)
            {
                NoteForm EditForm = new NoteForm();
                int EditInd = NoteList.SelectedIndices[0];
                var note = noteList.Note[EditInd];
                EditForm.CurrentNote = note;
                if (EditForm.ShowDialog() == DialogResult.OK)
                {
                    noteList.Note.RemoveAt(EditInd);
                    NoteList.Items[EditInd].Remove();
                    noteList.Note.Insert(EditInd, EditForm.CurrentNote);
                    ProjectSave();
                    FillListView(noteList);
                }
            }
        }

        /// <summary>
        /// Редактирование заметки через меню
        /// </summary>
        private void EditNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditNote();
        }

        /// <summary>
        /// Редактирование заметки с помощью кнопки
        /// </summary>
        private void EditNoteButton_Click(object sender, EventArgs e)
        {
            EditNote();
        }

        /// <summary>
        /// Метод удаления заметки
        /// </summary>
        public void RemoveNote()
        {
            if (NoteList.SelectedIndices.Count != 0)
            {
                int RemInd = NoteList.SelectedIndices[0];
                noteList.Note.RemoveAt(RemInd);
                NoteList.Items[RemInd].Remove();
                ProjectSave();
            }
        }
        /// <summary>
        /// Удаление заметки через меню
        /// </summary>
        private void RemoveNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveNote();
        }

        /// <summary>
        /// Удаление заметки с помощью кнопки
        /// </summary>
        private void RemoveNoteButton_Click(object sender, EventArgs e)
        {
            RemoveNote();
        }

        /// <summary>
        /// Выгрузка заметок из файла
        /// </summary>
        private void NoteApp_Load()
        {
            const string name = @"\NoteApp.notes";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string file = path + name;
            noteList = ProjectManager.LoadFromFile(file);
            FillListView(noteList);
        }

        /// <summary>
        /// Вызов окна About
        /// </summary>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form About = new AboutForm();
            About.ShowDialog();
        }

        /// <summary>
        /// Выход из программы через меню
        /// </summary>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectSave();
            this.Close();
        }

        /// <summary>
        /// Сохранение данных при закрытии программы через крестик
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProjectSave();
        }

        /// <summary>
        /// Метод сохранения заметок в файл
        /// </summary>
        private void ProjectSave()
        {
            const string name = @"\NoteApp.notes";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string file = path + name;
            ProjectManager.SaveToFile(noteList, file);
        }
    }
}