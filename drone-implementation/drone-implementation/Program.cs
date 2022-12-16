using AutoMapper;
using ddrone_DataAccess;
using drone_DataAccess;
using drone_DataAccess.Repositories;
using drone_DataAccess.UnitOfWork;
using drone_Domain.Interfaces;
using drone_implementation.Implementation.Interfaces;
using drone_implementation.Implementation.Services;
using drone_implementation.Profiles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddCors();

builder.Services.AddDbContext<ApplicationContext>(o => o.UseInMemoryDatabase("DroneDb"));
#region Repositories
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IMedicationRepository, MedicationRepository>();
builder.Services.AddTransient<IDroneRepository, DroneRepository>();
builder.Services.AddTransient<IDroneModelRepository, DroneModelRepository>();
builder.Services.AddTransient<IDroneItemRepository, DroneItemRepository>();
builder.Services.AddTransient<IStateRepository, StateRepository>();
#endregion
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IBaseResponse<>), typeof(BaseResponse<>));
builder.Services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));
builder.Services.AddScoped<IDroneService, DroneService>();
builder.Services.AddScoped<IDroneItemService, DroneItemService>();

builder.Services.AddHostedService<BackgroundReportService>();
builder.Services.AddScoped<IBackgroundReportService, ReportGenerationService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfiles());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
AddSeedData(app);

void AddSeedData(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetService<ApplicationContext>();
    AppSeedClass.SeedData(db);
}

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
