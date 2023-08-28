﻿using Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Blog.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpPost]
        [Route("createblog")]
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

        [HttpGet]
        [Route("deleteblog")]
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
        [Route("viewallblogs")]
        public IActionResult ViewAllBlogs(string login)
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
        [Route("viewsubblogs")]
        public IActionResult ViewSubBlogs(string login)
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
               

                return Ok(blogs);
            }

        }
    }
}
