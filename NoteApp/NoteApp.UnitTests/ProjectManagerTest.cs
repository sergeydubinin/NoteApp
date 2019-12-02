using System;
using System.Collections.Generic;
using NUnit.Framework;
using NoteApp;
using System.IO;
using Newtonsoft.Json;

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
            note1.TimeOfCreation = new DateTime(2019, 11, 5);
            note1.LastChangeTime = new DateTime(2019, 12, 1);
            list.Add(note1);

            var note2 = new Note();
            note2.Name = "Название заметки 2";
            note2.CategoryNotes = CategoryNotes.Different;
            note2.NoteText = "Текст заметки 2";
            note2.TimeOfCreation = new DateTime(2019, 12, 2);
            note2.LastChangeTime = new DateTime(2019, 12, 2);
            list.Add(note2);

            var project = new Project();
            project.Note.Add(note1);
            project.Note.Add(note2);

            ProjectManager.SaveToFile(project, @"C:\Users\serge\source\repos\NoteApp\NoteApp\NoteApp.UnitTests\expected.json");

            var expected = File.ReadAllText(@"C:\Users\serge\source\repos\NoteApp\NoteApp\NoteApp.UnitTests\expected.json");
            var actual = File.ReadAllText(@"C:\Users\serge\source\repos\NoteApp\NoteApp\NoteApp.UnitTests\example.json");

            Assert.AreEqual(actual, expected);
        }

        [Test(Description = "Тест десериализации")]
        public void TestLoadFromFile_CorrectValue()
        {
            var list = new List<Note>();
            var note1 = new Note();
            note1.Name = "Название заметки 1";
            note1.CategoryNotes = CategoryNotes.Work;
            note1.NoteText = "Текст заметки 1";
            note1.TimeOfCreation = new DateTime(2019, 11, 5);
            note1.LastChangeTime = new DateTime(2019, 12, 1);
            list.Add(note1);

            var note2 = new Note();
            note2.Name = "Название заметки 2";
            note2.CategoryNotes = CategoryNotes.Different;
            note2.NoteText = "Текст заметки 2";
            note2.TimeOfCreation = new DateTime(2019, 12, 2);
            note2.LastChangeTime = new DateTime(2019, 12, 2);
            list.Add(note2);

            var project = new Project();
            project.Note.Add(note1);
            project.Note.Add(note2);
            var actual = project.Note;

            var expected = ProjectManager.LoadFromFile(@"C:\Users\serge\source\repos\NoteApp\NoteApp\NoteApp.UnitTests\example.json");

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(actual[i].Name, expected.Note[i].Name);
                Assert.AreEqual(actual[i].CategoryNotes, expected.Note[i].CategoryNotes);
                Assert.AreEqual(actual[i].NoteText, expected.Note[i].NoteText);
                Assert.AreEqual(actual[i].TimeOfCreation, expected.Note[i].TimeOfCreation);
                Assert.AreEqual(actual[i].LastChangeTime, expected.Note[i].LastChangeTime);
            }
        }
    }
}
