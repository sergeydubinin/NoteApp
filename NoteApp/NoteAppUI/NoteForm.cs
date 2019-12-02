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
    /// <summary>
    /// Форма добавления и редактирования заметки
    /// </summary>
    public partial class NoteForm : Form
    {
        /// <summary>
        /// Текущая заметка
        /// </summary>
        private Note _currentNote = new Note();

        /// <summary>
        /// Возвращает и устанавливает значения для текущей заметки
        /// </summary>
        public Note CurrentNote
        {
            get
            {
                _currentNote.Name = TitleTextBox.Text;
                _currentNote.CategoryNotes = (CategoryNotes)CategoryComboBox.SelectedItem;
                _currentNote.NoteText = ContentTextBox.Text;
                _currentNote.LastChangeTime = ModifiedDatePicker.Value;
                _currentNote.TimeOfCreation = CreatedTimePicker.Value;
                return _currentNote;
            }

            set
            {
                _currentNote = value;
                if (value != null)
                {
                    TitleTextBox.Text = value.Name;
                    CategoryComboBox.Text = value.CategoryNotes.ToString();
                    ContentTextBox.Text = value.NoteText;
                    CreatedTimePicker.Value = value.TimeOfCreation;
                    ModifiedDatePicker.Value = DateTime.Now;
                }
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public NoteForm()
        {
            InitializeComponent();
            FillCategoryItems();
        }

        /// <summary>
        /// Зполняет категории заметки
        /// </summary>
        public void FillCategoryItems()
        {
            var values = Enum.GetValues(typeof(CategoryNotes));
            foreach (var value in values)
            {
                CategoryComboBox.Items.Add(value);
            }
            CategoryComboBox.SelectedIndex = 6;
        }

        /// <summary>
        /// Обработчик кнопки Ok
        /// </summary>
        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Обработчик кнопки Cancle
        /// </summary>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Проверка на ввод некорректных значений
        /// </summary>
        private void TitleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (TitleTextBox.Text.Length > 50 | string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                TitleTextBox.BackColor = Color.LightSalmon;
            }
            else
            {
                TitleTextBox.BackColor = Color.White;
            }
        }
    }
}
