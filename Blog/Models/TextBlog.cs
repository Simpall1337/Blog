using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class TextBlog
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Login")]
        public Users User { get; set; }
    }
}
