using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenIdDictExample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Configure Entity Framework Core to use Microsoft SQL Server.
    options.UseSqlServer("Data Source=.;Initial Catalog=OpenIdDict;Persist Security Info=True;User ID=sa;Password=admin");

    // Register the entity sets needed by OpenIddict.
    // Note: use the generic overload if you need to replace the default OpenIddict entities.
    options.UseOpenIddict();
});

builder.Services.AddOpenIddict()
    .AddCore(options =>
{
    // Configure OpenIddict to use the Entity Framework Core stores and models.
    // Note: call ReplaceDefaultEntities() to replace the default entities.
    options.UseEntityFrameworkCore()
           .UseDbContext<ApplicationDbContext>();
})
 .AddServer(options =>
 {
     // Enable the token endpoint.
     options.SetTokenEndpointUris("/connect/token");

     // Enable the client credentials flow.
     options.AllowClientCredentialsFlow();

     // Register the signing and encryption credentials.
     options.AddDevelopmentEncryptionCertificate()
            .AddDevelopmentSigningCertificate();

     // Register the ASP.NET Core host and configure the ASP.NET Core options.
     options.UseAspNetCore()
            .EnableTokenEndpointPassthrough();
 })

        // Register the OpenIddict validation components.
        .AddValidation(options =>
        {
            // Import the configuration from the local OpenIddict server instance.
            options.UseLocalServer();

            // Register the ASP.NET Core host.
            options.UseAspNetCore();
        });

builder.Services.AddHostedService<Worker>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();


app.MapControllers();

app.Run();
