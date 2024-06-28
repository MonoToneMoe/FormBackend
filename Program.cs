using Microsoft.EntityFrameworkCore;
using FormBackend.Services.Context;
using FormBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserService>();

var connectionString = builder.Configuration.GetConnectionString("FormBase");
builder.Services.AddDbContext<DataContext>(Options => Options.UseSqlServer(connectionString));


builder.Services.AddCors(options => options.AddPolicy("FormPolicy", builder =>{
    builder.WithOrigins("http://localhost:5123", "*")
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
}));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("FormPolicy");

app.UseAuthorization();

app.MapControllers();


app.Run();
