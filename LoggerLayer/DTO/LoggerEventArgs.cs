using System;

namespace LoggerLayer.DTO
{
    /// <summary>
    /// Информация о событии 
    /// </summary>
    public record LoggerEventArgs
    {
        /// <summary>
        /// Имя работника
        /// </summary>
        public string? NameWorker { get; init; }
        /// <summary>
        /// Описание операции
        /// </summary>
        public string? TypeOperation { get; init; }
        /// <summary>
        /// Дата создания события
        /// </summary>
        public DateTime DateOperation { get; init; }
    }
}
