using System;
using NUnit.Framework;
using NoteApp;

namespace NoteApp.UnitTests
{
    [TestFixture]
    public class NoteTest
    {
        [Test(Description = "Позитивный тест геттера Name")]
        public void TestNameGet_CorrectValue()
        {
            var expected = "Название заметки";
            var note = new Note();
            note.Name = expected;
            var actual = note.Name;

            Assert.AreEqual(expected, actual, "Геттер Name возвращает неправильное название заметки");
        }

        [Test(Description = "Позитивный тест сеттера Name")]
        public void TestNameSet_CorrectValue()
        {
            var name = "Название заметки";
            var note = new Note();

            Assert.DoesNotThrow(
            () => { note.Name = name; },
            "Не должно возникать исключения"
            );
        }

        [TestCase("ааааааааааааааааааааааааааааааааааааааааааааааааааааааа",
           "Должно возникать исключение, если название заметки больше 50 символов",
           TestName = "Негативный тест сеттера Name: название заметки больше 50 символов")]
        [TestCase("", "Должно возникать исключение, если название заметки равно null",
           TestName = "Негативный тест сеттера Name: название заметки равно null")]
        public void TestNameSet_ArgumentExeption(string wrongTitle, string message)
        {
            var note = new Note();

            Assert.Throws<ArgumentException>(
            () => { note.Name = wrongTitle; },
            message);
        }

        [Test(Description = "Позитивный тест геттера CategoryNotes")]
        public void TestCategoryNotesGet_CorrectValue()
        {
            var expected = CategoryNotes.Work;
            var note = new Note();
            note.CategoryNotes = expected;
            var actual = note.CategoryNotes;

            Assert.AreEqual(expected, actual, "Геттер CategoryNotes возвращает неправильную категорию заметки");
        }

        [Test(Description = "Позитивный тест геттера NoteText")]
        public void TestNoteTextGet_CorrectValue()
        {
            var expected = "Текст заметки";
            var note = new Note();
            note.NoteText = expected;
            var actual = note.NoteText;

            Assert.AreEqual(expected, actual, "Геттер NoteText возвращает неправильный текст заметки");
        }

        [Test(Description = "Позитивный тест геттера TimeOfCreation")]
        public void TestTimeOfCreationGet_CorrectValue()
        {
            var expected = DateTime.Now;
            var note = new Note();
            note.TimeOfCreation = expected;
            var actual = note.TimeOfCreation;

            Assert.AreEqual(expected, actual, "Геттер TimeOfCreation возвращает неправильную дату");
        }

        [Test(Description = "Позитивный тест сеттера TimeOfCreation")]
        public void TestTimeOfCreationSet_CorrectValue()
        {
            var note = new Note();

            Assert.DoesNotThrow(
            () => { note.TimeOfCreation = DateTime.Now; },
            "Не должно возникать исключения"
            );
        }

        [Test(Description = "Негативный тест сеттера TimeOfCreation: дата создания позже текущей даты")]
        public void TestTimeOfCreationSet_LongerCurrentDate()
        {
            var time = DateTime.Now;
            time = time.AddDays(1);
            var note = new Note();
            Assert.Throws<ArgumentException>(
            () => { note.TimeOfCreation = time; },
            "Должно возникать исключение, если дата создания позже текущей");
        }

        [Test(Description = "Позитивный тест геттера LastChangeTime")]
        public void TestLastChangeTimeGet_CorrectValue()
        {
            var expected = DateTime.Now;
            var note = new Note();
            note.LastChangeTime = expected;
            var actual = note.LastChangeTime;

            Assert.AreEqual(expected, actual, "Геттер LastChangeTime возвращает неправильную дату");
        }

        [Test(Description = "Позитивный тест сеттера LastChangeTime")]
        public void TestLastChangeTimeSet_CorrectValue()
        {
            var note = new Note();

            Assert.DoesNotThrow(
            () => { note.LastChangeTime = DateTime.Now; },
            "Не должно возникать исключения"
            );
        }

        [Test(Description = "Негативный тест сеттера LastChangeTime: дата последнего изменения позже текущей даты")]
        public void TestLastChangeTimeSet_LongerCurrentDate()
        {
            var time = DateTime.Now;
            time = time.AddDays(1);
            var note = new Note();
            Assert.Throws<ArgumentException>(
            () => { note.LastChangeTime = time; },
            "Должно возникать исключение, если дата последнего изменения позже текущей даты");
        }

        [Test(Description = "Негативный тест сеттера LastChangeTime: дата последнего изменения ранее даты создания")]
        public void TestDateOfChangeSet_LessCreatedDate()
        {
            var time = DateTime.Now;
            time = time.AddDays(-1);
            var note = new Note();
            Assert.Throws<ArgumentException>(
            () => { note.LastChangeTime = time; },
            "Должно возникать исключение, если дата последнего изменения ранее даты создания");
        }

        [Test(Description = "Тест Clone")]
        public void TestClone()
        {
            var expected = new Note();
            expected.Name = "Название заметки";
            expected.CategoryNotes = CategoryNotes.Work;
            expected.NoteText = "Текст заметки";
            expected.TimeOfCreation = DateTime.Now;
            expected.LastChangeTime = DateTime.Now;
            Note clone = (Note)expected.Clone();
            var actual = clone;

            Assert.AreEqual(expected, actual);

        }
    }
}
