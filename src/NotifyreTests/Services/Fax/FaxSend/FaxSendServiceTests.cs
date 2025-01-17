﻿using System;
using Xunit;
using FluentAssertions;
using Notifyre;
using Notifyre.Interfaces;
using System.Threading.Tasks;
using Notifyre.Services.Fax.FaxSend;
using Notifyre.Infrastructure.Utils;
using System.Collections.Generic;

namespace NotifyreTests.Services.Fax.FaxSend
{
    public class FaxSendServiceTests
    {
        private readonly IHttpHandler _HttpHandlerFake;

        public FaxSendServiceTests()
        {
            _HttpHandlerFake = new HttpHandlerFake();
        }

        /* ListSentFaxesAsync example response for the unit test
         {
            "payload": {
                "faxes": [{
                    "id": "7155ef1a-c7ff-42bb-b2c8-71ccbfe31ee3",
                    "recipientID": "9aca0071-2b61-4beb-bad2-a3ec8ce611e5",
                    "fromNumber": "61291989589",
                    "to": "\u002B61711111111",
                    "reference": "test fax",
                    "createdDateUtc": 1630454410,
                    "queuedDateUtc": 1630454411,
                    "lastModifiedDateUtc": 1630454412,
                    "highQuality": false,
                    "pages": 4,
                    "status": "completed"
                }, {
                    "id": "48a626f9-45cb-4dc4-8a50-f8c9c2d0caa6",
                    "recipientID": "532324e8-9dc6-48c1-8a1f-319e6e814cee",
                    "fromNumber": "61291989589",
                    "to": "\u002B61745612378",
                    "reference": "test fax for dev app",
                    "createdDateUtc": 1630454413,
                    "queuedDateUtc": 1630454414,
                    "lastModifiedDateUtc": 1630454415,
                    "highQuality": false,
                    "pages": 4,
                    "status": "queued"
                }],
                "total": 10
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task ListSentFaxesAsync_ValidInput_ReturnsOk()
        {
            // Arrange
            var service = new FaxSendService(_HttpHandlerFake);
            var request = new ListSentFaxesRequest()
            {
                FromDate = new DateTime(2021, 09, 24).ToUnixTimeStamp()
            };

            // Act 
            var result = await service.ListSentFaxesAsync(request);
            var firstSentFax = result.Faxes[0];
            var secondSentFax = result.Faxes[1];

            // Assert
            result.Faxes.Count.Should().Be(2);
            firstSentFax.ID.Should().Be("7155ef1a-c7ff-42bb-b2c8-71ccbfe31ee3");  // see above example response
            firstSentFax.RecipientID.Should().Be("9aca0071-2b61-4beb-bad2-a3ec8ce611e5");
            firstSentFax.FromNumber.Should().Be("Shared Number (+61291989589)");
            firstSentFax.To.Should().Be("+61711111111");
            firstSentFax.Reference.Should().Be("test fax");
            firstSentFax.CreatedDateUtc.Should().BeGreaterThan(0);
            firstSentFax.QueuedDateUtc.Should().BeGreaterThan(0);
            firstSentFax.LastModifiedDateUtc.Should().BeGreaterThan(0);
            firstSentFax.HighQuality.Should().BeFalse();
            firstSentFax.Pages.Should().Be(4);
            firstSentFax.Status.Should().Be("successful");

            secondSentFax.ID.Should().Be("48a626f9-45cb-4dc4-8a50-f8c9c2d0caa6");
            secondSentFax.RecipientID.Should().Be("532324e8-9dc6-48c1-8a1f-319e6e814cee");
            secondSentFax.FromNumber.Should().Be("Shared Number (+61291989589)");
            secondSentFax.To.Should().Be("+61745612378");
            secondSentFax.Reference.Should().Be("test fax for dev app");
            secondSentFax.CreatedDateUtc.Should().BeGreaterThan(0);
            secondSentFax.QueuedDateUtc.Should().BeGreaterThan(0);
            secondSentFax.LastModifiedDateUtc.Should().BeGreaterThan(0);
            secondSentFax.HighQuality.Should().BeFalse();
            secondSentFax.Pages.Should().Be(4);
            secondSentFax.Status.Should().Be("queued");

            result.Total.Should().Be(2);
        }

        /* DownloadSentFaxAsync example response for the unit test
         {
            "payload": {
                "base64Str": "xyz"
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task DownloadSentFaxAsync_ValidInput_ReturnsOk()
        {
            // Arrange
            var service = new FaxSendService(_HttpHandlerFake);
            var request = new DownloadFaxRequest()
            {
                FileType = DownloadFaxRequestFileTypes.Pdf,
                RecipientID = new Guid("9aca0071-2b61-4beb-bad2-a3ec8ce611e5")
            };

            // Act 
            var result = await service.DownloadSentFaxAsync(request);

            // Assert
            result.Base64Str.Should().Be("xyz");  // see above example response

        }

        /* SubmitFaxAsync example response for the unit test
         {
            "payload": {
                "faxID": "66e00a07-4cc9-4380-8943-395162eac8e1"
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task SubmitFaxAsync_ValidInput_ReturnsOk()
        {
            // Arrange
            var service = new FaxSendService(_HttpHandlerFake);
            var request = new SubmitFaxRequest()
            {
                TemplateName = "template",
                Faxes = new SubmitFax()
                {
                    Recipients = new List<FaxRecipient>()
                    {
                        new FaxRecipient()
                        {
                            Type = SubmitFaxRequestRecipientValueTypes.FaxNumber,
                            Value = "+61212345678"
                        }
                    },
                    ScheduledDate = new DateTime(2021, 04, 1, 14, 23, 12).ToUnixTimeStamp(),
                    SenderID = null,
                    SendFrom = "+61212345677",
                    Subject = "my subject",
                    ClientReference = "client reference",
                    Documents = new List<SubmittedDocument>()
                    {
                        new SubmittedDocument()
                        {
                            Base64Str = "abc",
                            ContentType = SubmitFaxRequestContentTypes.Rtf
                        }
                    },
                    Header = "header",
                    IsHighQuality = true,
                    CampaignName = "test"
                }
            };

            // Act
            var result = await service.SubmitFaxAsync(request);

            // Assert
            result.FaxID.Should().Be("66e00a07-4cc9-4380-8943-395162eac8e1"); // 
        }

        /* ListCoverPagesAsync example response for the unit test
        {
            "payload": [{
                "name": "simple template",
                "html": "\u003Cp\u003Emy cover page\u003C/p\u003E",
                "isDefault": true
            }],
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task ListCoverPagesAsync_ValidInput_ReturnsOk()
        {
            // Arrange
            var service = new FaxSendService(_HttpHandlerFake);

            // Act
            var result = await service.ListCoverPagesAsync();
            var actual = result[0];

            // Assert
            result.Count.Should().Be(1);
            actual.HTML.Should().NotBeNull();
            actual.Name.Should().Be("simple template");
            actual.IsDefault.Should().BeTrue();
        }

        /*
         {
            "payload": {
                "prices": [
                    {
                        "countryCode": "US",
                        "countryName": "USA",
                        "prefix": "1",
                        "price": 0.03,
                        "currency": "AUD"
                    },
                    {
                        "countryCode": "AT",
                        "countryName": "Austria",
                        "prefix": "43",
                        "price": 0.15,
                        "currency": "AUD"
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
        public async Task ListPricesAsync_ValidInput_ReturnsOk()
        {
            // Arrange
            var service = new FaxSendService(_HttpHandlerFake);

            // Act
            var result = await service.ListPricesAsync();
            var price1 = result.Prices[0];
            var price2 = result.Prices[1];

            // Assert
            result.Prices.Count.Should().Be(2);
            price1.CountryCode.Should().Be("US");
            price1.CountryName.Should().Be("USA");
            price1.Prefix.Should().Be("1");
            price1.Price.Should().Be(0.03m);
            price1.Currency.Should().Be("AUD");
            price2.CountryCode.Should().Be("AT");
            price2.CountryName.Should().Be("Austria");
            price2.Prefix.Should().Be("43");
            price2.Price.Should().Be(0.15m);
            price2.Currency.Should().Be("AUD");
        }
    }
}
