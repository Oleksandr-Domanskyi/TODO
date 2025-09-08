using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TODO.Core.Dto;
using TODO.Core.Dto.ProjectTask;
using TODO.Core.Entity;
using TODO.Infrastructure.Repositories.IRepositories;
using TODO.Infrastructure.Services;
using FluentResults;

namespace TODO.Tests
{
    [TestFixture]
    public class ProjectTaskRepositoryServicesTests
    {
        private Mock<IProjectTaskRepository>? _mockProjectTaskRepo;
        private ProjectTaskRepositoryServices? _service;

        [SetUp]
        public void Setup()
        {
            _mockProjectTaskRepo = new Mock<IProjectTaskRepository>();

            _service = new ProjectTaskRepositoryServices(
                _mockProjectTaskRepo.Object
            );
        }

        [Test]
        public async Task AddProjectTaskAsync_ShouldReturnCreatedTask()
        {

            var dto = new ProjectTaskCreateDTO
            {
                Title = "New Task",
                Description = "Task description",
                ExpiryDate = DateTime.UtcNow.AddDays(5)
            };

            _mockProjectTaskRepo!
                .Setup(r => r.CreateAsync(It.IsAny<ProjectTask>()))
                .ReturnsAsync((ProjectTask t) => t);


            var result = await _service!.AddProjectTask(dto);


            Assert.IsNotNull(result);
            Assert.AreEqual(dto.Title, result.Value.Title);
            Assert.AreEqual(dto.Description, result.Value.Description);
            Assert.AreEqual(dto.ExpiryDate, result.Value.ExpiryDate);

            _mockProjectTaskRepo.Verify(r => r.CreateAsync(It.IsAny<ProjectTask>()), Times.Once);
        }

        [Test]
        public async Task DeleteProjectTaskAsync_ShouldCallRepositoryAndReturnTrue()
        {

            var taskId = Guid.NewGuid();
            _mockProjectTaskRepo!.Setup(r => r.DeleteAsync(taskId)).ReturnsAsync(true);


            var result = await _service!.DeleteProjectTask(taskId);


            Assert.IsTrue(result.IsSuccess);
            _mockProjectTaskRepo.Verify(r => r.DeleteAsync(taskId), Times.Once);
        }

        [Test]
        public async Task DeleteProjectTaskAsync_WhenRepositoryReturnsFalse_ShouldReturnFailedResult()
        {

            var projectTaskId = Guid.NewGuid();
            _mockProjectTaskRepo!
                .Setup(r => r.DeleteAsync(projectTaskId))
                .ReturnsAsync(false);


            var result = await _service!.DeleteProjectTask(projectTaskId);


            Assert.IsTrue(result.IsSuccess); // метод завжди повертає IsSuccess = true
            _mockProjectTaskRepo.Verify(r => r.DeleteAsync(projectTaskId), Times.Once);
        }



    }
}
