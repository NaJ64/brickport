using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrickPort.Web.Utilities
{
    public static class HttpResultExtensions
    {
        public static ActionResult ToOk(this object obj) => new JsonResult(obj) 
        {
            StatusCode = (int)StatusCodes.Status200OK
        };
        
        public static ActionResult ToCreated(this object created) => HttpResultExtensions.ToCreated(created, null, null, null);
        public static ActionResult ToCreated(this object created, string controllerName, string actionName) => HttpResultExtensions.ToCreated(created, controllerName, actionName, null);
        public static ActionResult ToCreated(this object created, string controllerName = null, string actionName = null, object routeValues = null) 
        {
            return (controllerName == null || actionName == null)
                ? new JsonResult(created) { StatusCode = (int)StatusCodes.Status201Created }
                : new CreatedAtActionResult(actionName, controllerName, routeValues, created) as ActionResult;
        }
        
        public static ActionResult ToNotFound(this Exception ex) => new JsonResult(ex.Message) 
        { 
            StatusCode = (int)StatusCodes.Status404NotFound
        };
        
        public static ActionResult ToBadRequest(this Exception ex) => new JsonResult(ex.Message)
        {
            StatusCode = (int)StatusCodes.Status400BadRequest
        };

        public static ActionResult ToInternalServerError(this Exception ex) => new JsonResult(ex.Message) 
        { 
            StatusCode = (int)StatusCodes.Status500InternalServerError 
        };
    }
}