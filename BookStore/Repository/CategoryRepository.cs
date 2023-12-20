using BookStore.Data;
using BookStore.Models;

namespace BookStore.Repository
{
    public class CategoryRepository : GenreTypeRepository
    {
        private readonly  ApplicationDbContext db;
        public CategoryRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public Category Add(Category category)
        {
            db.Category.Add(category);
            db.SaveChanges();
            return category;
        }

        public Category Delete(string IdCategory)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return db.Category;
        }

        public Category GetCate(string IdCategories)
        {
            return db.Category.Find(IdCategories);
        }


        public Category Update(Category category)
        {
            db.Update(category);
            db.SaveChanges();
            return category;
        }
    }
}
