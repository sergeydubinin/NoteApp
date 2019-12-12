using System;
using System.Collections.Generic;
using NUnit.Framework;
using NoteApp;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;

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

            string name1 = @"\expected.json";
            string path = Assembly.GetExecutingAssembly().Location;
            string ppath = Path.GetDirectoryName(path);
            string file1 = ppath + name1;
            string name2 = @"\example.json";
            string file2 = ppath + name2;

            ProjectManager.SaveToFile(project, file1);

            var expected = File.ReadAllText(file1);
            var actual = File.ReadAllText(file2);

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

            string name = @"\example.json";
            string path = Assembly.GetExecutingAssembly().Location;
            string ppath = Path.GetDirectoryName(path);
            string file = ppath + name;


            var expected = ProjectManager.LoadFromFile(file);

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(actual[i], expected.Note[i]);
            }
        }
    }
}
