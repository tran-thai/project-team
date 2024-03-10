using System.Reflection;
using WebApplication3.Models;

namespace WebApplication3.Models.Services
{
    public interface ISearch
    {
        IQueryable<Article> GetAll();

        Task<Article> GetById(int? id);

        Task SaveChanges();

        public IQueryable<Article> Articles { get; set; }
        public string TitleSortOrder { get; set; }
        public string AuthorSortOrder { get; set; }

    }
}
