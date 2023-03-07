# ServicePlace
A market place for services

Wants to demonstrate testing in dotnet.

## instructions

```
# call this command while in the data project directory to let the migrations be created
dotnet ef migrations add InitialCreate --context ServicePlaceContext --startup-project ../ServicePlace.Web/ServicePlace.Web.csproj  

# create database after having the migrations
dotnet ef database update --startup-project ../ServicePlace.Web/ServicePlace.Web.csproj 
```