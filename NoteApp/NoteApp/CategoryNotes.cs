using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    /// <summary>
    /// Перечисление, содержащее названия 
    /// категорий заметок
    /// </summary>
    public enum CategoryNotes
    {
        /// <summary>
        /// Работа
        /// </summary>
        Work = 1,

        /// <summary>
        /// Дом
        /// </summary>
        Home,

        /// <summary>
        /// Здоровье и Спорт
        /// </summary>
        HealthAndSport,

        /// <summary>
        /// Люди
        /// </summary>
        People,

        /// <summary>
        /// Документы
        /// </summary>
        Documents,

        /// <summary>
        /// Финансы
        /// </summary>
        Finance,

        /// <summary>
        /// Разное
        /// </summary>
        Different
    }
}
