using Core.Services;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Xunit;
using System.Threading.Tasks;
using Core.Repositories;

namespace Tester.ServicesTest
{
    public class DirectoryServiceTest
    {
        [Theory, AutoData]
        public void CreateNew_Directory_Should_Success([Frozen] Mock<IDirectoryService> repository, DirectoryRepository directoryService, Directory directory)
        {
            repository.Setup(x => x.AddDirectoryAsync(directory)).Returns(It.IsAny<Task<Directory>>());
            Action action = async () =>
            {
                await directoryService.AddDirectoryAsync(directory);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoData]
        public void GetAllDirectories_Should_Success([Frozen] Mock<IDirectoryService> repository, DirectoryRepository directoryService, List<Directory> directories)
        {
            repository.Setup(x => x.GetDirectories()).Returns(directories.AsQueryable);
            Action action = () =>
            {
                var result = directoryService.GetDirectories();
                result.Count().Should().Be(directories.Count);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoData]
        public void GetDirectory_Should_Success([Frozen] Mock<IDirectoryService> repository, DirectoryRepository directoryService, Directory directory)
        {
            repository.Setup(x=>x.GetDirectoryAsync(directory)).Returns(It.IsAny<Task<Directory>>());
            Action action = async () =>
            {
                var result = await directoryService.GetDirectoryAsync(directory);
                result.Should().BeEquivalentTo(directory);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoData]
        public void DeleteDirectory_Should_Success([Frozen] Mock<IDirectoryService> repository, DirectoryRepository directoryService, Directory directory)
        {
            repository.Setup(x => x.DeleteDirectoryAsync(directory)).Returns(It.IsAny<Task<bool>>());
            Action action = async () =>
            {
                var result = await directoryService.DeleteDirectoryAsync(directory);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoData]
        public void UpdateDirectory_Should_Success([Frozen] Mock<IDirectoryService> repository, DirectoryRepository directoryService, Directory directory)
        {
            repository.Setup(x => x.UpdateDirectoryAsync(directory)).Returns(It.IsAny<Task<Directory>>());
            Action action = async () =>
            {
                var result = await directoryService.UpdateDirectoryAsync(directory);
            };
            action.Should().NotThrow<Exception>();
        }
    }
}