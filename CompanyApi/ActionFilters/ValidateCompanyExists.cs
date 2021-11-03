using Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace CompanyApi.ActionFilters
{
    public class ValidateCompanyExists : IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        public ValidateCompanyExists(IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");

            var id = (Guid)context.ActionArguments["id"];
            var company = await _unitOfWork.Company.GetCompanyAsync(id, trackChanges);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("company", company);
                await next();
            }
        }

    }
}
