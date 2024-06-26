using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CourseProvider.Functions;

public class GraphQL(ILogger<GraphQL> logger, IGraphQLRequestExecutor graphQLRequestExecutor)
{
    //En logginsats f�r att logga information, varningar och fel
    //En instans som exekverar GraphQL-f�rfr�gningar
    private readonly ILogger<GraphQL> _logger = logger;
    private readonly IGraphQLRequestExecutor _graphQLRequestExecutor = graphQLRequestExecutor;


    //Metoden skapar en Azure function som hanterar POST-f�rfr�gningar till rutten graphql och anv�nder en 
    //IGraphQLRequestExecutor f�r att exekvera GraphQL-f�rfr�gningarna. 
    //Loggning hanteras genom en ILogger<GraphQL> instans

    [Function("GraphQL")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "graphql")] HttpRequest req)
    {
        _logger.LogInformation("Received a GraphQL request.");

        try
        {
            // Utf�r en nullkontroll f�r att s�kerst�lla att _graphQLRequestExecutor �r inte null
            if (_graphQLRequestExecutor == null)
            {
                _logger.LogError("IGraphQLRequestExecutor is null. Unable to execute GraphQL request.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            // Utf�r GraphQL-f�rfr�gningen om _graphQLRequestExecutor �r inte null
            return await _graphQLRequestExecutor.ExecuteAsync(req);


        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : GraphQl.Run() :: {ex.Message}");
        }

        return new BadRequestResult();
    }
}