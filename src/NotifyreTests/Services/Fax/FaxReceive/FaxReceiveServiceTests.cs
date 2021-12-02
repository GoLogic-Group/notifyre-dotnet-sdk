using Xunit;
using FluentAssertions;
using Notifyre;
using Notifyre.Interfaces;
using System.Threading.Tasks;
using Notifyre.Services.Fax.FaxReceive;
using Snapshooter.Xunit;
using System.Collections.Generic;

namespace NotifyreTests.Services.Fax.FaxReceive
{
    public class FaxReceiveServiceTests
    {
        private readonly IHttpHandler _HttpHandlerFake;

        public FaxReceiveServiceTests()
        {
            _HttpHandlerFake = new HttpHandlerFake();
        }

        /* ListReceivedFaxesAsync example response for the unit test
        {
            "payload": [{
                "id": "13",
                "from": "\u002B61711111111",
                "to": "AZ07NWWI",
                "timestamp": 1632694075,
                "status": "completed",
                "pages": 1,
                "duration": 2736
            }, {
                "id": "15",
                "from": "\u002B61711111111",
                "to": "AZ07NWWI",
                "timestamp": 1632802359,
                "status": "completed",
                "pages": 3,
                "duration": 3948
            }],
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task ListReceivedFaxesAsync_ValidInput_ReturnsOk()
        {
            // Arrange
            var service = new FaxReceiveService(_HttpHandlerFake);
            var request = new ListReceivedFaxesRequest() { Limit = 10 };

            // Act 
            var result = await service.ListReceivedFaxesAsync(request);
            var firstReceivedFax = result[0];
            var secondtReceivedFax = result[1];

            // Assert
            firstReceivedFax.ID.Should().Be("13"); // see above example response
            firstReceivedFax.From.Should().Be("+61711111111");
            firstReceivedFax.To.Should().Be("AZ07NWWI");
            firstReceivedFax.Timestamp.Should().Be(1632694075);
            firstReceivedFax.Status.Should().Be("completed");
            firstReceivedFax.Pages.Should().Be(1);
            firstReceivedFax.Duration.Should().Be(2736);

            secondtReceivedFax.ID.Should().Be("15");
            secondtReceivedFax.From.Should().Be("+61711111111");
            secondtReceivedFax.To.Should().Be("AZ07NWWI");
            secondtReceivedFax.Timestamp.Should().Be(1632802359);
            secondtReceivedFax.Status.Should().Be("completed");
            secondtReceivedFax.Pages.Should().Be(3);
            secondtReceivedFax.Duration.Should().Be(3948);
        }

        /* DownloadReceivedFaxAsync example response for the unit test
        {
            "payload": {
                "tiffBase64": "xyz"
            },
            "success": true,
            "statusCode": 200,
            "message": "OK",
            "errors": []
        }
        */
        [Fact]
        public async Task DownloadReceivedFaxAsync_ValidInput_ReturnsOk()
        {
            // Arrange
            var service = new FaxReceiveService(_HttpHandlerFake);
            var request = new DownloadReceivedFaxRequest()
            {
                FaxID = 15 // will be checked in http fake handler
            };

            // Act 
            var result = await service.DownloadReceivedFaxAsync(request);

            // Assert
            result.TiffBase64.Should().Be("xyz"); // see above example response
        }

        /*{
            "Numbers": [
                {
                    "ID": 28,
                    "CountryCode": 61,
                    "Region": "Brisbane",
                    "AssignedNumber": 722222230,
                    "E164": "+61722222230",
                    "Status": "Active"
                },
                {
                    "ID": 11,
                    "CountryCode": 61,
                    "Region": "Sydney",
                    "AssignedNumber": 211111113,
                    "E164": "+61211111113",
                    "Status": "Active"
                }
            ]
        }*/
        [Fact]
        public async Task ListFaxNumbers_ValidInput_ReturnsOk()
        {
            // Arrange
            var expected = new ListFaxNumbersResponse
            {
                Numbers = new List<ListFaxNumbersResponse.FaxNumber>
                {
                    new()
                    {
                        ID = 28,
                        CountryCode = 61,
                        Region = "Brisbane",
                        AssignedNumber = 722222230,
                        E164 = "+61722222230",
                        Status = "Active"
                    },
                    new()
                    {
                        ID = 11,
                        CountryCode = 61,
                        Region = "Sydney",
                        AssignedNumber = 211111113,
                        E164 = "+61211111113",
                        Status = "Active"
                    }
                }
            };

            var service = new FaxReceiveService(_HttpHandlerFake);

            // Act 
            var result = await service.ListFaxNumbersAsync();

            // Assert
            result.Should().BeEquivalentTo(expected);
            result.MatchSnapshot(); // see above example response or FaxReceive > _snapshots_ folder
        }

    }
}
