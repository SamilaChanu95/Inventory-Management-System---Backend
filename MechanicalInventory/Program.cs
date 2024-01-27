using MechanicalInventory.Context;
using MechanicalInventory.Services;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddRateLimiter(opt =>
{
    opt.AddFixedWindowLimiter("Api", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(30);
        opt.PermitLimit = 15;
        // opt.QueueLimit = 2;
        // opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.AutoReplenishment = true;
    });
    opt.RejectionStatusCode = 429; // HTTP 429 Too Many Requests;
});

// var rateLimitPolicy = Policy.RateLimitAsync(2,TimeSpan.FromSeconds(30));

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

// Custom Middleware to use the Polly RateLimitPolicy
/*app.Use(async (context, next) =>
{
    var result = await rateLimitPolicy.ExecuteAndCaptureAsync(() => next());
    if (result.Outcome == OutcomeType.Failure)
    {
        context.Response.StatusCode = 429; // HTTP 429 Too Many Requests
        await context.Response.WriteAsync("Rate limit exceeded.");
        return;
    }
    await next();
});*/

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
