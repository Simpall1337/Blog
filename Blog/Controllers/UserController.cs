using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

namespace Blog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateAccount(string login, string password) {

            using (var db = new DataBaseContext())
            {
                try
                {
                    if (login == null || password == null)
                        return BadRequest();

                    var AddInTable = new Users
                    {
                        Login = login,
                        Password = password
                    };

                    db.Users.Add(AddInTable);
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
        public IActionResult LoginAccount(string login, string password)
        {
            using (var db = new DataBaseContext())
            {

                var data = db.Users.ToList();

                foreach (var item in data)
                {
                    if (item.Login == login && item.Password == password)
                    {
                        Console.WriteLine("yes");
                        return Ok();
                    }

                }

            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult DeleteAccount(string login)
        {
            using (var db = new DataBaseContext())
            {
                var userToDelete = db.Users.FirstOrDefault(e => e.Login == login);
                db.Users.Remove(userToDelete);
                db.SaveChanges();
                return Ok();
            }

        }

        
    }
}
