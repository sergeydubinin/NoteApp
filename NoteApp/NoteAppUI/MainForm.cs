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
        private Project _noteList = new Project();
        ProjectManager _projectManager = new ProjectManager();
        Note _note = new Note();

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Заполнить список контактов. Если в списке уже есть данные (список ранее был заполнен),
        /// то список будет очищен и снова заполнен.
        /// </summary>
        /// <param name="_note">Список контактов</param>
        public void FillListView(List<Note> _note)
        {
            if (NoteList.Items.Count > 0) NoteList.Items.Clear();
            foreach (Note note in _note)
            {
                AddNewNote(note);
            }
        }

        /// <summary>
        /// Добавить нового контакта
        /// </summary>
        /// <param name="Note">Контакт</param>
        public void AddNewNote(Note note)
        {
            int index = NoteList.Items.Add(note.Name).Index;
            NoteList.Items[index].Tag = note; //свойство Tag теперь ссылается на клиента, пригодится при удалении из списка и редактировании
        }

        private void NoteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NoteList.SelectedItems.Count != 0)
            {
                CategoryLabel.Text = _noteList.Note[NoteList.SelectedIndices[0]].CategoryNotes.ToString();
                TextBox.Text = _noteList.Note[NoteList.SelectedIndices[0]].NoteText;
                CreateTimePicker.Value = _noteList.Note[NoteList.SelectedIndices[0]].TimeOfCreation;
                ChangeTimePicker.Value = _noteList.Note[NoteList.SelectedIndices[0]].LastChangeTime;
            }
            else
            {
                CategoryLabel.Text = string.Empty;
                TextBox.Text = string.Empty;
                CreateTimePicker.Value = new DateTime(2000, 01, 01);
                ChangeTimePicker.Value = new DateTime(2000, 01, 01);
            }

        }

        private void AddNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddAndEditNoteForm AddNote = new AddAndEditNoteForm();
            if (AddNote.ShowDialog() == DialogResult.OK)
            {
                _noteList.Note.Add(AddNote._noteContainer);
                FillListView(_noteList.Note);
                SaveToFile(_noteList);

            }
        }

        private void AddNoteButton_Click(object sender, EventArgs e)
        {
            AddNoteToolStripMenuItem_Click(sender, e);
        }

        private void SaveToFile(Project noteList)
        {
            _projectManager.SaveToFile(noteList);
        }

        private void EditNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddAndEditNoteForm EditForm = new AddAndEditNoteForm();
            int EditInd = NoteList.SelectedIndices[0];
            EditForm.NoteView(_noteList.Note[EditInd]);
            if (EditForm.ShowDialog() == DialogResult.OK)
            {
                _noteList.Note.RemoveAt(EditInd);
                NoteList.Items[EditInd].Remove();
                _noteList.Note.Insert(EditInd, EditForm._noteContainer);
                FillListView(_noteList.Note);
                SaveToFile(_noteList);
            }
        }

        private void RemoveNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int RemInd = NoteList.SelectedIndices[0];
            _noteList.Note.RemoveAt(RemInd);
            NoteList.Items[RemInd].Remove();
            SaveToFile(_noteList);
        }

        private void EditNoteButton_Click(object sender, EventArgs e)
        {
            EditNoteToolStripMenuItem_Click(sender, e);
        }

        private void RemoveNoteButton_Click(object sender, EventArgs e)
        {
            RemoveNoteToolStripMenuItem_Click(sender, e);
        }
    }
}