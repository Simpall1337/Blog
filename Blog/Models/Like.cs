using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int IdLike { get; set; }
        public string Login { get; set; }

        [ForeignKey("Id")]
        public TextBlog Text { get; set; }
    }
}
