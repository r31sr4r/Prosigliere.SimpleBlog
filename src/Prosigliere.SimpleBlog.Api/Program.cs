using Prosigliere.SimpleBlog.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAppConnections(builder.Configuration)
    .AddUseCases()
    .AddAndConfigureControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseDocumentation();
app.MapControllers();
app.MigrateDatabase();

app.Run();
