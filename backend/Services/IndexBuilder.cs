using ChatBot.Models;
using Microsoft.Extensions.AI;
using Pinecone;

namespace ChatBot.Services;

public class IndexBuilder(
    StringEmbeddingGenerator stringEmbeddingGenerator,
    IndexClient indexClient,
    WikipediaClient wikipediaClient
)
{
    public async Task BuildDocumentIndex(string[] pageTitles)
    {
        foreach (var landMark in pageTitles)
        {
            Document wikiPage = await wikipediaClient.GetWikipediaPageForTitle(landMark);
            var embedding= await stringEmbeddingGenerator.GenerateAsync(
                [wikiPage.Content]
                ,options: new EmbeddingGenerationOptions()
                {
                    Dimensions = 512
                });

            var vectorArray = embedding[0].Vector.ToArray();

            var pineconeVector = new Vector
            {
                Id = wikiPage.Id,
                Values = vectorArray,
                Metadata = new Metadata
                {
                    { "title", wikiPage.Title }
                }
            };

            await indexClient.UpsertAsync(
                new UpsertRequest
                {
                    Vectors = [pineconeVector]
                }
            );

        }


    }

}
