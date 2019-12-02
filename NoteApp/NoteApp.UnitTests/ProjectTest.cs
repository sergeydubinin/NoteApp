using System;
using System.Collections.Generic;
using NUnit.Framework;
using NoteApp;

namespace NoteApp.UnitTests
{
    [TestFixture]
    class ProjectTest
    {
        [Test(Description = "Тест Note")]
        public void TestNoteGet_CorrectValue()
        {
            var expected = new List<Note>();
            var note = new Note();
            note.Name = "Название заметки";
            note.CategoryNotes = CategoryNotes.Work;
            note.NoteText = "Текст заметки";
            note.TimeOfCreation = DateTime.Now;
            note.LastChangeTime = DateTime.Now;
            expected.Add(note);

            var project = new Project();
            project.Note.Add(note);
            var actual = project.Note;

            Assert.AreEqual(expected[0].Name, actual[0].Name);
            Assert.AreEqual(expected[0].CategoryNotes, actual[0].CategoryNotes);
            Assert.AreEqual(expected[0].NoteText, actual[0].NoteText);
            Assert.AreEqual(expected[0].TimeOfCreation, actual[0].TimeOfCreation);
            Assert.AreEqual(expected[0].LastChangeTime, actual[0].LastChangeTime);
        }
    }
}
