using Microsoft.AspNetCore.Mvc;
using Mission11_Forbush.Models;
using Mission11_Forbush.Models.ViewModels;
using System.Diagnostics;

namespace Mission11_Forbush.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository _repo;

        public HomeController(IBookstoreRepository temp)
        {
            _repo = temp;
        }

        // We are bundeling together a ViewModel for the 'Index.cshtml' IndexViewModel page
        public IActionResult Index(int pageNum)
        {
            int pageSize = 10;

            var blah = new BooksListViewModel
            {

                Books = _repo.Books
                    .OrderBy(x => x.Title)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = _repo.Books.Count()
                }
            };

            return View(blah);
        }
    }
}
