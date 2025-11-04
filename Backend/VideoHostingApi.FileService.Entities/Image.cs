using VideoHostingApi.Common.Entities;

namespace VideoHostingApi.FileService.Entities
{
    /// <summary>
    /// Модель изображения
    /// </summary>
    public class Image : EntityBase
    {
        /// <summary>
        /// Название изображения
        /// </summary>
        public string Name { get; set; } = string.Empty;
    
        /// <summary>
        /// Mime type
        /// </summary>
        public string ContentType { get; set; } = string.Empty;
    
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }
    
        /// <summary>
        /// Дата загрузки
        /// </summary>
        public DateTime UploadedAt { get; set; }
    }

}