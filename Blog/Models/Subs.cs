using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class Subs
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string ToSub { get; set; }

        [ForeignKey("Login")]
        public Users User { get; set; }




    }
}
