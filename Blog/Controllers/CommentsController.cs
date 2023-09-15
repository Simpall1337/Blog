using Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateComments(int id, string login, string comment)
        {
            using (var db = new DataBaseContext())
            {
                try
                {
                    if (login == null || comment == null)
                        return BadRequest();

                    var AddInTable = new Coments
                    {
                        Login = login,
                        Comment = comment,
                        Id = id,
                    };

                    db.Coments.Add(AddInTable);
                    db.SaveChanges();
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
        }

        [HttpGet]
        public IActionResult ViewComments(int id)
        {
            using (var db = new DataBaseContext())
            {
                var list = db.Coments.Where(x => x.Id == id).ToList();
                return Ok(list);
            }
        }

        [HttpDelete]
        public IActionResult DeleteComments(int idcomment)
        {
            using (var db = new DataBaseContext())
            {
                var entityToDelete = db.Coments.FirstOrDefault(e => e.IdComment == idcomment);
                db.Coments.Remove(entityToDelete);
                db.SaveChanges();
                return Ok();
            }
        }
    }
}
