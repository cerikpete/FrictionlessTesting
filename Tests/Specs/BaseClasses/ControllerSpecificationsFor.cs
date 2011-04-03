using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Tests.Specs.BaseClasses
{
    public abstract class ControllerSpecificationsFor<ControllerClass> : SpecificationsFor<ControllerClass> where ControllerClass : class
    {
        protected object GetViewDataFromResult(ActionResult actionResult)
        {
            var viewResult = actionResult as ViewResult;
            if (viewResult == null)
            {
                throw new InvalidOperationException("Result returned from controller is not assignable to ActionResult");
            }
            return viewResult.ViewData["Data"];
        }
    }
}
