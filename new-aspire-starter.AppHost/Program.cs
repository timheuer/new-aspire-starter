using Aspire.Hosting.Azure;

var builder = DistributedApplication.CreateBuilder(args);

// dependencies
var frontendcache = builder.AddRedisContainer("frontendcache");

var photostorage = builder.AddAzureStorage("psstorage");
var qstorage = builder.AddAzureStorage("qstorage");
var blobgs = photostorage.AddBlobs("photos");
var queues = qstorage.AddQueues("pschange");

var keys = builder.AddAzureKeyVault("keys2");
var db = builder.AddPostgresContainer("db").AddDatabase("psdb");

// microservices
var apiservice = builder.AddProject<Projects.new_aspire_starter_ApiService>("apiservice")
    .WithReference(keys)
    .WithReference(db)
    .WithReference(queues);

builder.AddProject<Projects.new_aspire_starter_Web>("webfrontend")
    .WithReference(apiservice)
    .WithReference(frontendcache)
    .WithReference(blobgs)
    .WithReference(keys);

builder.Build().Run();
