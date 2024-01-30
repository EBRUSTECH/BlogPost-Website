using Azure;
using BlogTask.Models.Entities;
using BlogTask.Models.ViewModels;
using BlogTask.Services.Implementation;
using BlogTask.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlogTask.Controllers
{
    [Authorize(Roles = "admin")]
    public class DashboardController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IBlogService _blogService;
       
        public DashboardController(ICategoryService categoryService, IBlogService blogService)
        {
            _categoryService = categoryService;  
            _blogService = blogService;
        }      

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

       [HttpPost]
        public async Task<IActionResult> AddCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = model.Name,
                };

                await _categoryService.AddCategory(category);

                return RedirectToAction("AllCategories", "Dashboard");
            }
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int CategoryId)
        {
            var category = await _categoryService.GetCategory(CategoryId);

            if (category != null)
            {
                var editCategory = new AddCategoryViewModel
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name
                };

                return View(editCategory);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(AddCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = await _categoryService.GetCategory(model.CategoryId);

                if (existingCategory == null)
                {
                    return NotFound();
                }

                existingCategory.Name = model.Name;

                await _categoryService.UpdateCategory(existingCategory);

                return RedirectToAction("AllCategories", "Dashboard");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AllCategories()
        {
            var categories = await _categoryService.GetCategories();

            var viewModelCategories = categories.Select(category => new AllCategoryViewModel
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
            }).ToList();

            return View(viewModelCategories);
            
        }

        public async Task<IActionResult> DeleteTheCategory(int categoryId)
        {
            var result = await _categoryService.DeleteCategory(categoryId);
            
            return RedirectToAction("AllCategories", "Dashboard"); 
        }
   
        [HttpGet]
        public async Task<ActionResult> AddBlog()
        {
            var CategoriesList = await _categoryService.GetCategories();

            var model = new AddBlogViewModel
            {
               Categories = CategoriesList.Select(x => new SelectListItem { Text = x.Name, Value = x.CategoryId.ToString()})
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBlog(AddBlogViewModel model)
        {
            // Map view model to domain model                                                     
            var blogPost = new Blog
            {
                Title = model.Title,
                Text = model.Text,
            };

            var selectedCategories = new List<Category>();

            foreach (var selectedCategoryId in model.SelectedCategories)
            {
                if (int.TryParse(selectedCategoryId, out var selectedCategoryIdInt))
                {
                    var existingCategory = await _categoryService.GetCategory(selectedCategoryIdInt);

                    if (existingCategory != null)
                    {
                        selectedCategories.Add(existingCategory);
                    }
                }      
            }

            if (selectedCategories.Any())
            {
                blogPost.CategoryId = selectedCategories.First().CategoryId;
            }

            await _blogService.AddBlog(blogPost);

            return RedirectToAction("AllBlogPost", "Dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> AllBlogPost() 
        {
            var allBlogViewModel = new AllBlogViewModel
            {
                Blogs = await _blogService.GetAllBlog(),
            };

            return View(allBlogViewModel);
        }      
    }
}