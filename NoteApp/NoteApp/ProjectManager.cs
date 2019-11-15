using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace NoteApp
{
    /// <summary>
    /// Класс, реализующий сохранение объекта "Проект" в файл
    /// и загрузку проекта из файла
    /// </summary>
    public static class ProjectManager
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        private const string _name = @"\NoteApp.notes";

        /// <summary>
        /// Путь к папке документов
        /// </summary>
        private static readonly string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        /// <summary>
        /// Полный путь файла
        /// </summary>
        private static readonly string _file = _path + _name;

        /// <summary>
        /// Метод для сохранения объекта "Проект" в файл
        /// </summary>
        /// <param name="data">Данные для сохранения</param>
        public static void SaveToFile(Project data)
        {
            //Создаём экземпляр сериализатора
            JsonSerializer serializer = new JsonSerializer();

            //Открываем поток для записи в файл с указанием пути
            using (StreamWriter sw = new StreamWriter(_file))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                //Вызываем сериализацию и передаем объект, который хотим сериализовать
                serializer.Serialize(writer, data);
            }
        }

        /// <summary>
        /// Метод загрузки проекта из файла
        /// </summary>
        /// <returns>Возвращает данные из файла</returns>
        public static Project LoadFromFile()
        {
            //Создаём переменную, в которую поместим результат десериализации
            Project project = null;

            //Создаём экземпляр сериализатора
            JsonSerializer serializer = new JsonSerializer();

            //Открываем поток для чтения из файла с указанием пути
            using (var sr = new StreamReader(_file))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                //Вызываем десериализацию и явно преобразуем результат в целевой тип данных
                project = serializer.Deserialize<Project>(reader);
            }
            return project;
        }
    }
}
