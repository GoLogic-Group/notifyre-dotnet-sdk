using Notifyre.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NotifyreTests.Services
{
    public class HttpHandlerFake : IHttpHandler
    {

        private int _statusAttempts = 0;

        internal HttpHandlerFake()
        {
        }

        public async Task<HttpResponseMessage> GetAsync(Uri url)
        {
            string msg;
            HttpStatusCode? code = null;
            if (url.AbsoluteUri.Contains("/sms/received?fromdate=&todate=&sort=desc&tonumber=&fromnumber=&recipientid=&includereplycontent=false&limit=100&skip=0"))
            {
                msg = @"<?xml version=""1.0"" ?><GetCapabilities service=""WFS"" version=""1.0.0"" xmlns=""http://www.opengis.net/wfs"" />";
            }
            else if (url.AbsoluteUri.Contains("/sms/received?") && url.AbsoluteUri.Contains("1632096000")) // list sms replies request
            {
                msg = @"
                    {
                        ""payload"": {
                            ""smsReplies"": [
                                {
                                    ""recipientID"": ""09645834-7cdf-46e1-95bf-64e2547a5de5"",
                                    ""friendlyID"": ""MLJOJOF4UHG0"",
                                    ""recipientNumber"": ""+61416906715"",
                                    ""senderNumber"": ""+61477789874"",
                                    ""replyID"": ""067f11c0-38a5-402e-b060-8863ed263555"",
                                    ""receivedDateUtc"": 1654053477,
                                    ""contactDetails"": {
                                        ""firstName"": ""Go"",
                                        ""lastName"": ""logic"",
                                        ""organization"": ""Test GoLogic""
                                    },
                                    ""hasMmsDocuments"": false
                                },
                                {
                                    ""recipientID"": ""09645834-7cdf-46e1-95bf-64e2547a5de5"",
                                    ""friendlyID"": ""MLJOJOF4UHG0"",
                                    ""recipientNumber"": ""+61416906715"",
                                    ""senderNumber"": ""+61477789874"",
                                    ""replyID"": ""067f11c0-38a5-402e-b060-8863ed263555"",
                                    ""receivedDateUtc"": 1654053477,
                                    ""contactDetails"": {
                                        ""firstName"": ""Go"",
                                        ""lastName"": ""logic"",
                                        ""organization"": ""Test GoLogic""
                                    },
                                    ""hasMmsDocuments"": true
                                }
                            ],
                            ""total"": 2
                        },
                        ""success"": true,
                        ""statusCode"": 200,
                        ""message"": ""OK"",
                        ""errors"": []
                    }
                    ";
            }
            else if (url.AbsoluteUri.Contains("/sms/send?statustype=")) // list sms request
            {
                msg = @"
                    {
                        ""payload"": {
                            ""smsMessages"": [
                                {
                                    ""id"": ""2bdfff1a-461d-4b5c-b0bc-69af5535fc41"",
                                    ""friendlyID"": ""41S7FTJDSF75"",
                                    ""accountID"": ""AZ07NWWI"",
                                    ""createdBy"": ""9d19715d-97d3-4152-950d-cd487bfffa8f"",
                                    ""recipient"": {
                                        ""id"": ""120a5a36-937c-47c0-8f2d-74d1ea06c012"",
                                        ""friendlyID"": ""TS7SLP7F1PF5"",
                                        ""toNumber"": ""+61477345123"",
                                        ""fromNumber"": ""Shared Number (+61416906716)"",
                                        ""cost"": 0.08,
                                        ""messageParts"": 1,
                                        ""costPerPart"": 0.08,
                                        ""status"": ""sent"",
                                        ""statusMessage"": ""sent"",
                                        ""deliveryStatus"": ""delivered"",
                                        ""queuedDateUtc"": 1676933000,
                                        ""completedDateUtc"": 1676933412
                                    },
                                    ""status"": ""completed"",
                                    ""totalCost"": 0,
                                    ""metadata"": null,
                                    ""createdDateUtc"": 1676932998,
                                    ""submittedDateUtc"": 1635717738,
                                    ""completedDateUtc"": 1676933415,
                                    ""lastModifiedDateUtc"": 1676933415
                                }
                            ],
                            ""total"": 6
                        },
                        ""success"": true,
                        ""statusCode"": 200,
                        ""message"": ""OK"",
                        ""errors"": []
                    }
                    ";
            }
            else if (url.AbsoluteUri.Contains("/sms/received/a3a1f58f-c54b-4c49-a9ae-0e0f8f11550a")) // get sms reply
            {
                msg = @"
                    {
                        ""payload"": {
                            ""recipientID"": ""baf0be23-f102-48dd-90f5-2183c19cf890"",
                            ""friendlyID"": ""MLJOJOF4UHG0"",
                            ""recipientNumber"": ""+61416906715"",
                            ""senderNumber"": ""+61477789879"",
                            ""replyID"": ""a3a1f58f-c54b-4c49-a9ae-0e0f8f11550a"",
                            ""message"": ""Gologic reply 1"",
                            ""subject"": ""Gologic subject 1"",
                            ""receivedDateUtc"": 1635717854,
                            ""contactDetails"": {
                                        ""firstName"": ""Go"",
                                        ""lastName"": ""logic"",
                                        ""organization"": ""Test GoLogic""
                                    }
                        },
                        ""success"": true,
                        ""statusCode"": 200,
                        ""message"": ""OK"",
                        ""errors"": []
                    }
                    ";
            }
            else if (url.AbsoluteUri.Contains("/sms/received/a3a1f58f-c54b-4c49-a9ae-0e0f8f11550X")) // get sms reply
            {
                msg = @"
                    {
                        ""payload"": {
                            ""recipientID"": ""baf0be23-f102-48dd-90f5-2183c19cf890"",
                            ""friendlyID"": ""MLJOJOF4UHG0"",
                            ""recipientNumber"": ""+61416906715"",
                            ""senderNumber"": ""+61477789879"",
                            ""replyID"": ""a3a1f58f-c54b-4c49-a9ae-0e0f8f11550a"",
                            ""message"": ""Gologic reply 1"",
                            ""receivedDateUtc"": 1635717854,
                            ""contactDetails"": {
                                        ""firstName"": ""Go"",
                                        ""lastName"": ""logic"",
                                        ""organization"": ""Test GoLogic""
                                    }
                        },
                        ""success"": true,
                        ""statusCode"": 200,
                        ""message"": ""OK"",
                        ""errors"": []
                    }
                    ";
            }
            else if (url.AbsoluteUri.Contains("/mms/received/6cc7cc1f-b983-4c1a-9d78-3eccbbee355e/download")) // download mms reply
            {
                msg = @"
                    {
                        ""payload"": {
                            ""documentList"": [
                                {
                                    ""base64Str"": ""aksfioqyfn1pi"",
                                    ""type"": ""image/jpeg"",
                                }
                            ]                           
                        },
                        ""success"": true,
                        ""statusCode"": 200,
                        ""message"": ""OK"",
                        ""errors"": []
                    }
                    ";
            }
            else if (url.AbsoluteUri.Contains("/sms/numbers")) // list sms numbers
            {
                msg = "{\"payload\":{\"smsNumbers\":[{\"id\":\"39be4fb9-a875-468a-904d-c52d4dad21ee\",\"countryCode\":61,\"assignedNumber\":416234582,\"e164\":\"+61416234582\",\"provider\":\"AATP\",\"status\":\"active\",\"subscriptionID\":\"616bdf0a9400548a177ccd47\",\"createdDateUtc\":null,\"lastModifiedDateUtc\":1634459402,\"startDateUtc\":1634392800,\"finishDateUtc\":null,\"campaignID\":\"12345\"}],\"smsSenderIds\":[{\"id\":\"18194c53-a577-4c25-ba48-0cf4fd3054d4\",\"name\":\"SenderId\",\"status\":\"active\",\"createdDateUtc\":1632727486,\"lastModifiedDateUtc\":1632727486}]},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if ( // get sms (recipient) request
              url.AbsoluteUri.Contains("recipients")
              && url.AbsoluteUri.Contains("120a5a36-937c-47c0-8f2d-74d1ea06c012") // recipientId
              && url.AbsoluteUri.Contains("2bdfff1a-461d-4b5c-b0bc-69af5535fc41") // messageId
            )
            {
                msg = @"
                    {
                        ""payload"": {
                            ""id"": ""2bdfff1a-461d-4b5c-b0bc-69af5535fc41"",
                            ""friendlyID"": ""41S7FTJDSF75"",
                            ""accountID"": ""AZ07NWWI"",
                            ""createdBy"": ""9d19715d-97d3-4152-950d-cd487bfffa8f"",
                            ""recipient"": {
                                ""id"": ""120a5a36-937c-47c0-8f2d-74d1ea06c012"",
                                ""friendlyID"": ""TS7SLP7F1PF5"",
                                ""toNumber"": ""+61477345123"",
                                ""fromNumber"": ""Shared Number (+61416906716)"",
                                ""message"": ""test message"",
                                ""cost"": 0.08,
                                ""messageParts"": 1,
                                ""costPerPart"": 0.08,
                                ""status"": ""sent"",
                                ""statusMessage"": ""sent"",
                                ""queuedDateUtc"": 1676933000,
                                ""completedDateUtc"": 1676933412
                            },
                            ""status"": ""completed"",
                            ""totalCost"": 0,
                            ""metadata"": null,
                            ""createdDateUtc"": 1676932998,
                            ""submittedDateUtc"": null,
                            ""completedDateUtc"": 1676933415,
                            ""lastModifiedDateUtc"": 1676933415,
                            ""campaignName"": ""test""
                        },
                        ""success"": true,
                        ""statusCode"": 200,
                        ""message"": ""OK"",
                        ""errors"": []
                    }
                    ";
            }
            else if (url.AbsoluteUri.Contains("/sms/send/2bdfff1a-461d-4b5c-b0bc-69af5535fc41")) // messageId
            {
                msg = @"
                    {
                        ""payload"": {
                            ""id"": ""2bdfff1a-461d-4b5c-b0bc-69af5535fc41"",
                            ""friendlyID"": ""41S7FTJDSF75"",
                            ""accountID"": ""AZ07NWWI"",
                            ""createdBy"": ""9d19715d-97d3-4152-950d-cd487bfffa8f"",
                            ""recipients"": [
                                {
                                    ""id"": ""120a5a36-937c-47c0-8f2d-74d1ea06c012"",
                                    ""friendlyID"": ""TS7SLP7F1PF5"",
                                    ""toNumber"": ""+61477345123"",
                                    ""fromNumber"": ""Shared Number (+61416906716)"",
                                    ""cost"": 0.08,
                                    ""messageParts"": 1,
                                    ""costPerPart"": 0.08,
                                    ""status"": ""sent"",
                                    ""statusMessage"": ""sent"",
                                    ""deliveryStatus"": ""delivered"",
                                    ""queuedDateUtc"": 1676933000,
                                    ""completedDateUtc"": 1676933412
                                }
                            ],
                            ""status"": ""completed"",
                            ""totalCost"": 0,
                            ""metadata"": {},
                            ""createdDateUtc"": 1676932998,
                            ""submittedDateUtc"": null,
                            ""completedDateUtc"": 1676933415,
                            ""lastModifiedDateUtc"": 1676933415,
                            ""campaignName"": ""test""
                        },
                        ""success"": true,
                        ""statusCode"": 200,
                        ""message"": ""OK"",
                        ""errors"": []
                    }
                    ";
            }
            else if (url.AbsoluteUri.Contains("fax/received/15/download")) // download fax received
            {
                msg = "{\"payload\":{\"base64Str\":\"xyz\"},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if (url.AbsoluteUri.Contains("/fax/received")) // list fax received
            {
                msg = "{\"payload\":{\"faxes\":[{\"id\":\"13\",\"from\":\"+61711111111\",\"to\":\"AZ07NWWI\",\"timestamp\":1632694075,\"status\":\"completed\",\"pages\":1,\"duration\":2736},{\"id\":\"15\",\"from\":\"+61711111111\",\"to\":\"AZ07NWWI\",\"timestamp\":1632802359,\"status\":\"completed\",\"pages\":3,\"duration\":3948}],\"total\":10},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if (url.AbsoluteUri.Contains("/fax/numbers"))
            {
                msg = "{\"payload\":{\"numbers\":[{\"id\":28,\"countryCode\":61,\"region\":\"Brisbane\",\"assignedNumber\":722222230,\"e164\":\"+61722222230\",\"status\":\"Active\"},{\"id\":11,\"countryCode\":61,\"region\":\"Sydney\",\"assignedNumber\":211111113,\"e164\":\"+61211111113\",\"status\":\"Active\"}]},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if (url.AbsoluteUri.Contains("/fax/send/recipients/9aca0071-2b61-4beb-bad2-a3ec8ce611e5/download")) // download fax received
            {
                msg = "{\"payload\":{\"base64Str\":\"xyz\"},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if (url.AbsoluteUri.Contains("/fax/send/conversion/")) // get fax conversion status
            {
                if (_statusAttempts == 0)
                {
                    msg = "{\"payload\":null,\"success\":false,\"statusCode\":404,\"message\":\"NotFound\",\"errors\":[]}";
                    code = HttpStatusCode.NotFound;
                }
                else if (_statusAttempts == 1 || _statusAttempts == 2)
                {
                    msg = "{\"payload\":{\"id\":\"6787bea0-d222-4870-811b-1192eef28b34\",\"status\":\"converting\",\"pages\":null,\"fileName\":\"f1a901c1-a4b7-4a43-b00d-15da02fca38d\"},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
                }
                else
                {
                    msg = "{\"payload\":{\"id\":\"6787bea0-d222-4870-811b-1192eef28b34\",\"status\":\"successful\",\"pages\":1,\"fileName\":\"f1a901c1-a4b7-4a43-b00d-15da02fca38d\"},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
                }
                _statusAttempts++;
            }
            else if (url.AbsoluteUri.Contains("/prices"))
            {
                msg = @"
                    {
                        ""payload"": {
                            ""prices"": [
                                {
                                    ""countryCode"": ""US"",
                                    ""countryName"": ""USA"",
                                    ""prefix"": ""1"",
                                    ""price"": 0.03,
                                    ""currency"": ""AUD""
                                },
                                {
                                    ""countryCode"": ""AT"",
                                    ""countryName"": ""Austria"",
                                    ""prefix"": ""43"",
                                    ""price"": 0.15,
                                    ""currency"": ""AUD""
                                }
                            ]
                        },
                        ""success"": true,
                        ""statusCode"": 200,
                        ""message"": ""OK"",
                        ""errors"": []
                    }
                    ";
            }
            else if (url.AbsoluteUri.Contains("/fax/send")) // list fax sent
            {
                msg = @"
                    {
                        ""payload"": {
                            ""faxes"": [
                                {
                                    ""id"": ""7155ef1a-c7ff-42bb-b2c8-71ccbfe31ee3"",
                                    ""friendlyID"": ""TGR0L77T3QDR"",
                                    ""recipientID"": ""9aca0071-2b61-4beb-bad2-a3ec8ce611e5"",
                                    ""fromNumber"": ""Shared Number (+61291989589)"",
                                    ""to"": ""+61711111111"",
                                    ""reference"": ""test fax"",
                                    ""createdDateUtc"": 1683518132,
                                    ""queuedDateUtc"": 1683518809,
                                    ""lastModifiedDateUtc"": 1683518906,
                                    ""highQuality"": false,
                                    ""pages"": 4,
                                    ""status"": ""successful"",
                                    ""failedMessage"": null,
                                    ""campaignName"":""test""
                                },
                                {
                                    ""id"": ""48a626f9-45cb-4dc4-8a50-f8c9c2d0caa6"",
                                    ""friendlyID"": ""NDUMXBO0J2X2"",
                                    ""recipientID"": ""532324e8-9dc6-48c1-8a1f-319e6e814cee"",
                                    ""fromNumber"": ""Shared Number (+61291989589)"",
                                    ""to"": ""+61745612378"",
                                    ""reference"": ""test fax for dev app"",
                                    ""createdDateUtc"": 1678400752,
                                    ""queuedDateUtc"": 1678400756,
                                    ""lastModifiedDateUtc"": 1678400846,
                                    ""highQuality"": false,
                                    ""pages"": 4,
                                    ""status"": ""queued"",
                                    ""failedMessage"": null,
                                    ""campaignName"":""test""
                                }
                            ],
                            ""total"": 2
                        },
                        ""success"": true,
                        ""statusCode"": 200,
                        ""message"": ""OK"",
                        ""errors"": []
                    }
                    ";

            }
            else if (url.AbsoluteUri.Contains("addressbook/contacts/1ff6263a-23b9-4de5-bdf3-d83d99bb43c7")) // get contact
            {
                msg = "{\"payload\":{\"id\":\"1ff6263a-23b9-4de5-bdf3-d83d99bb43c7\",\"firstName\":\"Mike\",\"lastName\":\"Contact\",\"organization\":\"gologic\",\"email\":\"mike.z@g.com\",\"faxNumber\":\"+61711111111\",\"mobileNumber\":\"+61415151234\",\"groups\":[{\"id\":\"8e39ef8e-11fe-4d65-99bb-0c7927bac0d8\",\"name\":\"another\",\"createdDateUtc\": 1630454403},{\"id\":\"7509a3ca-a833-48a2-bd2e-ce0b5f034ded\",\"name\":\"gooooooy\",\"createdDateUtc\": 1630454404}],\"customFields\":[{\"id\":\"063ef468-b530-4aca-85a9-179e491acf31\",\"key\":\"cf1\",\"Value\":\"Joined 2021\"}]},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if (url.AbsoluteUri.Contains("addressbook/groups")) // list groups
            {
                msg = "{\"payload\":{\"groups\":[{\"id\":null,\"name\":\"All Contacts\",\"createdDateUtc\":null,\"totalContacts\":2},{\"id\":\"9735311e-f160-45d0-b05f-7a5626cb1d22\",\"name\":\"another\",\"createdDateUtc\": 1630454407,\"totalContacts\":2}]},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if (url.AbsoluteUri.Contains("/fax/send/recipients/9aca0071-2b61-4beb-bad2-a3ec8ce611e5/download")) // download fax sent
            {
                msg = "{\"payload\":{\"base64Str\":\"xyz\"},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if (url.AbsoluteUri.Contains("/fax/coverpages")) // list coverpages
            {
                msg = "{\"payload\":[{\"name\":\"simple template\",\"html\":\"<p>my cover page<\\/p>\",\"isDefault\":true}],\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else
            {
                throw new NotImplementedException();
            }
            await Task.Delay(0);
            var response = new HttpResponseMessage();
            HttpContent content = new StringContent(msg);
            response.Content = content;
            response.StatusCode = code ?? HttpStatusCode.OK;
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(Uri url, HttpContent content)
        {
            string body = await content.ReadAsStringAsync();
            string msg;
            if (
                body.Contains("breaking changes inc.")
                && url.AbsoluteUri.Contains("addressbook/contacts")
            )
            {
                msg = "{\"payload\":{\"id\":\"2e759a5f-ee8b-443d-ae2e-3e7832e6a1ff\",\"lastName\":\"Baltes\",\"organization\":\"breaking changes inc.\",\"email\":\"pascal.baltes@gologic.com.au\",\"faxNumber\":\"+61711111111\",\"mobileNumber\":\"+61477245453\",\"groups\":[{\"id\":\"c0333f6d-0d14-4050-a84d-15b9dc47df2c\",\"name\":\"Gologic Devs\",\"createdDateUtc\": 1630454401}],\"customFields\":[{\"id\":\"8ff84b68-8d3e-4bff-b28e-81ccbf9e4ca0\",\"key\":\"cf1\",\"customValue\":\"Joined 05.2021\"},{\"id\":\"c14db300-5ef5-412c-871d-ce7e08a77bdc\",\"key\":\"cf2\",\"customValue\":\"Likes Chocolate\"}]},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if (url.AbsoluteUri.Contains("/sms/send")) // submit sms request
            {
                msg = "{\"payload\":{\"smsMessageID\":\"c6e52a7d-9787-44d2-ac52-b03940b1c1fb\",\"invalidToNumbers\":null},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if (url.AbsoluteUri.Contains("addressbook/contacts/search")) // ListContacts
            {
                msg = "{\"payload\":{\"totalCount\":1,\"contacts\":[{\"id\":\"1ff6263a-23b9-4de5-bdf3-d83d99bb43c7\",\"firstName\":\"Mike\",\"lastName\":\"Contact\",\"organization\":\"gologic\",\"email\":\"mike.z@g.com\",\"faxNumber\":\"+61711111111\",\"mobileNumber\":\"+61415151234\",\"groups\":[{\"id\":\"8e39ef8e-11fe-4d65-99bb-0c7927bac0d8\",\"name\":\"another\",\"createdDateUtc\": 1630454405},{\"id\":\"7509a3ca-a833-48a2-bd2e-ce0b5f034ded\",\"name\":\"gooooooy\",\"createdDateUtc\": 1630454406}],\"customFields\":[{\"id\":\"063ef468-b530-4aca-85a9-179e491acf31\",\"key\":\"cf1\",\"Value\":\"Joined 2021\"}]}]},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if (url.AbsoluteUri.Contains("addressbook/contacts")) // create contact
            {
                msg = "{\"payload\":{\"id\":\"2e759a5f-ee8b-443d-ae2e-3e7832e6a1ff\",\"firstName\":\"Pascal\",\"lastName\":\"Baltes\",\"organization\":\"Gologic Group\",\"email\":\"pascal.baltes@gologic.com.au\",\"faxNumber\":\"+61711111111\",\"mobileNumber\":\"+61477245453\",\"groups\":[{\"id\":\"c0333f6d-0d14-4050-a84d-15b9dc47df2c\",\"name\":\"Gologic Devs\",\"createdDateUtc\": 1630454400}],\"customFields\":[{\"id\":\"8ff84b68-8d3e-4bff-b28e-81ccbf9e4ca0\",\"key\":\"cf1\",\"value\":\"Joined 05.2021\"},{\"id\":\"c14db300-5ef5-412c-871d-ce7e08a77bdc\",\"key\":\"cf2\",\"value\":\"Likes Chocolate\"}]},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if ( // AddContactsToGroups
                url.AbsoluteUri.Contains("addressbook/groups/contacts")
                && body.Contains("1ff6263a-23b9-4de5-bdf3-d83d99bb43c7")
                && body.Contains("1d64404c-6b8e-45ce-809b-ee05fa0f377f")
                && body.Contains("9735311e-f160-45d0-b05f-7a5626cb1d22")
            )
            {
                msg = "{\"payload\":{\"added\":true},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if (url.AbsoluteUri.Contains("addressbook/groups")) // create group
            {
                msg = "{\"payload\":{\"id\":\"725ed91e-d7ff-43a3-b8fb-6e0138be1545\",\"name\":\"my group\",\"createdDateUtc\": 1630454402},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if (url.AbsoluteUri.Contains("addressbook/groups/contacts")) // AddContactsToGroups
            {
                msg = "{\"payload\":{\"added\":true},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }

            else if (url.AbsoluteUri.Contains("/fax/send/conversion")) // submit fax
            {
                msg = "{\"payload\":{\"fileName\":\"fb669b26-e0ac-4793-8898-5d4d5dd3f2a1\"},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if (url.AbsoluteUri.Contains("/fax/send")) // submit fax
            {
                msg = "{\"payload\":{\"faxID\":\"66e00a07-4cc9-4380-8943-395162eac8e1\"},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            
            else
            {
                throw new NotImplementedException();
            }
            var response = new HttpResponseMessage();
            HttpContent contentResponse = new StringContent(msg);
            response.Content = contentResponse;
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(Uri url, HttpContent content)
        {
            string body = await content.ReadAsStringAsync();
            string msg;
            if ( // renamed response property
                url.AbsoluteUri.Contains("addressbook/groups")
                && body.Contains(Guid.Empty.ToString())
            )
            {
                msg = "{\"payload\":{\"hasDeleted\":true},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if (url.AbsoluteUri.Contains("addressbook/groups/contacts")) // delete contacts from groups
            {
                msg = "{\"payload\":{\"removed\":true},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if (
                 url.AbsoluteUri.Contains("addressbook/contacts")
                 || url.AbsoluteUri.Contains("addressbook/groups")
            ) // delete contacts or groups
            {
                msg = "{\"payload\":{\"deleted\":true},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else
            {
                throw new NotImplementedException();
            }
            var response = new HttpResponseMessage();
            HttpContent contentResponse = new StringContent(msg);
            response.Content = contentResponse;
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        public async Task<HttpResponseMessage> UpdateAsync(Uri url, HttpContent content)
        {
            string body = await content.ReadAsStringAsync();
            string msg;
            if (url.AbsoluteUri.Contains("addressbook/contacts"))
            {
                msg = "{\"payload\":{\"id\":\"2e759a5f-ee8b-443d-ae2e-3e7832e6a1ff\",\"firstName\":\"Pascal\",\"lastName\":\"Baltes\",\"organization\":\"Gologic Group\",\"email\":\"pascal.baltes@gologic.com.au\",\"faxNumber\":\"+61711111111\",\"mobileNumber\":\"+61477245453\",\"groups\":[{\"id\":\"c0333f6d-0d14-4050-a84d-15b9dc47df2c\",\"name\":\"Gologic Devs\",\"createdDateUtc\": 1630454408}],\"customFields\":[{\"key\":\"cf1\",\"value\":\"Joined 05.2021\"},{\"key\":\"cf2\",\"value\":\"Likes Chocolate\"}]},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else if (url.AbsoluteUri.Contains("addressbook/groups"))
            {
                msg = "{\"payload\":{\"id\":\"725ed91e-d7ff-43a3-b8fb-6e0138be1545\",\"name\":\"my group\",\"createdDateUtc\": 1630454409},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else
            {
                throw new NotImplementedException();
            }
            var response = new HttpResponseMessage();
            HttpContent contentResponse = new StringContent(msg);
            response.Content = contentResponse;
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        public async Task<HttpResponseMessage> PostCompressedAsync(Uri url, HttpContent content)
        {
            string body = await content.ReadAsStringAsync();
            string msg;
            if (url.AbsoluteUri.Contains("/fax/send/conversion")) // submit fax
            {
                msg = "{\"payload\":{\"fileName\":\"fb669b26-e0ac-4793-8898-5d4d5dd3f2a1\"},\"success\":true,\"statusCode\":200,\"message\":\"OK\",\"errors\":[]}";
            }
            else
            {
                throw new NotImplementedException();
            }
            var response = new HttpResponseMessage();
            HttpContent contentResponse = new StringContent(msg);
            response.Content = contentResponse;
            response.StatusCode = HttpStatusCode.OK;
            return response;
            throw new NotImplementedException();
        }
    }
}
