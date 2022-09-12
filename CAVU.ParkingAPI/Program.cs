using AutoMapper;
using Cavu.DataAccess;
using Cavu.DataAccess.Configuration;
using Cavu.Services.Interfaces;
using Cavu.Services.Mapper;
using Cavu.Services.Services;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var mapperConfiguration = new MapperConfiguration(mapperConfig =>
            mapperConfig.AddProfile(new ReservationMapProfile())
            );
var mapper = mapperConfiguration.CreateMapper();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestConnectionString"));
});



builder.Services.AddSingleton(mapper);
// Add services to the container.
builder.Services.AddMemoryCache();
//var service = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<ReservationService>();
builder.Services.AddTransient<IReservationService, ReservationService>();
builder.Services.AddTransient<IParkingSlotService, ParkingSlotService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
//builder.Services.Configure<ConfigSettings>(options => System.Configuration.GetSection("ConfigSettings").Bind(options));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddFluentValidation();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
