   var builder = WebApplication.CreateBuilder(args);

   builder.Services.AddControllers();
   builder.Services.AddCors(options =>
   {
       options.AddPolicy("AllowReactApp",
           builder => builder
               .WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader());
   });

   var app = builder.Build();

   app.UseCors("AllowReactApp");
   app.UseAuthorization();
   app.MapControllers();

   app.Run();