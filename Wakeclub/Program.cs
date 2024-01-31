using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Wakeclub.Data;
using Wakeclub.Entities;
using Wakeclub.MailKit;
using Wakeclub.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
//     .AddIdentityCookies();
// builder.Services.AddAuthorizationBuilder();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(
    options => options
        .UseLazyLoadingProxies()
        .UseSqlServer(connectionString));

// Auth
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<DataContext>();

// Email Services
// builder.Services.AddFluentEmail(builder.Configuration);
// builder.Services.AddTransient<IEmailService, EmailService>();
// builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IEmailSender, MailKitEmailSender>();
builder.Services.Configure<MailKitEmailSenderOptions>(options =>
{
    options.Host_Address = builder.Configuration["ExternalProviders:MailKit:SMTP:Address"];
    options.Host_Port = Convert.ToInt32(builder.Configuration["ExternalProviders:MailKit:SMTP:Port"]);
    options.Host_Username = builder.Configuration["ExternalProviders:MailKit:SMTP:Account"];
    options.Host_Password = builder.Configuration["ExternalProviders:MailKit:SMTP:Password"];
    options.Sender_EMail = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderEmail"];
    options.Sender_Name = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderName"];
});

// Error Handling
builder.Services.AddExceptionHandler<AppExceptionHandler>();
builder.Services.AddExceptionHandler<GeneralExceptionHandler>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

app.MapControllers();

app.MapGroup("/auth").MapIdentityApi<User>();

app.Run();