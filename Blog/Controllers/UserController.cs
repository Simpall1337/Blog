using Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("createacc")]
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

        [HttpPost]
        [Route("loginacc")]
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

        //[HttpGet]
        //[Route("deleteaccount")]
        //public IActionResult DeleteAccount()
        //{

        //}

    }

  
}
