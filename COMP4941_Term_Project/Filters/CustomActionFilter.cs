using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COMP4941_Term_Project.Filters
{
    public class CustomActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ActionDescriptor desc = filterContext.ActionDescriptor;
            string action = desc.ControllerDescriptor.ControllerName + desc.ActionName;
            System.Diagnostics.Debug.WriteLine(action);

            // if not logged in, redirect to Login view
            object branchID = HttpContext.Current.Session["branch"];
            if (branchID == null)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
                return;
            }

            // if admin, authorized
            if (branchID.ToString() == "admin") return;

            // check if this is an authorized action
            // if not, redirect to Home Page (should display a "not authorized" message page in the future)
            string[] actions = (string[])HttpContext.Current.Session["authorized"];
            if (!actions.Contains(action))
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }
        }
    }
}