using Microsoft.EntityFrameworkCore;
using FormBackend.Services.Context;
using FormBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<UserService>();
builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

var connectionString = builder.Configuration.GetConnectionString("FormBase");


// builder.Services.AddCors(options => options.AddPolicy("FormPolicy", builder =>{
//     builder.WithOrigins("http://localhost:5123")
//     .AllowAnyOrigin()
//     .AllowAnyMethod();
// }));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseCors("FormPolicy");

app.MapGet("/test", ()=>{
    return "Works";
}).WithName("Testing").WithOpenApi();

app.Run();
