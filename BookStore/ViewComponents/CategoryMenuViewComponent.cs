using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly GenreTypeRepository _Category;
        public CategoryMenuViewComponent(GenreTypeRepository genreTypeRepository)
        {
            _Category = genreTypeRepository;
        }

        public IViewComponentResult Invoke()
        {
            var category = _Category.GetAllCategories().OrderBy(x => x.CategoryName);
            return View(category);
        }
    }
}
