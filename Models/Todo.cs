using MongoDB.Bson.Serialization.Attributes;

namespace TodoList.Models
{
    public class Todo
    {
        [BsonId]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }     
    }
}