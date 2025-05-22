using System.Text.Json.Serialization;

namespace ToDoListAPI.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }

        [JsonConverter(typeof(DateOnlyConverter))]
        public DateTime Created { get; set; }

    }
}