using BookStore.Models;
namespace BookStore.Repository
{
    public interface GenreTypeRepository
    {
        Category Add(Category category);
        Category Update(Category category);
        Category Delete(string IdCategory);
        Category GetCate(string IdCategories);
        IEnumerable<Category> GetAllCategories();

    }
}
