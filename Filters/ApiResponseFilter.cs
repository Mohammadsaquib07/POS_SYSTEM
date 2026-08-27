using Erp.Model.Entities.ResponseAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Erp.Filters.ResponseAPI
{
    public class ApiResponseFilter :IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext ResultExecutingContexts)
        {
            if(ResultExecutingContexts.Result is ObjectResult objectResults)
            {
                var statusCodes = objectResults.StatusCode ?? StatusCodes.Status200OK;
                var wrappedResponse = new ApiResponse<object>
                {
                    Success = statusCodes is >= 200 and < 300,
                    Message = statusCodes is >= 200 and < 300 ? "Resquest successful" : "Request Failed",
                    Data = objectResults.Value,
                    StatusCode = statusCodes
                };

                ResultExecutingContexts.Result = new ObjectResult(wrappedResponse)
                {
                    StatusCode = statusCodes
                };

            }
        }

        public void OnResultExecuted(ResultExecutedContext context) { }
    }
}