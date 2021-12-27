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

    public class CategoryController : ControllerBase {

        private static List<Password> Categories = new List<Password> {
        new Password { Id = 1, Category = "Personal"},
        new Password { Id = 2, Category = "Personal2"}

        };


        // Se me muestran todas las categorias del array Categories
        [HttpGet]
        public ActionResult<List<Password>> Get() {
            return Ok(Categories); //200
        }

        // Se borra una categoria
        [HttpDelete] 
        [Route("{Category}")]
        public ActionResult<Password> Delete(string Category) {
            var categories = Categories.Find(x => x.Category == Category);
            if (categories == null) {
                return NotFound("There is not a categories saved for this user");
            } else {
                Categories.Remove(categories);
                return NoContent(); //204
            }
        }

        [HttpPost] // Add a  al array Categories
        public ActionResult Post(Password category) {
            var existingCategory = Categories.Find(x => x.Category == category.Category);
            if (existingCategory != null) {
                return Conflict("There is an existing category with this name!"); // Status 409
            } else {
                Categories.Add(category);
                var resourceUrl = Request.Path.ToString() + "/" + category.Category;
                return Created(resourceUrl, category); // Status 201
            }
        }


    }

}


