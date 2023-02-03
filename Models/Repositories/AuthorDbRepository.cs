using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Models.Repositories
{
    public class AuthorDbRepository : IBookstoreRepository<Author>
    {
        BookstoreDbContext db;                    //BookstoreDbContext is context of db

        public AuthorDbRepository(BookstoreDbContext _db)
        {
            db = _db;
        }
        public void Add(Author entity)
            {
                db.Authors.Add(entity);
                db.SaveChanges();
            }

            public void Delete(int id)
            {
                var author = Find(id);
                db.Authors.Remove(author);
                db.SaveChanges();
            }

            public Author Find(int id)
            {
                var author = db.Authors.FirstOrDefault(a => a.Id == id);
                return author;
            }

            public IList<Author> List()
            {
                return db.Authors.ToList();
            }

            public void Update(int id, Author newAuthor)
            {
                db.Update(newAuthor);
                db.SaveChanges();
            }

        public List<Author> Search(string term)
        {
            var result = db.Authors.Where(a => a.FullName.Contains(term)).ToList();
            return result;
        }
    }
}
