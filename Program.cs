using Microsoft.EntityFrameworkCore;
using FormBackend.Services.Context;
using FormBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<FormService>();

var connectionString = builder.Configuration.GetConnectionString("FormBase");
builder.Services.AddDbContext<DataContext>(Options => Options.UseSqlServer(connectionString));

builder.Services.AddCors(options => options.AddPolicy("FormPolicy", builder =>{
    builder.WithOrigins("http://localhost:5123", "*")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials();
}));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

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
