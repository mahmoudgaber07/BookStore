using Bookstore.Models;
using Bookstore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBookstoreRepository<Author> authourRepository;

        public AuthorController(IBookstoreRepository<Author> AuthourRepository)
        {
            authourRepository = AuthourRepository;      //Note: make interface in model is public
        }
        // GET: AuthorController
        public ActionResult Index()
        {
            var authors = authourRepository.List();         //get authors and store in var
            return View(authors);                           //put as pram to showing in html
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(int id)
        {
            var author = authourRepository.Find(id);
            return View(author);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                authourRepository.Add(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = authourRepository.Find(id);    //get single authour
            return View(author);                       //view this single authour to edit
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)            //change pram to type Author author
        {
            try
            {
                authourRepository.Update(id, author);        //use update method and send id and update author
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            var author = authourRepository.Find(id);        //get single authour
            return View(author);                            //view this single authour to Delete
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try
            {
                authourRepository.Delete(id);            //use delete method and send id of author
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
