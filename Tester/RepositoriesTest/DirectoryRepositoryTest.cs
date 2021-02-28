using AutoMapper;
using Core.Repositories;
using Core.Services;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Tester.DTOs;
using Xunit;

namespace Tester.RepositoriesTest
{
    public class DirectoryRepositoryTest
    {
        [Fact]
        public async void SaveDirectory_Should_SuccessAsync()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Directory, DirectoryDto>();
                cfg.CreateMap<Contact, ContactDto>();
            });
            configuration.AssertConfigurationIsValid();
            var mapper = configuration.CreateMapper();

            var options = new DbContextOptionsBuilder<RiseTechDBContext>()
                .UseInMemoryDatabase("ContactDatabase")
                .Options;

            Mock<IDirectoryService> directoryServiceMock = new Mock<IDirectoryService>();

            var context = new RiseTechDBContext(options);
            var directoryRepository = new DirectoryRepository(context);
            var directory = await directoryRepository.AddDirectoryAsync(
                new Directory
                {
                    Uuid = Guid.NewGuid(),
                    Name = "Metin",
                    Surname = "TAŞLIK",
                    Company = "X",
                    IsActive = true
                });
            context.SaveChanges();
            Assert.Equal("Metin", directory.Name);
        }

        [Fact]
        public async void DeleteDirectory_Should_SuccessAsync()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Directory, DirectoryDto>();
                cfg.CreateMap<Contact, ContactDto>();
            });
            configuration.AssertConfigurationIsValid();
            var mapper = configuration.CreateMapper();

            var options = new DbContextOptionsBuilder<RiseTechDBContext>()
                .UseInMemoryDatabase("ContactDatabase")
                .Options;

            Mock<IDirectoryService> directoryServiceMock = new Mock<IDirectoryService>();

            var context = new RiseTechDBContext(options);
            var directoryRepository = new DirectoryRepository(context);
            var directory = context.Add(
                new Directory
                {
                    Uuid = Guid.NewGuid(),
                    Name = "Metin",
                    Surname = "TAŞLIK",
                    Company = "X",
                    IsActive = true
                });
            context.SaveChanges();
            Assert.True(await directoryRepository.DeleteDirectoryAsync(directory.Entity.Uuid));
        }

        [Fact]
        public async void GetDirectories_Should_SuccessAsync()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Directory, DirectoryDto>();
                cfg.CreateMap<Contact, ContactDto>();
            });
            configuration.AssertConfigurationIsValid();
            var mapper = configuration.CreateMapper();

            var options = new DbContextOptionsBuilder<RiseTechDBContext>()
                .UseInMemoryDatabase("ContactDatabase")
                .Options;

            Mock<IDirectoryService> directoryServiceMock = new Mock<IDirectoryService>();

            var context = new RiseTechDBContext(options);
            var directoryRepository = new DirectoryRepository(context);
            var directory = await directoryRepository.AddDirectoryAsync(
                new Directory
                {
                    Uuid = Guid.NewGuid(),
                    Name = "Metin",
                    Surname = "TAŞLIK",
                    Company = "X",
                    IsActive = true
                });
            context.SaveChanges();

            Assert.Empty(await directoryRepository.GetDirectoriesAsync());
        }

        [Fact]
        public async void GetDirectory_Should_SuccessAsync()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Directory, DirectoryDto>();
                cfg.CreateMap<Contact, ContactDto>();
            });
            configuration.AssertConfigurationIsValid();
            var mapper = configuration.CreateMapper();

            var options = new DbContextOptionsBuilder<RiseTechDBContext>()
                .UseInMemoryDatabase("ContactDatabase")
                .Options;

            Mock<IDirectoryService> directoryServiceMock = new Mock<IDirectoryService>();

            var context = new RiseTechDBContext(options);
            var directoryRepository = new DirectoryRepository(context);
            var directory = context.Add(
                new Directory
                {
                    Uuid = Guid.NewGuid(),
                    Name = "Metin",
                    Surname = "TAŞLIK",
                    Company = "X",
                    IsActive = true
                });
            context.SaveChanges();
            Assert.NotNull(await directoryRepository.GetDirectoryAsync(directory.Entity.Uuid));
        }

        [Fact]
        public async void UpdateDirectory_Should_SuccessAsync()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Directory, DirectoryDto>();
                cfg.CreateMap<Contact, ContactDto>();
            });
            configuration.AssertConfigurationIsValid();
            var mapper = configuration.CreateMapper();

            var options = new DbContextOptionsBuilder<RiseTechDBContext>()
                .UseInMemoryDatabase("ContactDatabase")
                .Options;

            Mock<IDirectoryService> directoryServiceMock = new Mock<IDirectoryService>();

            var context = new RiseTechDBContext(options);
            var directoryRepository = new DirectoryRepository(context);
            var directory = context.Add(
                new Directory
                {
                    Uuid = Guid.NewGuid(),
                    Name = "Metin",
                    Surname = "TAŞLIK",
                    Company = "X",
                    IsActive = true
                });
            context.SaveChanges();
            directory.Entity.Company = "Y";
            var test = await directoryRepository.UpdateDirectoryAsync(directory.Entity);
            Assert.Equal("Y", test.Company);
        }
    }
}