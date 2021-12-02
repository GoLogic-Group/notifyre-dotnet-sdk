# Notifyre dotnet SDK

## Installation

To install the Nuget package, you can use the dotnet CLI or the NuGet CLI:

```sh
dotnet add package Notifyre.net
```

```sh
nuget install Notifyre.net
```

Alternatively, install the package using the Package Manager in Visual Studio.

## Documentation

For comprehensive documentation, please visit the [Notifyre API documentation][api-docs].

## Usage

Register the package in to your DI:

```C#
services.AddNotifyre(config =>
{
    config.ApiKey = Configuration.GetValue<string>("Notifyre:ApiKey");
});
```

You can resolve an instance of for example the `FaxService` to make requests:

```C#
var request = new ListFaxesRequest()
{
    FromDate = new DateTime(2021, 09, 23),
    ToDate = new DateTime(2021, 09, 24),
    Sort = ListFaxesRequestSortTypes.Desc
};
var response = await _faxService.ListFaxesAsync(request);
```

### Services:

-   FaxService
-   SmsService
-   ContactsService

[api-docs]: https://docs.notifyre.com
