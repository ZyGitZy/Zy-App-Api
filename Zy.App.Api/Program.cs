using Zy.App.Common.StoreCore;
//using Zy.Ids.App.IdsModelExtensions;
using Zy.User.App.IdentityModelExtensions;
using Zy.Video.App;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

IConfiguration configuration = builder.Configuration;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityModel(configuration);
//builder.Services.AddIdentityServiceModel();
builder.Services.AddScoped(typeof(IEntityStore<>), typeof(EntityStore<>));

builder.Services.AddCors();

var mvc = builder.Services.AddMvc();
builder.Services.AddVideoServiceModule(mvc);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(prex =>
{
    prex.AllowAnyHeader();
    prex.AllowAnyMethod();
    prex.AllowAnyOrigin();
    prex.WithExposedHeaders("*");
});


app.UseAuthorization();

app.MapControllers();

app.Run();
