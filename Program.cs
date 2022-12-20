var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World, I'm old, use /v1 instead!");
app.MapGet("/v1", () => "Hello World! v1");

app.MapGet("/v1/env", () => Environment.GetEnvironmentVariables());

// An API that write query string params inside a file on disk
// and returns the file content
app.MapGet("/v1/file/append", (string line) =>
{
	// Path of the file is an environment variable called DATA_PATH
	// if not set, it will use the current directory
	var path = Environment.GetEnvironmentVariable("DATA_PATH") ?? Directory.GetCurrentDirectory();
	var filename = "data.txt";
	var filepath = Path.Combine(path, filename);
	// Append the line to the file
	File.AppendAllText(filepath, line + Environment.NewLine);
	return File.ReadAllText(filepath);
});

app.Run();
