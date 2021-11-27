using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ofarz_rest_api.Domain.IService.IUserServices;
using ofarz_rest_api.Domain.Models.Account;
//using ofarz_rest_api.Domain.Models.Account;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Extensions;
using ofarz_rest_api.Persistence.Context;
using ofarz_rest_api.Resources.UserResources;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ofarz_rest_api.Controllers.UserController
{
    [Route("/api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<ApplicationUser> _userManager;


        public CategoriesController(ICategoryService categoryService,
                                    UserManager<ApplicationUser> userManager,
                                    AppDbContext context,
                                    IHttpContextAccessor httpContext)
        {
            _categoryService = categoryService;
            _userManager = userManager;
            _context = context;
            _httpContext = httpContext;
        }


        [HttpGet]
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            //var uid = _httpContext.HttpContext.User.Claims.SingleOrDefault(
            //c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            var currentUser = _userManager.GetUserId(HttpContext.User);

            //var user = await _userManager.FindByIdAsync(User.Identity.Name);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var categories = await _categoryService.ListAsync();
            //var resources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);
            return categories;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            var category = await _categoryService.FindByIdAsync(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Category resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            //var category = _mapper.Map<CategoryResource, Category>(resource);
            var result = await _categoryService.SaveAsync(resource);

            if (!result.Success)
                return BadRequest(result.Message);


            //var categoryResource = _mapper.Map<Category, CategoryResource>(result.Resource);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] CategoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());


            //var category = _mapper.Map<CategoryResource, Category>(resource);
            var result = await _categoryService.UpdateAsync(id, resource);


            if (!result.Success)
                return BadRequest(result.Message);


            //var categoryResource = _mapper.Map<Category, CategoryResource>(result.Resource);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _categoryService.DeleteAsync(id);


            if (!result.Success)
                return BadRequest(result.Message);



            return Ok(result);
        }
    }
}
