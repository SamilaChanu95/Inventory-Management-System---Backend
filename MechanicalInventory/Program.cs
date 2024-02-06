using MechanicalInventory.Context;
using MechanicalInventory.Models.RateLimiter;
using MechanicalInventory.Services;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.Configure<RateLimiterOptions>(builder.Configuration.GetSection("RateLimit"));

var FixedWindowLimiter = builder.Configuration.GetSection("RateLimit:0:FixedWindow").Get<FixedWindowOptions>();
var ConcurrencyLimiter = builder.Configuration.GetSection("RateLimit:1:Concurrency").Get<ConcurrencyOptions>();
string CORSPolicy = builder.Configuration.GetSection("CORS:AllowAllPolicy:PolicyName").Value ?? "no-policy";

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddRateLimiter(_limiter =>
{
    _limiter.AddFixedWindowLimiter(policyName: FixedWindowLimiter.PolicyName, options =>
    {
        options.PermitLimit = FixedWindowLimiter.PermitLimit;
        options.Window = TimeSpan.FromSeconds(FixedWindowLimiter.WindowTime);
        options.QueueLimit = FixedWindowLimiter.QueueLimit;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

builder.Services.AddRateLimiter(_limiter =>
{
    _limiter.AddConcurrencyLimiter(policyName: ConcurrencyLimiter.PolicyName, options =>
    {
        options.PermitLimit = ConcurrencyLimiter.PermitLimit;
        options.QueueLimit = ConcurrencyLimiter.QueueLimit;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

/*builder.Services.AddRateLimiter(opt =>
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
});*/

// var rateLimitPolicy = Policy.RateLimitAsync(2,TimeSpan.FromSeconds(30));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(cors => cors.AddPolicy(name: CORSPolicy,
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }
));

/*builder.Services.AddCors(cors => cors.AddDefaultPolicy(build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));*/

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

app.UseCors(CORSPolicy);

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
