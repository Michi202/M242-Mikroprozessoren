using M242.Model;
using Microsoft.AspNetCore.Mvc.Filters;

namespace M242.Api.Filters
{
    public class TransactionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Filters.Any(x => x.GetType() == typeof(NoTransactionAttribute)))
            {
                return;
            }
            var unitOfWork = (IM242UnitofWork)context.HttpContext.RequestServices.GetService(typeof(IM242UnitofWork));
            unitOfWork.BeginTransaction();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Filters.Any(x => x.GetType() == typeof(NoTransactionAttribute)))
            {
                return;
            }
            var unitOfWork = (IM242UnitofWork)context.HttpContext.RequestServices.GetService(typeof(IM242UnitofWork));
            if (unitOfWork.TransactionIsRunning)
            {
                if (context.Exception == null)
                {
                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
            }
        }
    }
}
