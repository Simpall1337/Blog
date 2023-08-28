using Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Blog.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        [HttpGet]
        [Route("like")]
        public IActionResult PutLike(int id,string login)
        {
            using (var db = new DataBaseContext())
            {
                if (login == null)
                    return BadRequest();

                //var result = db.Like.Where(x => x.Login == login && x.Id == id).ToList();
                if(db.Like.Any(x => x.Login == login && x.Id == id))
                    return BadRequest();

                var AddInTable = new Like
                {
                    Login = login,
                    Id = id,
                };
                
                db.Like.Add(AddInTable);
                db.SaveChanges();
                return Ok();

            }

        }

        [HttpGet]
        [Route("dislike")]
        public IActionResult DisLike(int id, string login)
        {
            using (var db = new DataBaseContext())
            {
                if (login == null)
                    return BadRequest();

                var dislike = db.Like.FirstOrDefault(e => e.Id == id && e.Login == login);
                db.Like.Remove(dislike);
                db.SaveChanges();
                return Ok();

            }

        }

        [HttpGet]
        [Route("viewlikes")]
        public IActionResult ViewLikes(int id)
        {
            using (var db = new DataBaseContext())
            {
                var view = db.Like.Where(e => e.Id == id).ToList();
                return Ok(view.Count());
            }

        }

    }
}
