using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Books
{
    public class BookAppService : 
        CrudAppService<Book, BookDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateBookDto>,
        IBookAppService
    {
        public BookAppService(IRepository<Book, Guid> repository) : base(repository)
        {

        }

        protected override Book MapToEntity(CreateUpdateBookDto createInput)
        {
            return base.MapToEntity(createInput);
        }

        protected override Task<Book> MapToEntityAsync(CreateUpdateBookDto createInput)
        {
            return base.MapToEntityAsync(createInput);
        }

        public override Task<BookDto> CreateAsync(CreateUpdateBookDto input)
        {
            return base.CreateAsync(input);
        }

    }
}
