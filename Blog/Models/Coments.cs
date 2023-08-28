using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class Coments
    {

        public int IdComment { get; set; }
        public int Id { get; set; }
        public string Login { get; set; }
        public string Comment { get; set; }

        [ForeignKey("Id")]
        public TextBlog Text { get; set; }
    }
}
