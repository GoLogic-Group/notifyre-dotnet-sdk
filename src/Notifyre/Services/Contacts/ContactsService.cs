using System.Threading.Tasks;
using Notifyre.Infrastructure.Utils;
using Notifyre.Interfaces;

namespace Notifyre
{
    public class ContactsService : NotifyreClient
    {
        protected override string Path => "addressbook/contacts";
        protected string GroupsEndpoint => "addressbook/groups";
        protected string GroupsContactsEndpoint => "addressbook/groups/contacts";
        protected string SearchContactsEndpoint => "search";

        public ContactsService(NotifyreConfiguration notifyreConfiguration) : base(notifyreConfiguration)
        {
        }

        public ContactsService(IHttpHandler handler) : base(handler)
        {
        }

        public async Task<CreateContactResponse> CreateContactAsync(
            CreateContactRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(Address);
            var body = JsonUtil.CreateBody(request);
            var result = await _HttpClient.PostAsync(uri, body).ConfigureAwait(false);
            return (await ReadJsonResponse<CreateContactResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<CreateGroupResponse> CreateGroupAsync(
            CreateGroupRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(BasePath, GroupsEndpoint);
            var body = JsonUtil.CreateBody(request);
            var result = await _HttpClient.PostAsync(uri, body).ConfigureAwait(false);
            return (await ReadJsonResponse<CreateGroupResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<AddContactsToGroupsResponse> AddContactsToGroups(
            AddContactsToGroupsRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(BasePath, GroupsContactsEndpoint);
            var body = JsonUtil.CreateBody(request);
            var result = await _HttpClient.PostAsync(uri, body).ConfigureAwait(false);
            return (await ReadJsonResponse<AddContactsToGroupsResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<GetContactResponse> GetContactAsync(
            GetContactRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(Address, request.ContactID.ToString());
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<GetContactResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<ListContactsResponse> ListContactsAsync(
            ListContactsRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(Address, SearchContactsEndpoint);
            var body = JsonUtil.CreateBody(request);
            var result = await _HttpClient.PostAsync(uri, body).ConfigureAwait(false);
            return (await ReadJsonResponse<ListContactsResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<ListGroupsResponse> ListGroupsAsync(
            ListGroupsRequest request
        )
        {
            var uri = UrlUtil.CreateUrlWithQuery(request, BasePath, GroupsEndpoint);
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<ListGroupsResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<RemoveContactsFromGroupResponse> RemoveContactsFromGroupAsync(
            RemoveContactsFromGroupRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(BasePath, GroupsContactsEndpoint);
            var body = JsonUtil.CreateBody(request);
            var result = await _HttpClient.DeleteAsync(uri, body).ConfigureAwait(false);
            return (await ReadJsonResponse<RemoveContactsFromGroupResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<UpdateContactResponse> UpdateContactAsync(
            UpdateContactRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(Address, request.ID.ToString());
            var body = JsonUtil.CreateBody(request);
            var result = await _HttpClient.UpdateAsync(uri, body).ConfigureAwait(false);
            return (await ReadJsonResponse<UpdateContactResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<UpdateGroupResponse> UpdateGroupAsync(
            UpdateGroupRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(BasePath, GroupsEndpoint, request.ID.ToString());
            var body = JsonUtil.CreateBody(request);
            var result = await _HttpClient.UpdateAsync(uri, body).ConfigureAwait(false);
            return (await ReadJsonResponse<UpdateGroupResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<DeleteContactsResponse> DeleteContactsAsync(
            DeleteContactsRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(Address);
            var body = JsonUtil.CreateBody(request);
            var result = await _HttpClient.DeleteAsync(uri, body).ConfigureAwait(false);
            return (await ReadJsonResponse<DeleteContactsResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<DeleteGroupsResponse> DeleteGroupsAsync(
            DeleteGroupsRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(BasePath, GroupsEndpoint);
            var body = JsonUtil.CreateBody(request);
            var result = await _HttpClient.DeleteAsync(uri, body).ConfigureAwait(false);
            return (await ReadJsonResponse<DeleteGroupsResponse>(result).ConfigureAwait(false)).Payload;
        }
    }
}
