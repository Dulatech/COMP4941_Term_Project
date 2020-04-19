using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Diagnostics;

namespace COMP4941_Term_Project.Filters
{
    public class ErrorLogFilter : ActionFilterAttribute, IExceptionFilter
    {
        //private static EventLog eventLog;
        static ErrorLogFilter() {
            Debug.WriteLine("ErrorLogFilter static constructor");
            //if (!EventLog.SourceExists("MVCLogSource"))
            //    EventLog.CreateEventSource("MVCLogSource", "MVCLog");
            //eventLog.Source = "MVCLogSource";
            //eventLog.Log = "MVCLog";
        }
        public void OnException(ExceptionContext filterContext)
        {
            var routeData = filterContext.RouteData;
            string controllerName = (string) routeData.Values["controller"];
            string actionName = (string) routeData.Values["action"];
            string exception = filterContext.Exception.Message;
            string msg = String.Format("Controller: {0}\nAction: {1}\n{2}", controllerName, actionName, exception);

            Debug.WriteLine(msg);
            //eventLog.WriteEntry(msg);
        }
    }
}