using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using ExperimentApplication.Models;
using ExperimentApplication.Repositories;
using ExperimentTest.TestUtils;
using Moq;
using Xunit;

namespace ExperimentTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task TestEF()
        {
            var data = new List<Category>
            {
                new Category("BBB"),
                new Category("ZZZ"),
                new Category("AAA"),
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Category>>();
            mockSet.As<IDbAsyncEnumerable<Category>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Category>(data.GetEnumerator()));

            mockSet.As<IQueryable<Category>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Category>(data.Provider));

            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ExperimentContext>();
            mockContext.Setup(c => c.Categories).Returns(mockSet.Object);

            var service = new CategoryRepository(mockContext.Object);
            var blogs = (await service.GetAllAsync()).ToList();

            Assert.Equal(3, blogs.Count());

            Assert.Equal("BBB", blogs[0].Name);
            Assert.Equal("ZZZ", blogs[1].Name);
            Assert.Equal("AAA", blogs[2].Name);
        }
    }
}
