using DLChat.Models;
using DLChat.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DLChat.Hubs;

namespace DLChat;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSignalR(); //SignalR
        services.AddLogging(); //SignalR

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Authority = Configuration["Cognito:Authority"];
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false
            };
        });

        services.AddCors(options =>
        {
            options.AddPolicy("EnableCORS", builder =>  
                builder
                .WithOrigins("http://localhost:4200", "http://localhost:56987")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());
        });

        services.AddControllers();
        services.AddHttpClient();
        services.Configure<DLChatDatabaseSettings>(Configuration.GetSection("DLChatDatabase"));
        services.AddTransient<DLChatDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DLChatDatabaseSettings>>().Value);
        services.AddTransient<UserServices>();
        services.AddTransient<ChatRoomServices>();
        services.AddTransient<ChatMessageServices>();
        services.AddTransient<LoginServices>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }


        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("EnableCORS");
        app.UseWebSockets();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<ChatHub>("/chatHub");
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}