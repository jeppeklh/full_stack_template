using Application.Services;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;

namespace Tests.Application.Services
{
    public class DepartmentServiceTests
    {
        private readonly Mock<IDepartmentRepository> _repo = new();
        private readonly DepartmentService _service;

        public DepartmentServiceTests()
        {
            _service = new DepartmentService(_repo.Object);
        }

        [Fact]
        public async Task AddDepartment_Throws_WhenDtoIsNull()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.AddDepartmentAsync(null!));
        }

        [Fact]
        public async Task DeleteDepartment_Throws_WhenRepoReturnsFalse()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repo.Setup(r => r.DeleteDepartmentAsync(id, It.IsAny<CancellationToken>())) // it.IsAny to ignore the token
                .ReturnsAsync(false); // return false to simulate not found

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteDepartmentAsync(id));
        }

        [Fact]
        public async Task GetDepartmentById_Throws_WhenIdMissing()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetDepartmentByIdAsync(Guid.Empty));
        }

        [Fact]
        public async Task GetAllDepartments_ReturnsMappedResults()
        {
            //Arrange
            var entities = new List<Department>
            {
                new Department { Id = Guid.NewGuid(), Name = "AA", Address = "Address1", DepartmentTypeId = Guid.NewGuid() }
            };
            _repo.Setup(r => r.GetAllDepartmentsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(entities);

            // Act
            var result = await _service.GetAllDepartmentsAsync();

            // Assert
            Assert.Single(result); // ensure only one item is returned
            var dto = result.First();
            Assert.Equal("AA", dto.Name);
            Assert.Equal("Address1", dto.Address);
        }
    }
}