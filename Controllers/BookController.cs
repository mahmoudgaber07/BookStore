using Bookstore.Models;
using Bookstore.Models.Repositories;
using Bookstore.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookstoreRepository<Book> bookRepository;
        private readonly IBookstoreRepository<Author> authorRepository;
        private readonly IHostingEnvironment hoisting;

        public BookController(IBookstoreRepository<Book> bookRepository,IBookstoreRepository<Author> authorRepository,IHostingEnvironment hoisting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hoisting = hoisting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return View(model);              //this will show in select at create.cshtml of Book
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string filename = string.Empty;      //declare empty var for name of file
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(hoisting.WebRootPath, "uploads"); //get uploads path//copmpine this path with folder named uploads
                        filename = model.File.FileName;         //name of file is its name
                        string fullpath = Path.Combine(uploads, filename);      //fullpath combine uploads and filename
                        model.File.CopyTo(new FileStream(fullpath, FileMode.Create)); //copy file to upload
                    }

                    if (model.AuthorId == -1)
                    {
                        ViewBag.Message = "Please select an Author from list";                       
                        return View(GetAllAuthors());
                    }
                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = authorRepository.Find(model.AuthorId),
                        ImageUrl = filename,              //add path
                    };
                    bookRepository.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }

            }

                
                ModelState.AddModelError("", "You have fill all required fields!");
                return View(GetAllAuthors());
            
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);
            var authId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;
            var model = new BookAuthorViewModel { 
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                //AuthorId = book.Author.Id,     //error so i add terninay operator in va authId
                AuthorId = authId,               //must add Author porberty in BookRepository to all books
                Authors = authorRepository.List().ToList(),
                ImageUrl = book.ImageUrl,
            };
            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorViewModel model)
        {
            try
            {
                string filename = string.Empty;      //declare empty var for name of file
                if (model.File != null)
                {
                    string uploads = Path.Combine(hoisting.WebRootPath, "uploads"); //get uploads path//copmpine this path with folder named uploads
                    filename = model.File.FileName;         //name of file is its name
                    string fullpath = Path.Combine(uploads, filename);      //fullpath combine uploads and filename
                    //Delete old file
                    string OldFileName = model.ImageUrl; //get filename //Note add new hidden input file in Edit.cshtml
                    string OldFullPath = Path.Combine(uploads, OldFileName);
                    if(fullpath != OldFullPath)
                    {
                    System.IO.File.Delete(OldFullPath);                              //Delete old file
                    //save new file
                    model.File.CopyTo(new FileStream(fullpath, FileMode.Create)); //Copy file to upload folder
                    }
                }

                Book book = new Book
                {
                    Id = model.BookId,              //add id from model
                    Title = model.Title,
                    Description = model.Description,
                    Author = authorRepository.Find(model.AuthorId),
                    ImageUrl = filename,           //change here from File to get new image name
                };
                bookRepository.Update(model.BookId,book);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteItem(int id)       //change function name because it same name and arg with BookRepository Delete function
        {                                           //Note: change name in Delete.cshtml //on submit asp-action="newName"
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public List<Author> FillSelectList()
        {
            var authorList = authorRepository.List().ToList();
            authorList.Insert(0, new Author { Id = -1, FullName = "Please Select an Author" });
            return authorList;
        }
        public BookAuthorViewModel GetAllAuthors()
        {
            var vmodel = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return vmodel;
        }
        public ActionResult Search(string term)
        {
            var res =bookRepository.Search(term);       //Note term must be th name of input
            return View("Index",res);      //show index but by res not all list
        }
    }
}
