using Acme.BookStore.Books;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Json;
using Volo.Abp.Validation;
using Xunit;

namespace Acme.BookStore.Samples
{
    public class BookAppService_Tests : BookStoreApplicationTestBase
    {
        private readonly IBookAppService _bookAppService;
        private readonly IJsonSerializer _jsonSerializer;

        public BookAppService_Tests()
        {
            _bookAppService = GetRequiredService<IBookAppService>();
            _jsonSerializer = GetRequiredService<IJsonSerializer>();
        }

        [Fact]
        public async Task Should_get_list_of_Books()
        {
            // Act
            var result = await _bookAppService.GetListAsync(
                new PagedAndSortedResultRequestDto());

            // Assert
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(b => b.Name == "1984");
        }

        [Fact]
        public async Task Should_Create_A_Valid_Book()
        {
            //Act
            var result = await _bookAppService.CreateAsync(
                new CreateUpdateBookDto
                {
                    Name = "New test book 42",
                    Price = 10,
                    PublishDate = DateTime.Now,
                    Type = BookType.ScienceFiction
                }
            );

            //Assert
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe("New test book 42");
        }

        [Fact]
        public async Task Should_Update_A_Valid_Book()
        {
            // Arrange
            var listresult = await _bookAppService.GetListAsync(
                new PagedAndSortedResultRequestDto());
            var fb = listresult.Items[0];

            //Act
            var result = await _bookAppService.UpdateAsync(fb.Id,
                new CreateUpdateBookDto
                {
                    Name = "Updated title",
                    Price = 10,
                    PublishDate = DateTime.Now,
                    Type = BookType.ScienceFiction
                }
            );

            //Assert
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe("Updated title");
        }

        [Fact]
        public async Task Should_Not_Create_A_Book_Without_Name()
        {
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _bookAppService.CreateAsync(
                    new CreateUpdateBookDto
                    {
                        Name = "",
                        Price = 10,
                        PublishDate = DateTime.Now,
                        Type = BookType.ScienceFiction
                    }
                );
            });

            exception.ValidationErrors
                .ShouldContain(err => err.MemberNames.Any(mem => mem == "Name"));
        }


        [Fact]
        public void Should_Serialize_Deserialize_DateTime_Correctly()
        {
            string json = "{\"name\":\"New book 1\",\"type\":2,\"publishDate\":\"2011-10-05T02:00:00.000Z\",\"price\":4}";
            var obj1 = _jsonSerializer.Deserialize<CreateUpdateBookDto>(json);
            Assert.NotNull(obj1);

            string json1 = _jsonSerializer.Serialize(obj1);
            Assert.Equal(json, json1);
        }

        

    }
}
