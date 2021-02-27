using AutoFixture.Xunit2;
using Core.Repositories;
using Core.Services;
using Data.Models;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tester.ServicesTest
{
    public class ContactServiceTest
    {
        [Theory, AutoData]
        public void CreateNewContact_Should_Success([Frozen] Mock<IContactService> repository, ContactRepository contactService, Contact contact)
        {
            repository.Setup(x => x.AddContactAsync(contact)).Returns(It.IsAny<Task<Contact>>());
            Action action = async () =>
            {
                await contactService.AddContactAsync(contact);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoData]
        public void DeleteContact_Should_Success([Frozen] Mock<IContactService> repository, ContactRepository contactService, Contact contact)
        {
            repository.Setup(x => x.DeleteContactAsync(contact)).Returns(It.IsAny<Task<bool>>());
            Action action = async () =>
            {
                await contactService.DeleteContactAsync(contact);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoData]
        public void GetContacts_Should_Success([Frozen] Mock<IContactService> repository, ContactRepository contactService, List<Contact> contacts)
        {
            repository.Setup(x => x.GetContacts()).Returns(It.IsAny<IEnumerable<Contact>>());
            Action action = () =>
            {
                var result = contactService.GetContacts();
                result.Count().Should().Be(contacts.Count);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoData]
        public void UpdateContact_Should_Success([Frozen] Mock<IContactService> repository, ContactRepository contactService, Contact contact)
        {
            repository.Setup(x => x.UpdateContactAsync(contact)).Returns(It.IsAny<Task<Contact>>());
            Action action = async () =>
            {
                await contactService.UpdateContactAsync(contact);
            };
            action.Should().NotThrow<Exception>();
        }
    }
}