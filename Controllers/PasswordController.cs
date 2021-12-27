using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AA1Cliente;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AA1Cliente.Controllers {
    [ApiController]
    [Route("[controller]")]

    public class PasswordController : ControllerBase {

        private static List<Password> Passwords = new List<Password> {
            new Password { Id = 1, Category = "Personal", User = "UserDefault", Date = "10/10/21", Url = "www.facebook.com", TagName = "Facebook", Pass = "password1", Description = "Password for Facebook" },
            new Password { Id = 2, Category = "Personal2", User = "UserDefault2", Date = "15/05/21", Url = "www.google.com", TagName = "Google", Pass = "password2", Description = "Password for Google" },
            new Password { Id = 3, Category = "Personal2", User = "UserDefault3", Date = "05/02/20", Url = "www.google.com", TagName = "Google", Pass = "password2", Description = "Password for Google" },
            new Password { Id = 4, Category = "Personal", User = "UserDefault4", Date = "22/11/21", Url = "www.instagram.com", TagName = "Instagram", Pass = "password2", Description = "Password for Instagram" },
            new Password { Id = 5, Category = "Personal2", User = "UserDefault5", Date = "17/08/21", Url = "www.github.com", TagName = "Github", Pass = "password2", Description = "Password for Github" }


        };




        // Se me muestran todas las passwords del array Passwords
        // <summary>
        // See all the passwords
        // </summary>
        [HttpGet]
        public ActionResult<List<Password>> Get() {
            return Ok(Passwords); //200
        }

        // Informacion sobre una password en concreto
        [HttpGet("{Id}/Password")]
        //[Route("{Id}")]
        public ActionResult<Password> Get(int Id) {
           var pass = Passwords.Find(x => x.Id == Id);
            return pass == null ? NotFound() : Ok(pass); //404 or 200
        }

        // get los passwords de una categoria
        [HttpGet]
        [Route("{Category}")]
        public ActionResult<List<Password>> Get(string Category) {

           List<Password> listCategory = new List<Password>();
            foreach (Password passwordCategory in Passwords) {
                if (passwordCategory.Category == Category) {
                    listCategory.Add(passwordCategory);
                }
            }
           
            return listCategory.FirstOrDefault() == null ? NotFound() : Ok(listCategory); //404 or 200
        }


        [HttpPost] // Add
        public ActionResult Post(Password pass) {
            var existingPassword = Passwords.Find(x => x.User == pass.User);
            if (existingPassword != null) {
                return Conflict("There is an existing password for this user!"); // Status 409
            } else {
                Passwords.Add(pass);
                var resourceUrl = Request.Path.ToString() + "/" + pass.Id;
                return Created(resourceUrl, pass); // Status 201
            }
        }


        [HttpPut] // Update
        public ActionResult Put(Password pass) {
            var existingPassword = Passwords.Find(x => x.Id == pass.Id);
            if (existingPassword == null) {
                return Conflict("There is not any user with this Id"); // Status 409
            } else {
                existingPassword.Id = pass.Id;
                existingPassword.Category = pass.Category;
                existingPassword.User = pass.User;
                existingPassword.Date = pass.Date;
                existingPassword.Url = pass.Url;
                existingPassword.TagName = pass.TagName;
                existingPassword.Pass = pass.Pass;
                existingPassword.Description = pass.Description;
                return Ok(); //200
            }
        }


        [HttpDelete] // Delete
        [Route("{Id}")]
        public ActionResult<Password> Delete(int Id) {
            var pass = Passwords.Find(x => x.Id == Id);
            if (pass == null) {
                return NotFound("There is not a password saved for this user"); //404
            } else {
                Passwords.Remove(pass);
                return NoContent(); //204
            }
        }

    }

}


