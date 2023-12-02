using Aspire.Hosting.Azure;

var builder = DistributedApplication.CreateBuilder(args);

// dependencies
var frontendcache = builder.AddRedisContainer("frontendcache");

var photostorage = builder.AddAzureStorage("psstorage");
var blobgs = photostorage.AddBlobs("photos");

// microservices
var apiservice = builder.AddProject<Projects.new_aspire_starter_ApiService>("apiservice");

builder.AddProject<Projects.new_aspire_starter_Web>("webfrontend")
    .WithReference(apiservice)
    .WithReference(frontendcache)
    .WithReference(blobgs);

builder.Build().Run();
