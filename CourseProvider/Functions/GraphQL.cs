using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CourseProvider.Functions
{
    public class GraphQL(ILogger<GraphQL> logger, IGraphQLRequestExecutor graphQLRequestExecutor)
    {
        //En logginsats f�r att logga information, varningar och fel
        //En instans som exekverar GraphQL-f�rfr�gningar
        private readonly ILogger<GraphQL> _logger = logger;
        private readonly IGraphQLRequestExecutor _graphQLRequestExecutor = graphQLRequestExecutor;

        //Run-metoden f�r heta GraphQL
        //Metoden skapar en Azure function som hanterar POST-f�rfr�gningar till rutten graphql och anv�nder en 
        //IGraphQLRequestExecutor f�r att exekvera GraphQL-f�rfr�gningarna. 
        //Loggning hanteras genom en ILogger<GraphQL> instans


        [Function("GraphQL")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "graphql")] HttpRequest req)
        {
            try
            {
                if (_graphQLRequestExecutor != null)
                {
                    return await _graphQLRequestExecutor.ExecuteAsync(req);
                }
                else
                {
                    _logger.LogError("GraphQL request executor is null.");
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing the GraphQL request.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
