using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoteApp;

namespace NoteAppUI
{
    public partial class MainForm : Form
    {
        private Project noteList = new Project();
        ProjectManager projectManager = new ProjectManager();

        public MainForm()
        {
            InitializeComponent();
            NoteApp_Load();
        }

        /// <summary>
        /// Заполнение списка заметок
        /// </summary>
        /// <param name="_note">Список заметок</param>
        public void FillListView(List<Note> _note)
        {
            //Если в списке уже есть данные, то список будет очищен и снова заполнен
            if (NoteList.Items.Count > 0) NoteList.Items.Clear();
            foreach (Note note in _note)
            {
                AddNewNote(note);
            }
        }

        /// <summary>
        /// Добавление заметки в ListView
        /// </summary>
        public void AddNewNote(Note note)
        {
            int index = NoteList.Items.Add(note.Name).Index;
            NoteList.Items[index].Tag = note;
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
        /// Добавление заметки (через меню)
        /// </summary>
        private void AddNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddAndEditNoteForm AddNote = new AddAndEditNoteForm();
            AddNote.CurrentNote = new Note();
            if (AddNote.ShowDialog() == DialogResult.OK)
            {
                noteList.Note.Add(AddNote.CurrentNote);
                FillListView(noteList.Note);
                SaveToFile(noteList);
            }
        }

        /// <summary>
        /// Добавление заметки (с помощью кнопки)
        /// </summary>
        private void AddNoteButton_Click(object sender, EventArgs e)
        {
            AddNoteToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Сохранение заметок в файл
        /// </summary>
        /// <param name="noteList"></param>
        private void SaveToFile(Project noteList)
        {
            projectManager.SaveToFile(noteList);
        }

        /// <summary>
        /// Редактирование заметки (через меню)
        /// </summary>
        private void EditNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NoteList.SelectedIndices.Count != 0)
            {
                AddAndEditNoteForm EditForm = new AddAndEditNoteForm();
                int EditInd = NoteList.SelectedIndices[0];
                var note = noteList.Note[EditInd];
                EditForm.CurrentNote = note;
                if (EditForm.ShowDialog() == DialogResult.OK)
                {
                    noteList.Note.RemoveAt(EditInd);
                    NoteList.Items[EditInd].Remove();
                    noteList.Note.Insert(EditInd, EditForm.CurrentNote);
                    SaveToFile(noteList);
                    FillListView(noteList.Note);
                }
            }
        }

        /// <summary>
        /// Удаление заметки (через меню)
        /// </summary>
        private void RemoveNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NoteList.SelectedIndices.Count != 0)
            {
                int RemInd = NoteList.SelectedIndices[0];
                noteList.Note.RemoveAt(RemInd);
                NoteList.Items[RemInd].Remove();
                SaveToFile(noteList);
            }
        }

        /// <summary>
        /// Редактирование заметки (с помощью кнопки)
        /// </summary>
        private void EditNoteButton_Click(object sender, EventArgs e)
        {
            EditNoteToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Удаление заметки (с помощью кнопки)
        /// </summary>
        private void RemoveNoteButton_Click(object sender, EventArgs e)
        {
            RemoveNoteToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Выгрузка заметок из файла
        /// </summary>
        private void NoteApp_Load()
        {
            noteList = ProjectManager.LoadFromFile();
            FillListView(noteList.Note);
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
        /// Закрытие главного окна
        /// </summary>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToFile(noteList);
            this.Close();
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}