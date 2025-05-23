IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("contentplatform-db")
    //.WithDataVolume()
    .WithPgAdmin();

var rabbitMq = builder.AddRabbitMQ("contentplatform-mq")
    .WithManagementPlugin();

builder.AddProject<Projects.ContentPlatform_Api>("contentplatform-api")
    .WithReference(postgres)
    .WithReference(rabbitMq)
    .WaitFor(postgres)
    .WaitFor(rabbitMq);

builder.AddProject<Projects.ContentPlatform_Reporting_Api>("contentplatform-reporting-api")
    .WithReference(postgres)
    .WithReference(rabbitMq)
    .WaitFor(postgres)
    .WaitFor(rabbitMq);

builder.AddProject<Projects.ContentPlatform_Presentation>("contentplatform-presentation");

builder.Build().Run();
