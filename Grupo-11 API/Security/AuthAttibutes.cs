using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Microsoft.AspNetCore.Http;
using Grupo11.Security.Model;

namespace Grupo11.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthControllerAttribute : ActionFilterAttribute
    {
        public Permissions[] PermissionsList { get; set; }

        public AuthControllerAttribute(params Permissions[] permissions)
        {
            PermissionsList = permissions;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? token = context.HttpContext.Session.GetString("sessionKey");

            if (!AuthNetCore.Authenticate(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (PermissionsList != null && PermissionsList.Length > 0)
            {
                if (!AuthNetCore.HavePermission(token, PermissionsList))
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
