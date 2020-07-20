using CreatingWebAPIService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http.Filters;

namespace CreatingWebAPIService
{
    public class CustomFilterAttribute: ActionFilterAttribute
    {

        
        string parameter = "RatingIP";
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            if (actionContext.ActionArguments == null || !actionContext.ActionArguments.ContainsKey(parameter))
                throw new Exception(string.Format("Parameter '{0}' not present", parameter));

            IPRating model = actionContext.ActionArguments[parameter] as IPRating;

           

            if (String.IsNullOrEmpty(model.UserName))
            {
                model.Status = "Faild";
                model.Status = "Invalid User Name. User Name  Must be  Eamil id";

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, model, actionContext.ControllerContext.Configuration.Formatters.JsonFormatter);


            }
            else
            {
                Regex objNotWholePattern = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                bool sdfdf =objNotWholePattern.IsMatch(model.UserName);
                if(!sdfdf)
                {
                    model.Status = "Faild";
                    model.Status = "Invalid User Name. User Name  Must be  Eamil id";

                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, model, actionContext.ControllerContext.Configuration.Formatters.JsonFormatter);


                }

            }

        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            
        }
        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    var param = context.ActionArguments.SingleOrDefault(p => p.Value is IPRating);
        //    if (param == null)
        //    {
        //        context.Result = new BadRequestObjectResult("Object is null");
        //        return;
        //    }


        //}

        //public void OnActionExecuted(ActionExecutedContext context)
        //{
        //}
    }
}