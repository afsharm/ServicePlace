# ServicePlace
A market place for services

Wants to demonstrate testing in dotnet.

[![build](https://github.com/afsharm/ServicePlace/actions/workflows/build-validation.yml/badge.svg)](https://github.com/afsharm/ServicePlace/actions/workflows/build-validation.yml)

## instructions

```
# call this command while in the data project directory to let the migrations be created
dotnet ef migrations add InitialCreate --context ServicePlaceContext --startup-project ../ServicePlace.Web/ServicePlace.Web.csproj  

# create database after having the migrations
dotnet ef database update --startup-project ../ServicePlace.Web/ServicePlace.Web.csproj 

# if `sqlite` does not work after installing vscode-sqlite extension
sudo apt-get install sqlite3
sudo apt-get install libsqlite3-dev

# using dotnet watch
dotnet watch --project src/ServicePlace.Web/ServicePlace.Web.csproj

# run on specific port
dotnet run --urls=http://localhost:5101

# run test
dotnet test src/ServicePlace.UnitTest/ServicePlace.UnitTest.csproj 

# install code coverage & report tools
dotnet tool install --global dotnet-coverage
dotnet tool install --global dotnet-reportgenerator-globaltool

# (in the test artifacts directory)
mkdir artifacts
dotnet coverage collect dotnet test ../src/ServicePlace.UnitTest/ --output-format cobertura
reportgenerator -reports:output.cobertura.xml -targetdir:.
```

### httprepl
```
# install httprepl
dotnet tool install -g Microsoft.dotnet-httprepl
httprepl http://localhost:5101

# define json editor inside httprepl
pref set editor.command.default /usr/bin/gedit

# multiple put
put 123/abc --no-body
```

## study

EF Core Testing
https://learn.microsoft.com/en-us/ef/core/testing/


.Net code covergae 
https://dotnetthoughts.net/generating-code-coverage-reports-in-dotnet-core/


Best practices for exceptions
https://learn.microsoft.com/en-us/dotnet/standard/exceptions/best-practices-for-exceptions
