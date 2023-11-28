var builder = DistributedApplication.CreateBuilder(args);

var apiservice = builder.AddProject<Projects.new_aspire_starter_ApiService>("apiservice");

builder.AddProject<Projects.new_aspire_starter_Web>("webfrontend")
    .WithReference(apiservice);


builder.Build().Run();
