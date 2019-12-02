using System;
using System.Collections.Generic;
using NUnit.Framework;
using NoteApp;
using System.IO;

namespace NoteApp.UnitTests
{
    [TestFixture]
    internal class ProjectManagerTest
    {
        [Test(Description = "Тест сериализации")]
        public void TestSaveToFile_CorrectValue()
        {
            var list = new List<Note>();
            var note1 = new Note();
            note1.Name = "Название заметки 1";
            note1.CategoryNotes = CategoryNotes.Work;
            note1.NoteText = "Текст заметки 1";
            note1.TimeOfCreation = DateTime.Now;
            note1.LastChangeTime = DateTime.Now;
            list.Add(note1);

            var note2 = new Note();
            note2.Name = "Название заметки 2";
            note2.CategoryNotes = CategoryNotes.Different;
            note2.NoteText = "Текст заметки 2";
            note2.TimeOfCreation = DateTime.Now;
            note2.LastChangeTime = DateTime.Now;
            list.Add(note2);

            var project = new Project();
            project.Note.Add(note1);
            project.Note.Add(note2);

            ProjectManager.SaveToFile(project);

            var expected = File.ReadAllText(@"expected.json");
            var actual = File.ReadAllText(@"example.json");

            Assert.AreEqual(actual, expected,
                "");
        }

        
    }
}
