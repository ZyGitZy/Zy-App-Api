using Zy.App.Common.StoreCore;
using Zy.User.App;
using Zy.Video.App;
using Zy.Ids.App;
using Zy.App.Common.AppExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

IConfiguration configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddLibScopModels();

var mvc = builder.Services.AddMvc();

mvc.AddIdsModel(configuration);
mvc.AddUserModel();
mvc.AddVideoServiceModule();

var app = builder.Build();

app.UseIdentityServer();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

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
