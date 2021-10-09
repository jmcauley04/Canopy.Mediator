# **Using Canopy.Mediator API DLL**



### Resources
- [Canopy Api Documentation](http://help.canopyapp.net/api/index.html) _the US hostname is usapi.canopyapp.net_


## Implemented Provider services

INTERFACE                 | IMPLEMENTATION            | Description
------------------------- | ------------------------- | ----------------------
ICanopySearchService<T>   | CanopySearchService<T>    | Get a list of business objects of type T.  Supports sorting, filtering, and paging.
ICanopyRetrieveService<T> | CanopyRetrieveService<T>  | Get a specific business object of type T.  Supports selecting by UUID, Code, and Gtin.

_Note: each service must be restricted to a specific business object using the "...Service<T>" notation._

Supported business objects defined by class models:
- Media
- Product


---
# Retrieving a resource service

_Note: the dependency injection approach is recommended due to the configuration required.  Alternatively, code a static class that handles the 
configuration and is used throughout the application._

### Dependency Injection (DI) Approach

The DI framework should provide a mechanism for registering services.  This DLL provides a succinct way to register all implemented Canopy services in an ASP.NET Core application by following these steps.

1. Find the ConfigureServices method in the startup.cs file.
2. Add the following code:

``` csharp
services.AddCanopyServices(
    new Uri("<Canopy base URI>"),
    new Account()
    {
        UUID = "<Account UUID>",
        HMACSHA256_APIKey = "<HMACSHA256_APIKey>"
    }, 
    <optinal-webproxy>);
```

3. Inject an implemented Canopy service's interface using one of the following approaches; note that the ICanopySearchService<Media> is used as an example:

In a _.razor file_, inject the service directly using the following code; note that the using statement can optionally be included in central "_Imports" file:
``` csharp
@using Canopy.Provider.Interfaces
//@inject INTERFACE PropertyName 
@inject ICanopySearchService<Media> ProviderMediaService 
```

In a _.razor.cs file_, inject as a property using the following notation
``` csharp
[Inject]
//private INTERFACE PropertyName { get; set; }
private ICanopySearchService<Media> ProviderMediaService { get; set; }
```

in a plain old csharp object (POCO), inject the property in the constructor.  Note that the POCO should, itself, be injected or should be retrieved from the DI framework:
``` csharp
public class POCO{
    //private readonly INTERFACE FieldName;
    private readonly ICanopySearchService<Media> ProviderMediaService;

    //public POCO(INTERFACE parameterName)
    public POCO(ICanopySearchService<Media> ProviderMediaService)
    {
        this.ProviderMediaService = ProviderMediaService;
    }
}
```

### Non-DI Approach

The Canopy.Provider.CanopyServiceProvider class has a static method for retrieving the configured http client in the following way.
``` csharp
var uri = new Uri("<Canopy base URI>");
var acct = new Account()
    {
        UUID = "<Account UUID>",
        HMACSHA256_APIKey = "<HMACSHA256_APIKey>"
    };

//var service = Canopy.Provider.CanopyServiceProvider.GetService<INTERFACE, IMPLEMENTATION>(uri, acct, <optinal-webproxy>);
var service = Canopy.Provider.CanopyServiceProvider.GetSearchService<ICanopySearchService<Media>, CanopySearchService<Media>>(uri, acct);
```

---
# Consuming a resource service

The services in this library abide by a fluent programming style which basically means that the developer attempted to create an experience that flows logically and intuitively
by stacking small, specialized interfaces that expose only those methods that are appropriate.  
See the following documentation for further reading on this topic:  
[Fluent Code in C#](https://www.red-gate.com/simple-talk/development/dotnet-development/fluent-code-in-c/)

Tips:
1. A service is lazy and will only perform an action when the ".Get()" method is executed.
2. The services return Task<T> rather than T.  It is recommended that these services be consumed using the "await" keyword.

Example of getting the first "qty" Media business objects
``` csharp
async Task GetMedia(int qty)
{
    try
    {
        List<Media> media = await SearchMediaService
            .WithBatchsize(qty)
            .GetBatchNumber(0)
            .Get();
    }
    catch (Exception ex)
    {
        Logger.LogError(ex.Message);
        throw;
    }
}
```

---
# CanopySearchService<T>

Supports T values:
- Media
- Product

### _WithBatchsize(int qty)_
Defaults to 25  

Limits the number of returned business objects to be at most "qty".

### _GetBatchNumber(int n)_
_where n >= 0_  
Defaults to 0  

Used for paging, this is required when WithBatchSize is used; returns page _n_.  Alternatively, this can be thought of as returning items in the range

$
[(n * qty) + 1, (n * qty) + qty]
$

### _Where<TKey>(Expression<Func<T, TKey>> keySelector, Comparator comparator, string value)_

While this looks complicated, the usage is simple.  The Expression is a means to prompt the IDE to allow the developer to select a strongly typed property.

1. Select the property (strongly typed generated from the business object).
2. Select the comparator (strongly typed generated from the enum Comparator).
3. Define the filter criteria.

Note that Like also serves as Equals when the wildcard symbol (%) is omitted.

**Comparators**         | **Comments**
---:                    | ---
Like                    | serves as Equals when the wildcard symbol (%) is omitted
GreaterThan             | Only works on numerical properties
GreaterThanOrEqualTo    | Only works on numerical properties
LessThan                | Only works on numerical properties
LessThanOrEqualTo       | Only works on numerical properties

``` csharp
List<Product> product = await SearchProductService
                    .Where(x => x.Description, Comparator.Like, "%Corned Beef%")
                    .Get();
```


### _OrderBy<TKey>(Expression<Func<T, TKey>> keySelector)_ and _OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector)_

Similar to the Where method, while this looks complicated, the usage is simple.  The Expression is a means to prompt the IDE to allow the developer to select a strongly typed property.  

If we wanted to reverse order the Where method's request by the Product object's Code property, we would add a single line:

``` csharp
product = await SearchProductService
                    .WithBatchsize(6)
                    .GetBatchNumber(0)
                    .Where(x => x.Description, Comparator.Like, "%Corned Beef%")
                    .OrderByDescending(x => x.Code)
                    .Get();
```
