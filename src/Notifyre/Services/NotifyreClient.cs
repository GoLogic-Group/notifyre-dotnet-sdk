using Newtonsoft.Json;
using Notifyre.Interfaces;
using Notifyre.Models;
using Notifyre.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Notifyre
{
    public abstract class NotifyreClient
    {
#if DEBUG
        protected string BasePath => $"https://api.dev.notifyre.com/{Version}";
#else
        protected string BasePath => $"https://api.notifyre.com/{Version}";
#endif
        protected virtual string Version => "20210928";

        protected abstract string Path { get; }

        protected virtual string Address => $"{BasePath}/{Path}";

        protected readonly IHttpHandler _HttpClient;

        protected NotifyreClient(IHttpHandler handler) => _HttpClient = handler;

        protected NotifyreClient(NotifyreConfiguration notifureConfiguration) => _HttpClient = new HttpHandler(notifureConfiguration);

        internal async virtual Task<BaseResponse<T>> ReadJsonResponse<T>(HttpResponseMessage message)
        {
            BaseResponse<T> response;
            try
            {
                var responseMsg = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
                response = JsonConvert.DeserializeObject<BaseResponse<T>>(responseMsg, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                });
            }
            catch (JsonReaderException ex)
            {
                if (!message.IsSuccessStatusCode)
                {
                    throw BuildException(message);
                }
                throw new NotifyreException("Response is not in valid JSON format");
            }
            catch (JsonSerializationException ex)
            {
                if (!message.IsSuccessStatusCode)
                {
                    throw BuildException(message);
                }
                throw new NotifyreException($"The response could not be deserialized: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw BuildException(message);
            }
            if (response?.StatusCode == Convert.ToInt32(HttpStatusCode.OK))
            {
                return response;
            }
            else
            {
                throw BuildException(response);
            }
        }

        private NotifyreException BuildException<T>(BaseResponse<T> response)
        {
            if (response == null)
            {
                var msg = $"The response could not be deserialized. Please check if the correct SDK version is installed. This SDK is targeting version {Version}. Please visit docs.notifyre.com for more information.";
                return new NotifyreException(msg)
                {
                    Success = false,
                    StatusCode = 400,
                    Errors = new List<string>()
                };
            }
            else
            {
                return new NotifyreException(response.Message)
                {
                    Success = response.Success,
                    StatusCode = response.StatusCode,
                    Errors = response.Errors
                };
            }
        }

        private NotifyreException BuildException(HttpResponseMessage message)
        {
            return new NotifyreException(message.ReasonPhrase)
            {
                Success = false,
                StatusCode = (int)message.StatusCode
            };
        }
    }
}
