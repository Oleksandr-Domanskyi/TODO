using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TODO.Core.Dto;
using TODO.Core.Entity;
using TODO.Infrastructure.Repositories.IRepositories;
using TODO.Infrastructure.Services;
using TODO.Infrastructure.Services.IServices;
using FluentResults;
using TODO.Core.Dto.ProjectTask;

namespace TODO.Tests
{
    [TestFixture]
    public class SubTaskRepositoryServicesTests
    {
        private Mock<ISubTaskRepository>? _mockSubTaskRepo;
        private Mock<IProjectTaskRepositoryServices>? _mockProjectTaskRepoService;
        private Mock<IProjectTaskRepository>? _mockProjectTaskRepo;
        private SubTaskRepositoryServices? _service;

        [SetUp]
        public void Setup()
        {
            _mockSubTaskRepo = new Mock<ISubTaskRepository>();
            _mockProjectTaskRepoService = new Mock<IProjectTaskRepositoryServices>();
            _mockProjectTaskRepo = new Mock<IProjectTaskRepository>();

            _service = new SubTaskRepositoryServices(
                _mockSubTaskRepo.Object,
                _mockProjectTaskRepoService.Object,
                _mockProjectTaskRepo.Object
            );
        }

        [Test]
        public async Task AddSubTaskAsync_ShouldReturnCreatedSubTask()
        {
            // Arrange
            var projectTaskId = Guid.NewGuid();
            var dto = new SubTaskCreateDTO
            {
                Title = "New SubTask",
                Description = "Test description",
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            };

            _mockSubTaskRepo!
                .Setup(r => r.AddAsync(It.IsAny<SubTask>()))
                .ReturnsAsync((SubTask st) => st);

            // Act
            var result = await _service!.AddSubTask(projectTaskId, dto);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(dto.Title, result.Value.Title);
            Assert.AreEqual(dto.Description, result.Value.Description);
            Assert.AreEqual(dto.ExpiryDate, result.Value.ExpiryDate);

            _mockSubTaskRepo.Verify(r => r.AddAsync(It.IsAny<SubTask>()), Times.Once);
        }

        [Test]
        public async Task DeleteSubTaskAsync_ShouldCallRepositoryAndReturnTrue()
        {
            // Arrange
            var subTaskId = Guid.NewGuid();
            var subTask = new SubTask { Id = subTaskId };
            _mockSubTaskRepo!
                .Setup(r => r.GetByIdAsync(subTaskId))
                .ReturnsAsync(subTask);
            _mockSubTaskRepo!
                .Setup(r => r.DeleteAsync(subTaskId))
                .ReturnsAsync(true);

            // Act
            var result = await _service!.DeleteSubTask(subTaskId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            _mockSubTaskRepo.Verify(r => r.DeleteAsync(subTaskId), Times.Once);
        }

        [Test]
        public async Task DeleteSubTaskAsync_WhenRepositoryReturnsFalse_ShouldReturnFalse()
        {
            // Arrange
            var subTaskId = Guid.NewGuid();
            var subTask = new SubTask { Id = subTaskId };
            _mockSubTaskRepo!
                .Setup(r => r.GetByIdAsync(subTaskId))
                .ReturnsAsync(subTask);
            _mockSubTaskRepo!
                .Setup(r => r.DeleteAsync(subTaskId))
                .ReturnsAsync(false);

            // Act
            var result = await _service!.DeleteSubTask(subTaskId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public async Task MarkAsDoneAsync_ShouldSetIsCompletedTrue()
        {

            var subTaskId = Guid.NewGuid();
            var subTask = new SubTask
            {
                Id = subTaskId,
                Title = "Test Task",
                IsCompleted = true,
                IsActive = true
            };


            _mockSubTaskRepo!
                .Setup(r => r.MarkAsDoneAsync(subTaskId))
                .ReturnsAsync(true);

            _mockSubTaskRepo!
                .Setup(r => r.GetByIdAsync(subTaskId))
                .ReturnsAsync(subTask);

            _mockProjectTaskRepoService!
                .Setup(r => r.UpdateTotalProgress(It.IsAny<Guid>()))
                .ReturnsAsync(Result.Ok());


            var result = await _service!.MarkAsDone(subTaskId);


            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Value.IsCompleted);
            Assert.IsTrue(result.Value.IsActive);

            _mockSubTaskRepo.Verify(r => r.MarkAsDoneAsync(subTaskId), Times.Once);
            _mockSubTaskRepo.Verify(r => r.GetByIdAsync(subTaskId), Times.Once);
            _mockProjectTaskRepoService.Verify(r => r.UpdateTotalProgress(subTask.ProjectTask_Id), Times.Once);
        }

    }
}
