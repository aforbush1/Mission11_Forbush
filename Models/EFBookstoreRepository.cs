
using Microsoft.AspNetCore.Mvc;

namespace Mission11_Forbush.Models
{
    public class EFBookstoreRepository : IBookstoreRepository
    {
        private BookstoreContext _context;
        public EFBookstoreRepository(BookstoreContext temp)
        {
            _context = temp;
        }

        public IQueryable<Book> Books => _context.Books;
    }
}
