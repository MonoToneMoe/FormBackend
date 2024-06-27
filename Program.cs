using Microsoft.EntityFrameworkCore;
using FormBackend;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

var connectionString = builder.Configuration.GetConnectionString("FormBase");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.UseCors("FormPolicy");

UserService _service = new UserService();

app.MapPost("/AddUser", (UserModel user) =>{
    return _service.AddUser(user);
});

app.MapGet("/GetUsers", () =>{
    return _service.GetUsers();
});


app.Run();
