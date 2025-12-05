using AutoMapper;
using GenericStore.Application.Services;
using GenericStore.Domain.Entities;
using GenericStore.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GenericStore.Application.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IMapper> _mockMapper = new();
        private GenericStoreContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<GenericStoreContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // By this way, the database is unique for each test with Guid.NewGuid()
                .Options;

            return new GenericStoreContext(options);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity()
        {
            // Arrange
            using var context = CreateContext();

            var product = new Product
            {
                ProductId = 1,
                Name = "Test Product",
                Description = "Test Description",
                Price = 9.99m,
                OnSale = false,
                CreatedDate = DateTime.UtcNow
            };
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var service = new ProductService(context, _mockMapper.Object);

            // Act
            var result = await service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ProductId);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNotFound()
        {
            // Arrange
            using var context = CreateContext();
            var service = new ProductService(context, _mockMapper.Object);

            // Act
            var result = await service.GetByIdAsync(999); // Non-existing ID

            // Assert
            Assert.Null(result);
        }
    }
}
