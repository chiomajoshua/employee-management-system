Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(new WebApplicationOptions
    {
        ApplicationName = typeof(Program).Assembly.FullName,
        ContentRootPath = Directory.GetCurrentDirectory()
    });

    builder.Host.UseSerilog((ctx, lc) => lc
       .WriteTo.Console()
       .ReadFrom.Configuration(ctx.Configuration));

    builder.Services.RegisterDatabaseService(builder.Configuration);
    builder.Services.RegisterGenericRepository();

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddResponseCaching();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("pol",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
    });
    builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

    builder.Services.AddControllers()
         .AddFluentValidation(s =>
         {
             s.RegisterValidatorsFromAssemblyContaining<Program>();
             s.DisableDataAnnotationsValidation = true;
         });

    builder.Services.AddTransient<ExceptionMiddleware>();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "EMS API", Version = "v1" });
        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    });

    builder.Services.AddIdentityService(builder.Configuration);


    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterModule(new AutofacContainerModule());
        });




    var app = builder.Build();
    app.UseSerilogRequestLogging();
    app.UseGlobalExceptionErrorHandler();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("./v1/swagger.json", "EMS API v1"));

    app.UseRouting();
    app.UseCors("pol");
    app.UseHttpsRedirection();
    app.UseResponseCaching();

    app.UseAuthentication();
    app.UseAuthorization();
    

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
