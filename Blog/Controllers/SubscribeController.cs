using Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubscribeController : ControllerBase
    {
        [HttpPost]
        public IActionResult Subscribe(string login,string tosub)
        {
            using (var db = new DataBaseContext())
            {

               if(db.Subs.Any(x => x.Login == login && x.ToSub == tosub) || login == null)
                    return BadRequest();

                var AddInTable = new Subs
                {
                    Login = login,
                    ToSub = tosub,
                };

                db.Subs.Add(AddInTable);
                db.SaveChanges();
                return Ok();

            }

        }

        [HttpDelete]
        public IActionResult UnSubscribe(string login, string unsub)
        {
            using (var db = new DataBaseContext())
            {

                var UnSub = db.Subs.FirstOrDefault(e => e.Login == login && e.ToSub == unsub);
                db.Subs.Remove(UnSub);
                db.SaveChanges();
                return Ok();

            }

        }

        [HttpGet]
        [Route("viewsubscribes")]
        public IActionResult ViewSubscribe(string login)
        {
            using (var db = new DataBaseContext())
            {
                if (login == null)
                    return BadRequest();

                var subList = db.Subs.Where(x => x.ToSub == login).ToList();
                List<string>newlist = new List<string>();

                foreach (var subs in subList)
                {
                    newlist.Add(subs.Login);
                }
                
                return Ok(new SubArray { Subs = newlist, Count = subList.Count() });

            }

        }

        [HttpGet]
        [Route("viewtosubscribe")]
        public IActionResult ViewToSubscribe(string login)
        {
            using (var db = new DataBaseContext())
            {
                var subList = db.Subs.Where(x => x.Login == login).ToList();
                List<string> logins = new List<string>();
                foreach (var sub in subList)
                {
                    logins.Add(sub.ToSub);
                }

                return Ok(logins);

            }

        }

    }
}
