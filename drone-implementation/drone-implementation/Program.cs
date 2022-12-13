using drone_DataAccess.Repositories;
using drone_DataAccess.UnitOfWork;
using drone_Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Repositories
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IMedicationRepository, MedicationRepository>();
builder.Services.AddTransient<IDroneRepository, DroneRepository>();
builder.Services.AddTransient<IDroneModelRepository, DroneModelRepository>();
builder.Services.AddTransient<IStateRepository, StateRepository>();
#endregion
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
