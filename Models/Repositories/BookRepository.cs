using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Models.Repositories
{
    public class BookRepository : IBookstoreRepository<Book>
    {
        List<Book> books;   
        public BookRepository()
        {
             books = new List<Book>()
            {
                new Book
                {
                    Id = 1, Title="C#",Description="C#",Author = new Author(), ImageUrl="C#.png"
                },
                new Book
                {
                    Id = 2, Title="JS",Description="Javascript",Author = new Author() ,ImageUrl="Js.png"
                },
                new Book
                {
                    Id = 3, Title="C++",Description="C++",Author = new Author(), ImageUrl="C++.png"
                },
            };
        }
        public void Add(Book entity)
        {
            entity.Id = books.Max(b=> b.Id) + 1;                  //add auto Id
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);     //prefer to use Find Method 
            books.Remove(book);
        }

        public Book Find(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);     
            return book;
        }

        public IList<Book> List()
        {
            return books; 
        }

   

        public void Update(int id,Book newBook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);     //prefer to use Find Method like //var book = Find(id);
            book.Title = newBook.Title;
            book.Description = newBook.Description;
            book.Author = newBook.Author;   
            book.ImageUrl = newBook.ImageUrl;
        }

        public List<Book> Search(string term)
        {
            var result = books.Where(b => b.Title.Contains(term) || b.Description.Contains(term) || b.Author.FullName.Contains(term)).ToList();
            return result;
        }
    }
}
