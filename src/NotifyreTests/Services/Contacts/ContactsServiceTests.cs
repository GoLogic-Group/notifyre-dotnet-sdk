using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Notifyre;
using Notifyre.Interfaces;
using System.Threading.Tasks;

namespace NotifyreTests.Services.Contacts
{
    public class ContactsServiceTests
    {
        private readonly IHttpHandler _HttpHandlerFake;

        public ContactsServiceTests()
        {
            _HttpHandlerFake = new HttpHandlerFake();
        }

        /* AddContactsToGroups fake response for the unit test
         {
            "payload": {
                "added": true
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task AddContactsToGroups_ValidRequest_ReturnsOk()
        {
            // Arrange
            var service = new ContactsService(_HttpHandlerFake);
            var request = new AddContactsToGroupsRequest()
            {
                Contacts = new List<Guid>()
                {
                    new Guid("1ff6263a-23b9-4de5-bdf3-d83d99bb43c7"),
                    new Guid("1d64404c-6b8e-45ce-809b-ee05fa0f377f"),
                },
                Groups = new List<Guid>()
                {
                    new Guid("9735311e-f160-45d0-b05f-7a5626cb1d22")
                }
            };

            // Act 
            var result = await service.AddContactsToGroups(request);

            // Assert
            result.Added.Should().BeTrue();
        }

        /* CreateContactAsync example response for the unit test
         {
            "payload": {
                "id": "2e759a5f-ee8b-443d-ae2e-3e7832e6a1ff",
                "firstName": "Pascal",
                "lastName": "Baltes",
                "organization": "Gologic Group",
                "email": "pascal.baltes@gologic.com.au",
                "faxNumber": "\u002B61711111111",
                "mobileNumber": "\u002B61477245453",
                "groups": [{
                    "id": "c0333f6d-0d14-4050-a84d-15b9dc47df2c",
                    "name": "Gologic Devs",
                    "createdDateUtc": 1630454400
                }],
                "customFields": [{
                    "id": "8ff84b68-8d3e-4bff-b28e-81ccbf9e4ca0",
                    "key": "cf1",
                    "value": "Joined 05.2021"
                }, {
                    "id": "c14db300-5ef5-412c-871d-ce7e08a77bdc",
                    "key": "cf2",
                    "value": "Likes Chocolate"
                }]
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task CreateContactAsync_ValidInput_ReturnsOk()
        {
            // Arrange
            var service = new ContactsService(_HttpHandlerFake);
            var request = new CreateContactRequest()
            {
                CustomFields = new List<CustomField>()
                {
                    new CustomField()
                    {
                        Key = "cf1",
                        Value = "Joined 05.2021"
                    },
                    new CustomField()
                    {
                        Key = "cf2",
                        Value = "Likes Chocolate"
                    }
                },
                FirstName = "Pascal",
                LastName = "Baltes",
                Organization = "Gologic Group",
                Email = "pascal.baltes@gologic.com.au",
                FaxNumber = "+61711111111",
                MobileNumber = "+61477245453",
                GroupName = "Gologic Devs"
            };

            // Act 
            var result = await service.CreateContactAsync(request);

            // Assert
            result.ID.Should().Be(new Guid("2e759a5f-ee8b-443d-ae2e-3e7832e6a1ff")); // see above example response
            result.FirstName.Should().Be(request.FirstName);
            result.LastName.Should().Be(request.LastName);
            result.Organization.Should().Be(request.Organization);
            result.Email.Should().Be(request.Email);
            result.FaxNumber.Should().Be(request.FaxNumber);
            result.MobileNumber.Should().Be(request.MobileNumber);
            result.Groups.Should().BeEquivalentTo(new List<CreateContactResponse.GroupDto>() {
                new CreateContactResponse.GroupDto()
                {
                    ID = new Guid("c0333f6d-0d14-4050-a84d-15b9dc47df2c"),
                    Name = request.GroupName,
                    CreatedDateUtc = 1630454400
                }
            });
            result.CustomFields.Should().BeEquivalentTo(new List<CreateContactResponse.CustomFieldDto>()
            {
                new CreateContactResponse.CustomFieldDto()
                {
                    ID = new Guid("8ff84b68-8d3e-4bff-b28e-81ccbf9e4ca0"),
                    Key = request.CustomFields[0].Key,
                    Value = request.CustomFields[0].Value,
                },
                new CreateContactResponse.CustomFieldDto()
                {
                    ID = new Guid("c14db300-5ef5-412c-871d-ce7e08a77bdc"),
                    Key = request.CustomFields[1].Key,
                    Value = request.CustomFields[1].Value,
                }
            });
        }

        /* CreateContactAsync example response for the unit test
         {
            "payload": {
                "id": "2e759a5f-ee8b-443d-ae2e-3e7832e6a1ff",
                "lastName": "Baltes",
                "organization": "breaking changes inc.",
                "email": "pascal.baltes@gologic.com.au",
                "faxNumber": "\u002B61711111111",
                "mobileNumber": "\u002B61477245453",
                "groups": [{
                    "id": "c0333f6d-0d14-4050-a84d-15b9dc47df2c",
                    "name": "Gologic Devs",
                    "createdDateUtc": 1630454401
                }],
                "customFields": [{
                    "id": "8ff84b68-8d3e-4bff-b28e-81ccbf9e4ca0",
                    "key": "cf1",
                    "customValue": "Joined 05.2021"
                }, {
                    "id": "c14db300-5ef5-412c-871d-ce7e08a77bdc",
                    "key": "cf2",
                    "customValue": "Likes Chocolate"
                }]
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task CreateContactAsync_ValidInput_ReturnsResponseWithMissingProperty()
        {
            // Arrange
            var service = new ContactsService(_HttpHandlerFake);
            var request = new CreateContactRequest()
            {
                CustomFields = new List<CustomField>()
                {
                    new CustomField()
                    {
                        Key = "cf1",
                        Value = "Joined 05.2021"
                    },
                    new CustomField()
                    {
                        Key = "cf2",
                        Value = "Likes Chocolate"
                    }
                },
                FirstName = "Pascal",
                LastName = "Baltes",
                Organization = "breaking changes inc.",
                Email = "pascal.baltes@gologic.com.au",
                FaxNumber = "+61711111111",
                MobileNumber = "+61477245453",
                GroupName = "Throw"
            };

            // Act 
            Func<Task<CreateContactResponse>> act = async () => await service.CreateContactAsync(request);

            // Assert
            await act.Should().ThrowAsync<NotifyreException>().Where(e => e.Message.Contains("The response could not be deserialized"));
        }

        /* CreateGroupAsync example response for the unit test
         {
            "payload": {
                "id": "725ed91e-d7ff-43a3-b8fb-6e0138be1545",
                "name": "my group",
                "createdDateUtc": 1630454402
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task CreateGroupAsync_ValidInput_ReturnsOk()
        {
            // Arrange
            var service = new ContactsService(_HttpHandlerFake);
            var request = new CreateGroupRequest()
            {
                Name = "my group"
            };

            // Act 
            var result = await service.CreateGroupAsync(request);

            // Assert
            result.ID.Should().Be(new Guid("725ed91e-d7ff-43a3-b8fb-6e0138be1545")); // see above example response
            result.Name.Should().Be(request.Name);
            result.CreatedDateUtc.Should().Be(1630454402);
        }

        /* DeleteContactsAsync example response for the unit test
         {
            "payload": {
                "deleted": true
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task DeleteContactsAsync_ValidInput_ReturnsOk()
        {
            // Arrange
            var service = new ContactsService(_HttpHandlerFake);
            var request = new DeleteContactsRequest()
            {
                Contacts = new List<Guid>()
                {
                    Guid.NewGuid()
                }
            };

            // Act 
            var result = await service.DeleteContactsAsync(request);

            // Assert
            result.Deleted.Should().BeTrue();
        }

        /* DeleteGroupsAsync example response for the unit test
         {
            "payload": {
                "deleted": true
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task DeleteGroupsAsync_ValidInput_ReturnsOk()
        {
            // Arrange
            var service = new ContactsService(_HttpHandlerFake);
            var request = new DeleteGroupsRequest()
            {
                Groups = new List<Guid>()
                {
                    Guid.NewGuid()
                }
            };

            // Act 
            var result = await service.DeleteGroupsAsync(request);

            // Assert
            result.Deleted.Should().BeTrue();
        }

        /* DeleteGroupsAsync fake response for the unit test
         {
            "payload": {
                "hasDeleted": true
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task DeleteGroupsAsync_InvalidHttpResponse_ReturnsResponseWithRenamedProperty()
        {
            // Arrange
            var service = new ContactsService(_HttpHandlerFake);
            var request = new DeleteGroupsRequest()
            {
                Groups = new List<Guid>()
                {
                    Guid.Empty
                }
            };

            // Act 
            Func<Task<DeleteGroupsResponse>> act = async () => await service.DeleteGroupsAsync(request);

            // Assert
            await act.Should().ThrowAsync<NotifyreException>().Where(e => e.Message.Contains("The response could not be deserialized"));
        }

        /* GetContactAsync fake response for the unit test
         {
            "payload": {
                "id": "1ff6263a-23b9-4de5-bdf3-d83d99bb43c7",
                "firstName": "Mike",
                "lastName": "Contact",
                "organization": "gologic",
                "email": "mike.z@g.com",
                "faxNumber": "+61711111111",
                "mobileNumber": "+61415151234",
                "groups": [{
                        "id": "8e39ef8e-11fe-4d65-99bb-0c7927bac0d8",
                        "name": "another",
                        "createdDateUtc": 1630454403
                    },
                    {
                        "id": "7509a3ca-a833-48a2-bd2e-ce0b5f034ded",
                        "name": "gooooooy",
                        "createdDateUtc": 1630454404
                    }
                ],
                "customFields": [
                    {
                        "id": "063ef468-b530-4aca-85a9-179e491acf31",
                        "key": "cf1",
                        "Value": "Joined 2021"
                    }
                ]
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task GetContactAsync_ValidRequest_ReturnsOk()
        {
            // Arrange
            var service = new ContactsService(_HttpHandlerFake);
            var request = new GetContactRequest()
            {
                ContactID = new Guid("1ff6263a-23b9-4de5-bdf3-d83d99bb43c7")
            };

            // Act 
            var result = await service.GetContactAsync(request);

            // Assert
            result.ID.Should().Be(new Guid("1ff6263a-23b9-4de5-bdf3-d83d99bb43c7")); // see example request above
            result.FirstName.Should().Be("Mike");
            result.LastName.Should().Be("Contact");
            result.Organization.Should().Be("gologic");
            result.Email.Should().Be("mike.z@g.com");
            result.FaxNumber.Should().Be("+61711111111");
            result.MobileNumber.Should().Be("+61415151234");
            result.Groups.Should().BeEquivalentTo(
                new List<GetContactResponse.GroupDto>()
                {
                    new GetContactResponse.GroupDto()
                    {
                        ID = new Guid("8e39ef8e-11fe-4d65-99bb-0c7927bac0d8"),
                        Name = "another",
                        CreatedDateUtc = 1630454403
                    },
                    new GetContactResponse.GroupDto()
                    {
                        ID = new Guid("7509a3ca-a833-48a2-bd2e-ce0b5f034ded"),
                        Name = "gooooooy",
                        CreatedDateUtc = 1630454404
                    }
                }
            );
            result.CustomFields.Should().BeEquivalentTo(
                new List<GetContactResponse.CustomFieldDto>()
                {
                    new GetContactResponse.CustomFieldDto()
                    {
                        ID = new Guid("063ef468-b530-4aca-85a9-179e491acf31"),
                        Key = "cf1",
                        Value = "Joined 2021",
                    }
                }
            );
        }

        /* ListContactsAsync fake response for the unit test
         {
            "payload": {
                "totalCount": 1,
                "contacts": [
                    {
                        "id": "1ff6263a-23b9-4de5-bdf3-d83d99bb43c7",
                        "firstName": "Mike",
                        "lastName": "Contact",
                        "organization": "gologic",
                        "email": "mike.z@g.com",
                        "faxNumber": "+61711111111",
                        "mobileNumber": "+61415151234",
                        "groups": [
                            {
                                "id": "8e39ef8e-11fe-4d65-99bb-0c7927bac0d8",
                                "name": "another",
                                "createdDateUtc": 1630454405
                            },
                            {
                                "id": "7509a3ca-a833-48a2-bd2e-ce0b5f034ded",
                                "name": "gooooooy",
                                "createdDateUtc": 1630454406
                            }
                        ],
                        "customFields": [
                            {
                                "id": "063ef468-b530-4aca-85a9-179e491acf31",
                                "key": "cf1",
                                "Value": "Joined 2021"
                            }
                        ]
                    }
                ]
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task ListContactsAsync_ValidRequest_ReturnsOk()
        {
            // Arrange
            var service = new ContactsService(_HttpHandlerFake);
            var request = new ListContactsRequest()
            {
                Type = ListGroupsContactNumberType.FaxNumber
            };

            // Act 
            var result = await service.ListContactsAsync(request);
            var actual = result.Contacts[0];

            // Assert
            result.TotalCount.Should().Be(1);
            actual.ID.Should().Be(new Guid("1ff6263a-23b9-4de5-bdf3-d83d99bb43c7")); // see example request above
            actual.FirstName.Should().Be("Mike");
            actual.LastName.Should().Be("Contact");
            actual.Organization.Should().Be("gologic");
            actual.Email.Should().Be("mike.z@g.com");
            actual.FaxNumber.Should().Be("+61711111111");
            actual.MobileNumber.Should().Be("+61415151234");
            actual.Groups.Should().BeEquivalentTo(
                new List<GetContactResponse.GroupDto>()
                {
                    new GetContactResponse.GroupDto()
                    {
                        ID = new Guid("8e39ef8e-11fe-4d65-99bb-0c7927bac0d8"),
                        Name = "another",
                        CreatedDateUtc = 1630454405
                    },
                    new GetContactResponse.GroupDto()
                    {
                        ID = new Guid("7509a3ca-a833-48a2-bd2e-ce0b5f034ded"),
                        Name = "gooooooy",
                        CreatedDateUtc = 1630454406
                    }
                }
            );
            actual.CustomFields.Should().BeEquivalentTo(
                new List<GetContactResponse.CustomFieldDto>()
                {
                    new GetContactResponse.CustomFieldDto()
                    {
                        ID = new Guid("063ef468-b530-4aca-85a9-179e491acf31"),
                        Key = "cf1",
                        Value = "Joined 2021",
                    }
                }
            );

        }

        /* ListGroupsAsync fake response for the unit test
         {
            "payload": {
                "groups": [{
                    "id": null,
                    "name": "All Contacts",
                    "createdDateUtc": null,
                    "contactsCount": 2
                }, {
                    "id": "9735311e-f160-45d0-b05f-7a5626cb1d22",
                    "name": "another",
                    "createdDateUtc": 1630454407,
                    "contactsCount": 2
                }]
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task ListGroupsAsync_ValidRequest_ReturnsOk()
        {
            // Arrange
            var service = new ContactsService(_HttpHandlerFake);
            var request = new ListGroupsRequest();

            // Act 
            var result = await service.ListGroupsAsync(request);
            var firstGroup = result.Groups[0];
            var secondGroup = result.Groups[1];

            // Assert
            result.Groups.Count.Should().Be(2);

            firstGroup.ID.Should().BeNull();
            firstGroup.Name.Should().Be("All Contacts");
            firstGroup.CreatedDateUtc.Should().BeNull();
            firstGroup.TotalContacts.Should().Be(2);

            secondGroup.ID.Should().Be(new Guid("9735311e-f160-45d0-b05f-7a5626cb1d22"));
            secondGroup.Name.Should().Be("another");
            secondGroup.CreatedDateUtc.Should().Be(1630454407);
            secondGroup.TotalContacts.Should().Be(2);
        }

        /* ListGroupsAsync fake response for the unit test
         {
            "payload": {
                "removed": true
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task RemoveContactsFromGroupAsync_ValidRequest_ReturnsOk()
        {
            // Arrange
            var service = new ContactsService(_HttpHandlerFake);
            var request = new RemoveContactsFromGroupRequest() { 
                Contacts = new List<Guid> { Guid.NewGuid() }, 
                GroupID = Guid.NewGuid() 
            };

            // Act 
            var result = await service.RemoveContactsFromGroupAsync(request);

            // Assert
            result.Removed.Should().BeTrue();
        }

        /* UpdateContactAsync fake response for the unit test
         {
            "payload": {
                "id": "2e759a5f-ee8b-443d-ae2e-3e7832e6a1ff",
                "firstName": "Pascal",
                "lastName": "Baltes",
                "organization": "Gologic Group",
                "email": "pascal.baltes@gologic.com.au",
                "faxNumber": "\u002B61711111111",
                "mobileNumber": "\u002B61477245453",
                "groups": [{
                    "id": "c0333f6d-0d14-4050-a84d-15b9dc47df2c",
                    "name": "Gologic Devs",
                    "createdDateUtc": 1630454408
                }],
                "customFields": [{
                    "key": "cf1",
                    "value": "Joined 05.2021"
                }, {
                    "key": "cf2",
                    "value": "Likes Chocolate"
                }]
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task UpdateContactAsync_ValidRequest_ReturnsOk()
        {
            // Arrange
            var service = new ContactsService(_HttpHandlerFake);
            var request = new UpdateContactRequest()
            {
                CustomFields = new List<CustomField>()
                {
                    new CustomField()
                    {
                        Key = "cf1",
                        Value = "Joined 05.2021"
                    },
                    new CustomField()
                    {
                        Key = "cf2",
                        Value = "Likes Chocolate"
                    }
                },
                FirstName = "Pascal",
                LastName = "Baltes",
                Organization = "Gologic Group",
                Email = "pascal.baltes@gologic.com.au",
                FaxNumber = "+61711111111",
                MobileNumber = "+61477245453"
            };

            // Act 
            var result = await service.UpdateContactAsync(request);

            // Assert
            result.ID.Should().Be(new Guid("2e759a5f-ee8b-443d-ae2e-3e7832e6a1ff")); // see above example response
            result.FirstName.Should().Be(request.FirstName);
            result.LastName.Should().Be(request.LastName);
            result.Organization.Should().Be(request.Organization);
            result.Email.Should().Be(request.Email);
            result.FaxNumber.Should().Be(request.FaxNumber);
            result.MobileNumber.Should().Be(request.MobileNumber);
            result.Groups.Should().BeEquivalentTo(new List<UpdateContactResponse.GroupDto>() {
                new UpdateContactResponse.GroupDto()
                {
                    ID = new Guid("c0333f6d-0d14-4050-a84d-15b9dc47df2c"),
                    Name = "Gologic Devs",
                    CreatedDateUtc = 1630454408
                }
            });
            result.CustomFields.Should().BeEquivalentTo(new List<UpdateContactResponse.CustomFieldDto>()
            {
                new UpdateContactResponse.CustomFieldDto()
                {
                    Key = request.CustomFields[0].Key,
                    Value = request.CustomFields[0].Value,
                },
                new UpdateContactResponse.CustomFieldDto()
                {
                    Key = request.CustomFields[1].Key,
                    Value = request.CustomFields[1].Value,
                }
            });
        }

        /* UpdateGroupAsync fake response for the unit test
         {
            "payload": {
                "id": "725ed91e-d7ff-43a3-b8fb-6e0138be1545",
                "name": "my group",
                "createdDateUtc": 1630454409
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task UpdateGroupAsync_ValidRequest_ReturnsOk()
        {
            // Arrange
            var service = new ContactsService(_HttpHandlerFake);
            var request = new UpdateGroupRequest();

            // Act 
            var result = await service.UpdateGroupAsync(request);

            // Assert
            result.ID.Should().Be(new Guid("725ed91e-d7ff-43a3-b8fb-6e0138be1545")); // see above example response
            result.Name.Should().Be("my group");
            result.CreatedDateUtc.Should().Be(1630454409);
        }
    }
}
