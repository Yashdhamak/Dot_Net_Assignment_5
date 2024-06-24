using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Management_System.ServiceFilters
{
    public class EmployeeFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is EmployeeAdditionalDetails);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Employee details are null.");
                return;
            }

            var employeeDetails = (EmployeeAdditionalDetails)param.Value;
            if (string.IsNullOrWhiteSpace(employeeDetails.EmployeeBasicDetailsUId))
            {
                context.Result = new BadRequestObjectResult("EmployeeBasicDetailsUId is required.");
                return;
            }

            

            var result = await next();
        }
    }
}
