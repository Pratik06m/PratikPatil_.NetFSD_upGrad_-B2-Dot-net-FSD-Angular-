using CategoryService.Models;

namespace CategoryService.Data
{
    public static class CategoryDbSeeder
    {
        public static void Seed(CategoryDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { CategoryId = 1, CategoryName = "Friends", Description = "Personal contacts" },
                    new Category { CategoryId = 2, CategoryName = "Office", Description = "Work-related contacts" }
                );
                context.SaveChanges();
            }
        }
    }
}
