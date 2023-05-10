using FluentAssertions;
using Notifyre;
using Notifyre.Infrastructure.Utils;
using Notifyre.Interfaces;
using Snapshooter.Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NotifyreTests.Services.Sms
{
    public class SmsServiceTests
    {
        private readonly IHttpHandler _HttpHandlerFake;

        public SmsServiceTests()
        {
            _HttpHandlerFake = new HttpHandlerFake();
        }

        /* ListSmsAsync example response for the unit test
         {
            "payload": {
                "smsMessages": [
                    {
                        "id": "2bdfff1a-461d-4b5c-b0bc-69af5535fc41",
                        "accountID": "AZ07NWWI",
                        "createdBy": "9d19715d-97d3-4152-950d-cd487bfffa8f",
                        "recipient": {
                            "id": "120a5a36-937c-47c0-8f2d-74d1ea06c012",
                            "toNumber": "+61477345123",
                            "fromNumber": "Shared Number (+61416906716)",
                            "cost": 0.08,
                            "messageParts": 1,
                            "costPerPart": 0.08,
                            "status": "queued",
                            "queuedDateUtc": 1635717825,
                            "completedDateUtc": null
                        },
                        "status": "queued",
                        "totalCost": 0,
                        "createdDateUtc": 1635717732,
                        "submittedDateUtc": 1635717738,
                        "completedDateUtc": null,
                        "lastModifiedDateUtc": 1635717852
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
        public async Task ListSentSmsAsync()
        {
            // Arrange
            var service = new SmsService(_HttpHandlerFake);

            // Act 
            var result = await service.ListSentSmsAsync(new ListSentSmsRequest());
            var actualSms = result.SmsMessages[0];

            // Assert
            actualSms.AccountID.Should().Be("AZ07NWWI"); // see above example response
            actualSms.CreatedBy.Should().Be("9d19715d-97d3-4152-950d-cd487bfffa8f");
            actualSms.CreatedDateUtc.Should().Be(1635717732);
            actualSms.SubmittedDateUtc.Should().Be(1635717738);
            actualSms.CompletedDateUtc.Should().BeNull();
            actualSms.LastModifiedDateUtc.Should().Be(1635717852);
            actualSms.Recipient.QueuedDateUtc.Should().Be(1635717825);
            actualSms.Recipient.ID.Should().Be(new Guid("120a5a36-937c-47c0-8f2d-74d1ea06c012"));
            actualSms.Recipient.ToNumber.Should().Be("+61477345123");
            actualSms.Recipient.Cost.Should().Be(0.08m);
            actualSms.Recipient.MessageParts.Should().Be(1);
            actualSms.Recipient.Status.Should().Be("queued");
            actualSms.Recipient.CompletedDateUtc.Should().BeNull();
            actualSms.Recipient.FromNumber.Should().Be("Shared Number (+61416906716)");
        }

        /* GetSentSmsAsync example response for the unit test
         {
            "payload": {
                "id": "2bdfff1a-461d-4b5c-b0bc-69af5535fc41",
                "accountID": "AZ07NWWI",
                "createdBy": "9d19715d-97d3-4152-950d-cd487bfffa8f",
                "recipient": {
                    "id": "120a5a36-937c-47c0-8f2d-74d1ea06c012",
                    "toNumber": "+61477345123",
                    "fromNumber":"Shared Number (+61416906716)",
                    "message": "test message",
                    "cost": 0.08,
                    "messageParts": 1,
                    "costPerPart": 0.08,
                    "status": "queued",
                    "queuedDateUtc": 1630541580,
                    "completedDateUtc": null
                },
                "status": "queued",
                "totalCost": 0,
                "createdDateUtc": 1630541580,
                "submittedDateUtc": 1630541581,
                "completedDateUtc": null,
                "lastModifiedDateUtc": 1630541581
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task GetSentSmsAsync_ValidInput_ReturnsOk()
        {
            // Arrange
            var messageId = new Guid("2bdfff1a-461d-4b5c-b0bc-69af5535fc41"); // see above example response
            var request = new GetSentSmsRequest()
            {
                MessageID = messageId,
            };
            var service = new SmsService(_HttpHandlerFake);

            // Act 
            var result = await service.GetSentSmsAsync(request);

            // Assert
            result.Status.Should().Be("queued");
            result.ID.Should().Be(messageId);
            result.AccountID.Should().Be("AZ07NWWI");
            result.SubmittedDateUtc.Should().Be(1635717738);
            result.CreatedDateUtc.Should().Be(1635717732);
            result.LastModifiedDateUtc.Should().Be(1635717852);
            result.CompletedDateUtc.Should().BeNull();
            result.Recipients[0].ID.Should().Be("120a5a36-937c-47c0-8f2d-74d1ea06c012");
            result.Recipients[0].ToNumber.Should().Be("+61477345123");
            result.Recipients[0].QueuedDateUtc.Should().Be(1630541580);
            result.Recipients[0].FromNumber.Should().Be("Shared Number (+61416906716)");
        }

        [Fact]
        public async Task GetSentSmsRecipientAsync_ValidInput_ReturnsOk()
        {
            // Arrange
            var messageId = new Guid("2bdfff1a-461d-4b5c-b0bc-69af5535fc41"); // see above example response
            var recipientID = new Guid("120a5a36-937c-47c0-8f2d-74d1ea06c012");
            var request = new GetSentSmsRecipientRequest()
            {
                MessageID = messageId,
                RecipientID = recipientID
            };
            var service = new SmsService(_HttpHandlerFake);

            // Act 
            var result = await service.GetSentSmsRecipientAsync(request);

            // Assert
            result.Status.Should().Be("queued");
            result.ID.Should().Be(messageId);
            result.AccountID.Should().Be("AZ07NWWI");
            result.SubmittedDateUtc.Should().Be(1630541581);
            result.CreatedDateUtc.Should().Be(1630541580);
            result.CompletedDateUtc.Should().BeNull();
            result.Recipient.ID.Should().Be(recipientID);
            result.Recipient.ToNumber.Should().Be("+61477345123");
            result.Recipient.QueuedDateUtc.Should().Be(1630541580);
            result.Recipient.Message.Should().Be("test message");
            result.Recipient.FromNumber.Should().Be("Shared Number (+61416906716)");
        }

        /* SubmitSmsAsync example response for the unit test
         {
            "payload": {
                "smsMessageID": "c6e52a7d-9787-44d2-ac52-b03940b1c1fb",
                "invalidToNumbers": null
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task SubmitSmsAsync_ValidInput_ReturnsOk()
        {
            // Arrange
            var messageId = new Guid("c6e52a7d-9787-44d2-ac52-b03940b1c1fb"); // see above example response
            var request = new SubmitSmsRequest()
            {
                From = null,
                Recipients = new List<SmsRecipient>()
                {
                    new SmsRecipient()
                    {
                        Type = SubmitSmsRequestRecipientType.MobileNumber,
                        Value = "+61477245453"
                    }
                },
                Body = "message from a unit test",
                ScheduledDate = new DateTime(2021, 09, 29, 14, 15, 23).ToUnixTimeStamp(),
                Metadata = new Dictionary<string, string>
                {
                    {"TestKey", "TestVal"},
                    {"TestKey1", "TestVal1"}
                }
            };
            var service = new SmsService(_HttpHandlerFake);

            // Act 
            var result = await service.SubmitSmsAsync(request);

            // Assert
            result.SmsMessageID.Should().Be(messageId.ToString());
            result.InvalidToNumbers.Should().BeNull();
        }

        /* ListSmsRepliesAsync example unexpected xml response for a unit test
             <?xml version="1.0" ?>
            <GetCapabilities
               service="WFS"
               version="1.0.0"
               xmlns="http://www.opengis.net/wfs" />
        */
        [Fact]
        public async Task ListSmsRepliesAsync_ValidInput_ReturnsInvalidResponseShouldFailGracefully()
        {
            // Arrange
            var request = new ListSmsRepliesRequest()
            {
                Sort = ListSmsRepliesRequestSortTypes.Desc
            };
            var service = new SmsService(_HttpHandlerFake);

            // Act 
            Func<Task> act = () => service.ListSmsRepliesAsync(request);

            // Assert
            await act.Should()
                .ThrowAsync<NotifyreException>()
                .WithMessage("Response is not in valid JSON format");
        }

        /* ListSmsRepliesAsync example response for a unit test
            {
	            "payload": {
		            "smsReplies": [
			            {
				            "recipientID": "09645834-7cdf-46e1-95bf-64e2547a5de5",
				            "recipientNumber": "+61416906715",
				            "senderNumber": "+61477789874",
				            "replyDetails": [
					            {
						            "replyID": "7cc692b0-5e78-4b59-a45d-c147b61afd95",
						            "externalReplyID": "1a395e67-c4fa-461c-8dfa-0249cf690071",
						            "provider": "voicehub",
						            "receivedDateUtc": 1654053477,
						            "createdDateUtc": 1654053479
					            },
					            {
						            "replyID": "d2329dbe-a2e9-4b84-bd59-2dc7724954dd",
						            "externalReplyID": "76dfd732-be0a-4440-bb9a-f4c34ae84c6e",
						            "provider": "voicehub",
						            "receivedDateUtc": 1654053670,
						            "createdDateUtc": 1654053672
					            }
				            ],
				            "createdDateUtc": 1654053479,
				            "contactDetails": {
					            "firstName": "Mike",
					            "lastName": "Z",
					            "organization": "Test GoLogic"
				            }
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
        public async Task ListSmsRepliesAsync_ValidInput_ReturnsOk()
        {
            // Arrange
            var request = new ListSmsRepliesRequest()
            {
                FromDate = new DateTime(2021, 09, 20).ToUnixTimeStamp()
            };
            var service = new SmsService(_HttpHandlerFake);

            // Act 
            var result = await service.ListSmsRepliesAsync(request);
            var actual = result.SmsReplies[0];

            // Assert
            result.SmsReplies.Count.Should().Be(1);
            actual.RecipientID.Should().Be(new Guid("09645834-7cdf-46e1-95bf-64e2547a5de5"));
            actual.RecipientNumber.Should().Be("+61416906715");
            actual.SenderNumber.Should().Be("+61477789874");
            actual.ReplyID.Should().Be("067f11c0-38a5-402e-b060-8863ed263555");
            actual.ReceivedDateUtc.Should().Be(1654053477);
            actual.ContactDetails.FirstName.Should().Be("Go");
            actual.ContactDetails.LastName.Should().Be("logic");
            actual.ContactDetails.Organization.Should().Be("Test GoLogic");
        }

        /* GetSmsReplyAsync example response for a unit test
            {
                "payload": {
                    "recipientID": "baf0be23-f102-48dd-90f5-2183c19cf890",
                    "recipientNumber": "+61416906715",
                    "senderNumber": "+61477789879",
                    "replyID": "a3a1f58f-c54b-4c49-a9ae-0e0f8f11550a",
                    "message": "Gologic reply 1",
                    "receivedDateUtc": 1635717854
                },
                "success": true,
                "statusCode": 200,
                "message": "OK",
                "errors": []
            }
        */
        [Fact]
        public async Task GetSmsReplyAsync_ValidInput_ReturnsOk()
        {
            // Arrange
            var request = new GetSmsReplyRequest()
            {
                ReplyID = "a3a1f58f-c54b-4c49-a9ae-0e0f8f11550a"
            };
            var service = new SmsService(_HttpHandlerFake);

            // Act 
            var result = await service.GetSmsReplyAsync(request);

            // Assert
            result.RecipientID.Should().Be(new Guid("baf0be23-f102-48dd-90f5-2183c19cf890"));
            result.RecipientNumber.Should().Be("+61416906715");
            result.SenderNumber.Should().Be("+61477789879");
            result.ReplyID.Should().Be("a3a1f58f-c54b-4c49-a9ae-0e0f8f11550a");
            result.Message.Should().Be("Gologic reply 1");
            result.ReceivedDateUtc.Should().Be(1635717854);
        }

        /*{
          "SmsNumbers": [
            {
              "ID": "39be4fb9-a875-468a-904d-c52d4dad21ee",
              "CountryCode": 61,
              "AssignedNumber": 416234582,
              "E164": "+61416234582",
              "Provider": "AATP",
              "Status": "active",
              "SubscriptionID": "616bdf0a9400548a177ccd47",
              "CreatedDateUtc": null,
              "LastModifiedDateUtc": 1634459402,
              "StartDateUtc": 1634392800,
              "FinishDateUtc": null
            }
          ],
          "SmsSenderIds": [
            {
              "ID": "18194c53-a577-4c25-ba48-0cf4fd3054d4",
              "Name": "SenderId",
              "Status": "active",
              "CreatedDateUtc": 1632727486,
              "LastModifiedDateUtc": 1632727486
            }
          ]
        }*/
        [Fact]
        public async Task ListFaxNumbers_ValidInput_ReturnsOk()
        {
            // Arrange
            var expected = new ListSmsNumbersResponse
            {
                SmsNumbers = new List<ListSmsNumbersResponse.SmsNumberDto>
                {
                    new()
                    {
                        ID = new Guid("39be4fb9-a875-468a-904d-c52d4dad21ee"),
                        CountryCode = 61,
                        AssignedNumber = 416234582,
                        E164 = "+61416234582",
                        Provider = "AATP",
                        Status = "active",
                        SubscriptionID = "616bdf0a9400548a177ccd47",
                        CreatedDateUtc = null,
                        LastModifiedDateUtc = 1634459402,
                        StartDateUtc = 1634392800,
                        FinishDateUtc = null
                    }
                },
                SmsSenderIds = new List<ListSmsNumbersResponse.SmsSenderIdDto>
                {
                    new()
                    {
                        ID = new Guid("18194c53-a577-4c25-ba48-0cf4fd3054d4"),
                        Name = "SenderId",
                        Status = "active",
                        CreatedDateUtc = 1632727486,
                        LastModifiedDateUtc = 1632727486
                    }
                }
            };

            var service = new SmsService(_HttpHandlerFake);

            // Act 
            var result = await service.ListSmsNumbersAsync();

            // Assert
            result.Should().BeEquivalentTo(expected);
            result.MatchSnapshot(); // see above example response or Sms _snapshots_ folder
        }
    }
}
