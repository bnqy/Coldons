using Microsoft.AspNetCore.Mvc.Formatters;
using Coldons.Lib; // AddNorthwindContext extension method
using Northwind.WebApi.Repositories;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using static System.Console;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddNorthwindContext();
builder.Services.AddControllers(options =>
{
	WriteLine("Default output formatters");
	
	foreach (IOutputFormatter formatter in options.OutputFormatters)
	{
		OutputFormatter? mediaFormatter = formatter as OutputFormatter;
		if (mediaFormatter == null)
		{
			WriteLine($" {formatter.GetType().Name}");
		}
		else
		{
			WriteLine(" {0}, Media types: {1}", 
				arg0: mediaFormatter.GetType().Name, 
				arg1: string.Join(", ", mediaFormatter.SupportedMediaTypes));
		}
	}
}).AddXmlDataContractSerializerFormatters().AddXmlSerializerFormatters();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new()
	{
		Title = "Northwind Service API",
		Version = "v1"
	});
});


builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind Service API Version 1");
		c.SupportedSubmitMethods(new[] {
			SubmitMethod.Get, 
			SubmitMethod.Post,
			SubmitMethod.Put, 
			SubmitMethod.Delete });
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
