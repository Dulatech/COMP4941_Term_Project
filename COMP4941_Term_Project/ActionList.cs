using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using COMP4941_Term_Project.Filters;

namespace COMP4941_Term_Project
{
    public class ActionList
    {
        // list of concatenated (PascalCase) controller & action names separated by "."
        public static readonly List<string> LIST;

        // keyValue pairs of controller & action name
        // ex) ControllerActions["Employees"] would return a list of actions for the EmployeesController
        public static readonly Dictionary<string, List<string>> ControllerActions;

        static ActionList()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            // IsDefined(typeof(CustomActionFilter),true) checks for controller&action names with the annotation [CustomeActionFilter]
            var controllers = asm.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type) && type.IsDefined(typeof(CustomActionFilter), true));

            List<string> controllerNames = new List<string>();
            List<List<string>> actionsNames = new List<List<string>>();
            Dictionary<string, List<string>> keyValuePairs = new Dictionary<string, List<string>>();

            List<string> controllerActions = new List<string>();
            foreach (var controller in controllers)
            {
                string controllerName = controller.Name;
                controllerName = controllerName.Substring(0, controllerName.Length - 10);
                controllerNames.Add(controllerName);
                List<string> actions = new List<string>();
                foreach (var action in controller.GetMethods()
                .Where(method => method.IsPublic && method.IsDefined(typeof(CustomActionFilter), true)))
                {
                    controllerActions.Add(controllerName + action.Name);
                    actions.Add(action.Name);
                }
                keyValuePairs.Add(controllerName, actions);
                actionsNames.Add(actions);
            }
            LIST = controllerActions;
            ControllerActions = keyValuePairs;
        }
    }
}