﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using ExperimentApplication.Classes;
using ExperimentApplication.Models;
using ExperimentApplication.Repositories;
using ExperimentTest.TestUtils;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;
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
            mockContext.Setup(c => c.Set<Category>()).Returns(mockSet.Object);

            var service = new CategoryRepository(mockContext.Object);
            var blogs = (await service.GetAllAsync()).ToList();

            Assert.Equal(3, blogs.Count());

            Assert.Equal("BBB", blogs[0].Name);
            Assert.Equal("ZZZ", blogs[1].Name);
            Assert.Equal("AAA", blogs[2].Name);
        }


        private Mock<ExperimentContext> GenerateMockContext<T>(IQueryable<T> data, Mock<ExperimentContext> mockContext = null) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IDbAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<T>(data.GetEnumerator()));

            mockSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<T>(data.Provider));

            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            if (mockContext == null)
                mockContext = new Mock<ExperimentContext>();
            mockContext.Setup(c => c.Set<T>()).Returns(mockSet.Object);
            return mockContext;
        }

        public void TestExternalDep()
        {
            /*
            E.g. Testing email service.
            I have decided to take out all Email-related classes reference from Controllers.
            An additional Service layer should be the one referenced by Controllers.
            The Service layer SHALL return HTTPStatusCode to enable Controller to respond accordingly (with Noty etc)
            The Service layer should reference Email-related classes.
            
            Unit-testing Service Layer:
            1) Email-sending class should be an interface (Send)
            2) Verify behaviour that an external dep method is indeed called at least once (or other behaviour). (No other way to test)
            */
        }

        [Theory,
         InlineData(12, 6),
         InlineData(6, 3)]
        public void TestTimeSensitiveCode(int userCount, int inActive)
        {
            /*
            Any feature that require DateTime.Now / UtcNow or Today should be unit-test pluggable.
            I should be able to determine the values returned by these methods to make the test time-insensitive and more accurate.
            */

            /*
            For example, I want to find Users who have not been active for 2 days.
            */

            // Arrange
            var fixture = AutoFixtureFactory.Create();
            var users = fixture.CreateMany<User>(userCount).AsQueryable();
            SystemTime.SetDateTime(new DateTime(2015, 1, 20));

            var inactiveDate = new DateTime(2015, 1, 10);
            var futureDate = new DateTime(2015, 1, 19);

            foreach (var user in users.Take(inActive))
                user.LastSeen = inactiveDate;

            foreach (var user in users.Skip(inActive))
                user.LastSeen = futureDate;

            var mockContext = GenerateMockContext(users);
            var userRepo = new UserRepository(mockContext.Object);

            // Act
            var inactiveUsers = userRepo.GetInactiveUsers();

            // Assert
            Assert.Equal(inactiveUsers.Count(), inActive);
        }

        [Fact]
        public void TestNonAsyncEF()
        {
            var data = new List<Category>
            {
                new Category("BBB"),
                new Category("ZZZ"),
                new Category("AAA"),
            }.AsQueryable();

            var userData = new List<User>
            {
                new User("John Doe")
            }.AsQueryable();

            // desired API: Fluents
            // desired usage: Build() -> returns Mock<ExperimentContext<
            /*
            sample: Build<ExperimentContext>()
                    .AddData<User>(userData) 
                    .AddData<Category>(categoryData)
                    .Create()
                    returns Mock<Context> containing sample data states.

            sample: Create(data)
                    returns Mock<Context> containing sample data states.

            Data should be AutoFixture generated... 
            
            Relevant states to be tested should then be manually arranged.

            */
            var mockContext = GenerateMockContext(data);
            mockContext = GenerateMockContext(userData, mockContext);

            var service = new CategoryRepository(mockContext.Object);
            var userRepo = new UserRepository(mockContext.Object);
            var blogs = service.GetAll().ToList();

            Assert.Equal(3, blogs.Count());
            Assert.Equal(2, userRepo.Count);

            Assert.Equal("BBB", blogs[0].Name);
            Assert.Equal("ZZZ", blogs[1].Name);
            Assert.Equal("AAA", blogs[2].Name);
        }

        [Fact]
        public void TestRefactoredTest()
        {
            // here we want to test logic with queries inside.


        }

        [Theory, CustomAutoMoqData]
        public void TestTheory(User user)
        {

        }

        [Fact]
        public void BizLogic1()
        {
            // User cannot have more than 3 listings....
            var fixture = AutoFixtureFactory.Create();
            var user = fixture.Create<User>();
            user.ListingItems = new List<Listing>(fixture.CreateMany<Listing>(3));
            Assert.Equal(user.TryAddListing(fixture.Create<Listing>()), false);
        }

        [Fact]
        public void BizLogic2()
        {
            // User cannot have more than 3 listings....
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var user = fixture.Create<User>();
            user.ListingItems = new List<Listing>(fixture.CreateMany<Listing>(3));
            Assert.Equal(user.TryAddListing(fixture.Create<Listing>()), false);
        }
    }
}
