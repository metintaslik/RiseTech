using AutoFixture.Xunit2;
using Core.Repositories;
using Core.Services;
using Data.Models;
using FluentAssertions;
using Moq;
using System;
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
        public void UpdateContact_Should_Success([Frozen] Mock<IContactService> repository, ContactRepository contactService, Contact contact)
        {
            repository.Setup(x => x.InactiveContactAsync(contact)).Returns(It.IsAny<Task<Contact>>());
            Action action = async () =>
            {
                await contactService.InactiveContactAsync(contact);
            };
            action.Should().NotThrow<Exception>();
        }
    }
}