using Zy.App.Common.StoreCore;
using Zy.Ids.App.IdsModelExtensions;
using Zy.User.App.IdentityModelExtensions;
using Zy.Video.App;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

IConfiguration configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityModel(configuration);
builder.Services.AddIdentityServiceModel();
builder.Services.AddScoped(typeof(IEntityStore<>), typeof(EntityStore<>));

builder.Services.AddCors();

var mvc = builder.Services.AddMvc();
builder.Services.AddVideoServiceModule(mvc);

var app = builder.Build();

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
