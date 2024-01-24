using MechanicalInventory.Context;
using MechanicalInventory.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(cors => cors.AddDefaultPolicy(build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddSingleton<DataContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBajajService, BajajService>();
builder.Services.AddScoped<ITvsService, TvsService>();
builder.Services.AddScoped<IHondaService, HondaService>();
builder.Services.AddScoped<IHeroService, HeroService>();
builder.Services.AddScoped<IYamahaService, YamahaService>();
builder.Services.AddScoped<IKtmService, KtmService>();
builder.Services.AddScoped<IMahindraService, MahindraService>();
builder.Services.AddScoped<IKawasakiService, KawasakiService>();
builder.Services.AddScoped<ISuzukiService, SuzukiService>();
builder.Services.AddScoped<IDemarkService, DemarkService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
