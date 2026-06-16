// See https://aka.ms/new-console-template for more information

using Chatbot;
using ChatBot;
using ChatBot.Services;

Console.WriteLine("Hello, World!");

// SourceData.LandMarks.ToList().ForEach(Console.WriteLine);


var title = SourceData.LandMarks[2];
// var wikipediaClient = new WikipediaClient();

var builder = WebApplication.CreateBuilder(args);
Startup.ConfigureServices(builder);


var app = builder.Build();

var indexBuilder = app.Services.GetRequiredService<IndexBuilder>();
var landMarks= SourceData.LandMarks;

await indexBuilder.BuildDocumentIndex(landMarks);






