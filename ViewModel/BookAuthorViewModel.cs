using Bookstore.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.ViewModel
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }
        
        [Required]
        [StringLength(50,MinimumLength = 5)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(100)]
        [MinLength(10)]
        public string Description { get; set; }
        

        [Required]
        public int AuthorId { get; set; }
        public List<Author> Authors { get; set; }
        public IFormFile File { get; set; }
        public string ImageUrl { get; set; }
    }
}
