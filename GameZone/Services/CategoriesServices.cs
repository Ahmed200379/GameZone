using GameZone.Data;
using GameZone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameZone.Services
{
    public class CategoriesServices : ICategoriesService
    {
        private readonly ApplicationDbContext _Context;
        public CategoriesServices(ApplicationDbContext context)
        {
            _Context = context;
        }

        public void Create(Category category)
        {
            _Context.Categories.Add(category);
          var isSaved=  _Context.SaveChanges();
            if (isSaved <= 0)
            {
                Console.WriteLine("error in save");
            }
        }

        public IEnumerable<SelectListItem> GetSelectList()
        {
           return _Context.Categories
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
               .OrderBy(c => c.Text)
               .AsNoTracking()
              .ToList();
        }
    }
}
