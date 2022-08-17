using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static List<Book> Booklist = new List<Book>()
        {
            new Book{
                Id = 1,
                Title = "Ikna Sanatı",
                GenreId = 1, //Personal Growth
                PageCount = 200,
                PublishDate = new DateTime(2001,06,12),
            },
            new Book{
                Id = 2,
                Title = "Lean Startup",
                GenreId = 1, //Personal Growth
                PageCount = 200,
                PublishDate = new DateTime(2001,06,12),
            },
            new Book{
                Id = 3,
                Title = "Dune",
                GenreId = 2, //Science Fiction
                PageCount = 200,
                PublishDate = new DateTime(2001,06,12),
            }

         };

        [HttpGet]
        public List<Book> GetBooks()
        {
            var booklist = Booklist.OrderBy(x => x.GenreId).ToList<Book>();
            return booklist;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = Booklist.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        /* [HttpGet]
         public Book Get([FromQuery] string id)
         {
             var book = Booklist.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
             return book;
         }*/



        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = Booklist.SingleOrDefault(x => x.Title == newBook.Title);

            if (book is not null)
                return BadRequest();

            Booklist.Add(newBook);
            return Ok();

        }


        [HttpPut]
        public IActionResult UpdateBook(int id,[FromBody] Book updatedbook)
        {
            var book = Booklist.SingleOrDefault(x => x.Id == id);

            if (book is null)
                return BadRequest();

            book.GenreId = updatedbook.GenreId != default ? updatedbook.GenreId : book.GenreId;
            book.PageCount = updatedbook.PageCount != default ? updatedbook.PageCount : book.PageCount;
            book.PublishDate = updatedbook.PublishDate != default ? updatedbook.PublishDate : book.PublishDate;
            book.Title = updatedbook.Title != default ? updatedbook.Title : book.Title;

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = Booklist.SingleOrDefault(x => x.Id == id);

            if (book is null)
                return BadRequest();

            Booklist.Remove(book);
            return Ok();
        }
    }
}
