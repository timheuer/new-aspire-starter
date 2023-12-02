using Aspire.Hosting.Azure;

var builder = DistributedApplication.CreateBuilder(args);

// dependencies
var frontendcache = builder.AddRedisContainer("frontendcache");
var db = builder.AddPostgresContainer("db");
var photostorage = builder.AddAzureStorage("photostorage");
var blobgs = photostorage.AddBlobs("photos");

// microservices
var apiservice = builder.AddProject<Projects.new_aspire_starter_ApiService>("apiservice")
    .WithReference(db);

builder.AddProject<Projects.new_aspire_starter_Web>("webfrontend")
    .WithReference(apiservice)
    .WithReference(frontendcache)
    .WithReference(blobgs);

builder.Build().Run();
