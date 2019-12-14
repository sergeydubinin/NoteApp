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
        private Project Notes = new Project();
        private readonly Project sortNotes = new Project();

        /// <summary>
        /// Конструктор
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            NoteApp_Load();
            FillCategoryItems();
        }

        /// <summary>
        /// Заполнение списка заметок
        /// </summary>
        public void FillListView(Project project)
        {
            if (NoteList.Items.Count > 0) NoteList.Items.Clear();
            var sortNotes = project.SortNotes();
            foreach (Note note in sortNotes)
            {
                NoteList.Items.Add(note.Name);
            }
        }

        /// <summary>
        /// Отображение выбранной заметки
        /// </summary>
        private void NoteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var displayedNotes = (CategoryComboBox.Text == "All") ? Notes : sortNotes;
            if (NoteList.SelectedItems.Count != 0)
            {
                NameLabel.Text = displayedNotes.Note[NoteList.SelectedIndices[0]].Name;
                CategoryLabel.Text = displayedNotes.Note[NoteList.SelectedIndices[0]].CategoryNotes.ToString();
                TextBox.Text = displayedNotes.Note[NoteList.SelectedIndices[0]].NoteText;
                CreateTimePicker.Value = displayedNotes.Note[NoteList.SelectedIndices[0]].TimeOfCreation;
                ChangeTimePicker.Value = displayedNotes.Note[NoteList.SelectedIndices[0]].LastChangeTime;
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
                Notes.Note.Add(AddNote.CurrentNote);
                FillListView(Notes);
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
                int selectedIndex = (CategoryComboBox.Text == "All") ? NoteList.SelectedIndices[0]
                    : GetNoteIndex(Notes.Note, sortNotes.Note);
                var note = Notes.Note[selectedIndex];
                EditForm.CurrentNote = note;
                if (EditForm.ShowDialog() == DialogResult.OK)
                {
                    Notes.Note.RemoveAt(selectedIndex);
                    NoteList.Items[selectedIndex].Remove();
                    Notes.Note.Insert(selectedIndex, EditForm.CurrentNote);
                    ProjectSave();
                    FillListView(Notes);
                    FilldListCategory();
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
                int selectedIndex = (CategoryComboBox.Text == "All") ? NoteList.SelectedIndices[0]
                    : GetNoteIndex(Notes.Note, sortNotes.Note);
                Notes.Note.RemoveAt(selectedIndex);
                NoteList.Items[selectedIndex].Remove();
                ProjectSave();
                FillListView(Notes);
                FilldListCategory();
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
            Notes = ProjectManager.LoadFromFile(file);
            FillListView(Notes);
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
            ProjectManager.SaveToFile(Notes, file);
        }

        public void FillCategoryItems()
        {
            CategoryComboBox.Items.Add("All");
            foreach (CategoryNotes element in Enum.GetValues(typeof(CategoryNotes)))
            {
                CategoryComboBox.Items.Add(element);
            }
            CategoryComboBox.SelectedIndex = 0;
        }

        private int GetNoteIndex(List<Note> notes, List<Note> findedNotes)
        {
            int index = 0;

            foreach (var note in notes)
            {
                if (note == findedNotes[NoteList.SelectedIndices[0]])
                {
                    return index;
                }

                index++;
            }
            return -1;
        }

        private void FilldListCategory()
        {
            if (CategoryComboBox.Text != "All")
            {
                sortNotes.Note = Notes.FindCategory(CategoryComboBox.Text);
                FillListView(sortNotes);
            }
            else
            {
                FillListView(Notes);
            }
        }

        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilldListCategory();
        }

       
    }
}