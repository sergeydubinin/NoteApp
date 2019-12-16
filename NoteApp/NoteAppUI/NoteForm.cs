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
        private Note _selectedNote = new Note();

        /// <summary>
        /// Возвращает и устанавливает значения для текущей заметки
        /// </summary>
        public Note SelectedNote
        {
            get
            {
                _selectedNote.Name = TitleTextBox.Text;
                _selectedNote.CategoryNotes = (CategoryNotes)CategoryComboBox.SelectedItem;
                _selectedNote.NoteText = ContentTextBox.Text;
                _selectedNote.LastChangeTime = ModifiedDatePicker.Value;
                _selectedNote.TimeOfCreation = CreatedTimePicker.Value;
                return _selectedNote;
            }

            set
            {
                _selectedNote = value;
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
        /// Заполнение категорий заметок
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
        /// Проверка на ввод некорректного названия заметки
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

        /// <summary>
        /// Отображение всплывающих подсказок при вводе некорректного названия заметки
        /// </summary>
        private void TitleTextBox_MouseEnter(object sender, EventArgs e)
        {
            if (TitleTextBox.Text.Length > 50)
            {
                toolTip.SetToolTip(TitleTextBox, "Название заметки превышает 50 символов");
                toolTip.IsBalloon = true;
            }
            else if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                toolTip.SetToolTip(TitleTextBox, "Не введено название заметки");
                toolTip.IsBalloon = true;
            }
            else
            {
                toolTip.RemoveAll();
                toolTip.IsBalloon = false;
            }
        }
    }
}
