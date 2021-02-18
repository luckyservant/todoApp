using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoApp_UmitSadeguzel.Service;
using ToDoApp_UmitSadeguzel.ViewModels;

namespace ToDoApp_UmitSadeguzel.Filters
{
    public class CheckDuplicateItems:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var postedItemId = 0;
            filterContext.ActionParameters.TryGetValue("itemDesc", out object value);

            // If ItemId is null then it is a Create Action
            if(value == null)
            {
                postedItemId = int.Parse(filterContext.HttpContext.Request.Params["ItemId"]);
            }

            var postedItemDesc = filterContext.HttpContext.Request.Params["ItemDesc"].ToString();
            ToDoAppService s = new ToDoAppService();
            var result = s.CheckIfDuplicateItemDesc(postedItemId, postedItemDesc);
            if (result)
            {
                string message = "Already inside!! Don't get confused :/";
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary(message),
                    TempData = filterContext.Controller.TempData
                };
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}