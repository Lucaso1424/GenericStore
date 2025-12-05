namespace GenericStore.Test;

using AutoMapper;
using Core.Application.DTOs;
using Core.Application.Services;
using GenericStore.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class GenericServiceTests
{
    private readonly Mock<GenericStoreContext> _mockContext;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GenericService<GenericStoreContext, TestEntity, TestEntityDTO> _service;

    public GenericServiceTests()
    {
        _mockContext = new Mock<GenericStoreContext>();
        _mockMapper = new Mock<IMapper>();
        _service = new GenericService<GenericStoreContext, TestEntity, TestEntityDTO>(_mockContext.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        var data = new List<TestEntity> { new TestEntity(), new TestEntity() };
        _mockContext.Setup(c => c.Set<TestEntity>()).ReturnsDbSet(data);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoEntities()
    {
        // Arrange
        var data = new List<TestEntity>();
        _mockContext.Setup(c => c.Set<TestEntity>()).ReturnsDbSet(data);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task CreateAsync_ShouldAddEntity()
    {
        // Arrange
        var entity = new TestEntity();
        var dbSetMock = new Mock<DbSet<TestEntity>>();
        _mockContext.Setup(c => c.Set<TestEntity>()).Returns(dbSetMock.Object);

        // Act
        await _service.CreateAsync(entity);

        // Assert
        dbSetMock.Verify(m => m.Add(entity), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateEntity()
    {
        // Arrange
        var dto = new TestEntityDTO { TrackingState = "Modified", Id = 1 };
        var entity = new TestEntity();

        _mockMapper.Setup(m => m.Map<TestEntity>(It.IsAny<TestEntityDTO>())).Returns(entity);

        var dbSetMock = new Mock<DbSet<TestEntity>>();
        dbSetMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(entity);
        _mockContext.Setup(c => c.Set<TestEntity>()).Returns(dbSetMock.Object);

        // Act
        await _service.UpdateAsync(dto);

        // Assert
        _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        dbSetMock.Verify(m => m.Update(It.Is<TestEntity>(e => e == entity)), Times.Once);
    }


    [Fact]
    public async Task DeleteAsync_ShouldRemoveEntity()
    {
        // Arrange
        var entity = new TestEntity();
        var dbSetMock = new Mock<DbSet<TestEntity>>();
        dbSetMock.Setup(m => m.FindAsync(1)).ReturnsAsync(entity);
        _mockContext.Setup(c => c.Set<TestEntity>()).Returns(dbSetMock.Object);

        // Act
        await _service.DeleteAsync(1);

        // Assert
        dbSetMock.Verify(m => m.Remove(entity), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
    }
}

public class TestEntity { }

public class TestEntityDTO : BaseEntityDTO 
{
    private int id { get; set; }
    public int Id 
    { 
        get => id; 
        set 
        { 
            id = value; 
        }
    }
}