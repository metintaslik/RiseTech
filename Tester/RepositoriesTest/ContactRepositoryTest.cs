using Core.Services;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;
using Moq;
using AutoMapper;
using Tester.DTOs;
using Core.Repositories;

namespace Tester.RepositoriesTest
{
    public class ContactRepositoryTest
    {
        [Fact]
        public async void SaveContact_Should_SuccessAsync()
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
            Mock<IContactService> contactServiceMock = new Mock<IContactService>();

            var context = new RiseTechDBContext(options);
            var contactRepository = new ContactRepository(context);
            var directory = context.Directories.Add(
                new Directory
                {
                    Uuid = Guid.NewGuid(),
                    Name = "Metin",
                    Surname = "TAŞLIK",
                    Company = "X",
                    IsActive = true
                });
            var test = await contactRepository.AddContactAsync(
                new Contact
                {
                    PersonId = directory.Entity.Uuid,
                    Telephone = "5345881510",
                    Email = "metinn.taslik@gmail.com",
                    Location = "İstanbul",
                    IsActive = true
                });
            context.SaveChanges();
            Assert.Equal(1, test.Id);
        }

        [Fact]
        public async void UpdateContact_Should_SuccessAsync()
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
            Mock<IContactService> contactServiceMock = new Mock<IContactService>();

            var context = new RiseTechDBContext(options);
            var contactRepository = new ContactRepository(context);
            var directory = context.Directories.Add(
                new Directory
                {
                    Uuid = Guid.NewGuid(),
                    Name = "Metin",
                    Surname = "TAŞLIK",
                    Company = "X",
                    IsActive = true
                });
            var contact = context.Contacts.Add(
                new Contact
                {
                    PersonId = directory.Entity.Uuid,
                    Telephone = "5345881510",
                    Email = "metinn.taslik@gmail.com",
                    Location = "İstanbul",
                    IsActive = true
                });

            contact.Entity.IsActive = false;
            var test = await contactRepository.InactiveContactAsync(contact.Entity);
            context.SaveChanges();
            Assert.Equal(false, contact.Entity.IsActive);
        }
    }
}
