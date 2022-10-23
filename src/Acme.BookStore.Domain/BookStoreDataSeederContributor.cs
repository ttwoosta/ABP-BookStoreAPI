using Acme.BookStore.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore
{
    public class BookStoreDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private IRepository<Book, Guid> _bookRepo;

        public BookStoreDataSeederContributor(IRepository<Book, Guid> bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _bookRepo.GetCountAsync() == 0)
            {
                await _bookRepo.InsertAsync(new Book { 
                    Name = "1984",
                    Type = BookType.Dystopia,
                    PublishDate = new DateTime(1949, 6, 8),
                    Price = 19.84f
                }, 
                autoSave: true);

                await _bookRepo.InsertAsync(new Book
                {
                    Name = "New Perspectives",
                    Type = BookType.Science,
                    PublishDate = new DateTime(2017, 2, 8),
                    Price = 99.99f
                },
                autoSave: true);
            }
        }
    }
}
