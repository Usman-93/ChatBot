using ChatBot.Services;
using Microsoft.Extensions.AI;
using OpenAI.Embeddings;
using Pinecone;

namespace ChatBot;

public class Startup
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        var openAiKey = Utils.RequireEnv("OPENAI_API_KEY");
        var pineconeKey = Utils.RequireEnv("PINECONE_API_KEY");


        builder.Services.AddSingleton<StringEmbeddingGenerator>(
            s => new EmbeddingClient(model: "text-embedding-3-small", apiKey: openAiKey)
            .AsIEmbeddingGenerator()
        );
                
        builder.Services.AddSingleton<IndexClient>(s => new PineconeClient(pineconeKey).Index("wikipedia-landmarks"));
        
        builder.Services.AddSingleton<WikipediaClient>();
        builder.Services.AddSingleton<IndexBuilder>();
        
    }

}