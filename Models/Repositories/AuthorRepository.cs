using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Models.Repositories
{
    public class AuthorRepository : IBookstoreRepository<Author>
    {
        IList<Author> authors;

        public AuthorRepository()
        {
            authors = new List<Author>()
            {
                new Author() { Id = 1, FullName = "Amr El Rawy" },
                new Author() { Id = 2, FullName = "Ahmed Mourad" },
                new Author() { Id = 3, FullName = "Omar Gaber" },

            };
            }
        public void Add(Author entity)
        {
            entity.Id = authors.Max(a=>a.Id) +1;
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            authors.Remove(author);
        }

        public Author Find(int id)
        {
            var author = authors.FirstOrDefault(a => a.Id == id);
            return author; 
        }

        public IList<Author> List()
        {
            return authors;
        }

        public void Update(int id, Author newAuthor)
        {
            var author = Find(id);
            author.FullName = newAuthor.FullName;
        }
        public List<Author> Search(string term)
        {
            var result = authors.Where(a => a.FullName.Contains(term)).ToList();
            return result;
        }
    }
}
