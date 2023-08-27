using Api;
using AppLogic.Events;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrderManagement.Api.Filters;
using OrderManagement.AppLogic;
using OrderManagement.AppLogic.Events;
using OrderManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<OrderManagementContext>(options =>
{
    string connectionString = configuration["ConnectionString"];
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 15,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddRabbitMQEventBus(configuration);
builder.Services.AddScoped<CocktailAddedEventHandler>();//idk of deze 2 voor of na de repos moette komen
builder.Services.AddScoped<MenuAddedEventHandeler>();
builder.Services.AddScoped<CocktailAddedToMenuEventHandler>();
builder.Services.AddScoped<UserRegisteredEventHandler>();
builder.Services.AddScoped<CocktailDeletedEventHandler>();
builder.Services.AddScoped<MenuDeletedEventHandeler>();
builder.Services.AddScoped<CocktailDeletedFromMenuEventHandeler>();

builder.Services.AddScoped<IOrderRepository, OrderDbRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerDbRepository>();
builder.Services.AddScoped<ICocktailMenuRepository, MenuDbRepository>();
builder.Services.AddScoped<ICocktailRepository, CocktailDbRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemDbRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemDbRepository>();
//builder.Services.AddScoped<HumanResourcesDbInitializer>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICocktailMenuService, CocktailMenuService>();

builder.Services.AddScoped<OrderDbInitializer>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//--------require login?------

//var readPolicy = new AuthorizationPolicyBuilder()
//    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
//    .RequireAuthenticatedUser()
//    .RequireClaim("scope", "devops.read")
//    .Build();
//
//builder.Services.AddSingleton(provider => new ApplicationExceptionFilterAttribute(provider.GetRequiredService<ILogger<ApplicationExceptionFilterAttribute>>()));
//builder.Services.AddControllers(options =>
//{
//    options.Filters.AddService<ApplicationExceptionFilterAttribute>();
//    options.Filters.Add(new AuthorizeFilter(readPolicy));
//});

//


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        string identityUrl = builder.Configuration.GetValue<string>("Urls:IdentityUrl");
        options.Authority = identityUrl;
        options.Audience = "ordermanagement";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false
        };

    });



var app = builder.Build();

IServiceScope startUpScope = app.Services.CreateScope();
var initializer = startUpScope.ServiceProvider.GetRequiredService<OrderDbInitializer>();
initializer.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection(); voor maui over http werken

app.UseAuthorization();

app.MapControllers();

AddEventBusSubscriptions(app);

app.Run();

void AddEventBusSubscriptions(IApplicationBuilder app)
{
    IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

    eventBus.Subscribe<CocktailAddedIntegrationEvent, CocktailAddedEventHandler>();
    eventBus.Subscribe<MenuAddedIntegrationEvent, MenuAddedEventHandeler>();
    eventBus.Subscribe<CocktailAddedToMenuIntegrationEvent, CocktailAddedToMenuEventHandler>();
    eventBus.Subscribe<UserRegisteredIntegrationEvent, UserRegisteredEventHandler>();
    eventBus.Subscribe<CocktailDeletedIntegrationEvent, CocktailDeletedEventHandler>();
    eventBus.Subscribe<MenuDeletedIntegrationEvent, MenuDeletedEventHandeler>();
    eventBus.Subscribe<CocktailDeletedFromMenuIntegrationEvent, CocktailDeletedFromMenuEventHandeler>();

}
