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
    public partial class AddAndEditNoteForm : Form
    {
        private Note _note = new Note();
        public Note _noteContainer => _note;

        public AddAndEditNoteForm()
        {
            InitializeComponent();
            CategoryComboBox.Items.Add(CategoryNotes.Work);
            CategoryComboBox.Items.Add(CategoryNotes.Home);
            CategoryComboBox.Items.Add(CategoryNotes.HealthAndSport);
            CategoryComboBox.Items.Add(CategoryNotes.People);
            CategoryComboBox.Items.Add(CategoryNotes.Documents);
            CategoryComboBox.Items.Add(CategoryNotes.Finance);
            CategoryComboBox.Items.Add(CategoryNotes.Different);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
            _note.Name = TitleTextBox.Text;
            _note.CategoryNotes = (CategoryNotes)CategoryComboBox.SelectedItem;
            _note.NoteText = ContentTextBox.Text;
            _note.LastChangeTime = ModifiedDatePicker.Value;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
