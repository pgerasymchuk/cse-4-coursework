using CycleShare.Server;
using CycleShare.Server.Context;
using CycleShare.Server.Repositories.Interfaces;
using CycleShare.Server.Repositories;
using CycleShare.Server.Services.Interfaces;
using CycleShare.Server.Services;
using Microsoft.EntityFrameworkCore;
using CycleShare.Server.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(AutomapperProfile));

builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IBikeRepository, BikeRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IGalleryItemRepository, GalleryItemRepository>();

builder.Services.AddScoped<IBikeService, BikeService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IGalleryItemService, GalleryItemService>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped(provider => provider.GetRequiredService<IOptions<AppSettings>>().Value);

builder.Services.Configure<JWT>(builder.Configuration.GetSection("AppSettings:JWT"));
builder.Services.AddScoped(provider => provider.GetRequiredService<IOptions<JWT>>().Value);

builder.Services.Configure<StorageSettings>(builder.Configuration.GetSection("AppSettings:StorageSettings"));
builder.Services.AddScoped(provider => provider.GetRequiredService<IOptions<StorageSettings>>().Value);

builder.Services.Configure<UserSettings>(builder.Configuration.GetSection("AppSettings:UserSettings"));
builder.Services.AddScoped(provider => provider.GetRequiredService<IOptions<UserSettings>>().Value);

builder.Services.Configure<EmailEntitySettings>(builder.Configuration.GetSection("AppSettings:EmailEntitySettings"));
builder.Services.AddScoped(provider => provider.GetRequiredService<IOptions<EmailEntitySettings>>().Value);

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("AppSettings:EmailSettings"));
builder.Services.AddScoped(provider => provider.GetRequiredService<IOptions<EmailSettings>>().Value);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
