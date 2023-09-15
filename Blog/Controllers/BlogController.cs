using Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Blog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateBlog(string login,string text)
        {
            using (var db = new DataBaseContext())
            {
                try
                {
                    if (login == null || text == null)
                        return BadRequest();

                    var AddInTable = new TextBlog
                    {
                        Login = login,
                        Text = text,
                        Date = DateTime.Now,
                    };

                    db.Text.Add(AddInTable);
                    db.SaveChanges();
                    return Ok();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("error" + ex);
                    return BadRequest();
                }
            }

        }

        [HttpDelete]
        public IActionResult DeleteBlog(int id)
        {
            using (var db = new DataBaseContext())
            {
                var entityToDelete = db.Text.FirstOrDefault(e => e.Id == id);
                db.Text.Remove(entityToDelete);
                db.SaveChanges();
                return Ok();
            }
        }

        [HttpGet]
        [Route("viewuserallblogs")]
        public IActionResult ViewUserAllBlogs(string login)
        {
            using (var db = new DataBaseContext())
            {
                
                var list = db.Text.Where(x => x.Login == login).ToList();

                if (list.Count() == 0)
                   return BadRequest();

                return Ok(list);
            }

        }
        [HttpGet]
        [Route("viewallblogs")]
        public IActionResult ViewAllBlogs(int page)
        {
            using (var db = new DataBaseContext())
            {
                int pageSize = 5;
                int startIndex = page * pageSize - pageSize;
                int endIndex = startIndex + pageSize;

                return Ok(db.Text.Skip(startIndex).Take(pageSize).ToList());
               
            }

        }

        [HttpGet]
        [Route("viewsubblogs")]
        public IActionResult ViewSubBlogs(string login,int page)
        {
            using (var db = new DataBaseContext())
            {
                var subList = db.Subs.Where(x => x.Login == login).ToList();
                List<string> logins = new List<string>();
                foreach (var sub in subList)
                {
                   logins.Add(sub.ToSub);
                }
                List<BlogSubs> blogs = new List<BlogSubs>();
                foreach (var users in logins)
                {
                    var add = db.Text.Where(x => x.Login == users).ToList();
                    foreach (var item in add)
                    {
                        var a = new BlogSubs(){ Login = item.Login, Date=item.Date,Text= item.Text };
                        blogs.Add(a);
                    }
                }
                int pageSize = 5;
                int startIndex = page * pageSize - pageSize;
                int endIndex = startIndex + pageSize;

                List<BlogSubs> list = new List<BlogSubs>();

                while (startIndex < endIndex)
                {
                    list.Add(blogs[startIndex]);
                    startIndex++;
                }

                return Ok(list);
            }

        }
    }
}
