Have to add
{
"Logging": {
"LogLevel": {
"Default": "Information",
"Microsoft.AspNetCore": "Warning"
}
},
"AllowedHosts": "*",
"ConnectionStrings": {
"local": CONNECTION_STRING
}
}
to appsettings.json in DeviceStore.API.

I chose to seperated the solution into multiple for cleaner architecture, readability and to stick to solid principles 🗿.
