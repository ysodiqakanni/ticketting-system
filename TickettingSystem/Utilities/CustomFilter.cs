using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TickettingSystem.Utilities
{ 
    public class AuthorizeUserAttribute : AuthorizeAttribute
    { 
        public class ClaimRequirementAttribute : TypeFilterAttribute
        {
            public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
            {
                Arguments = new object[] { new Claim(claimType, claimValue) };
            }
        }

        public class ClaimRequirementFilter : IAuthorizationFilter
        {
            readonly Claim _claim;

            public ClaimRequirementFilter(Claim claim)
            {
                _claim = claim;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
               
                // var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
                var hasClaim = context.HttpContext.Session.GetString("JWToken") != null;
                if (!hasClaim)
                {
                    context.HttpContext.Response.Redirect("/staff/login");
                    //context.Result = new ForbidResult();
                    
                }
            }
        }

        //public class Over18Requirement : AuthorizationHandler<Over18Requirement>, IAuthorizationRequirement
        //{
        //    public override Task HandleAsync(AuthorizationHandlerContext context, Over18Requirement requirement)
        //    {
        //        return base.HandleAsync(context);
        //    }
        //    public override void Handle(AuthorizationHandlerContext context, Over18Requirement requirement)
        //    {
        //        if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
        //        {
        //            context.Fail();
        //            return;
        //        }

        //        var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value);
        //        int age = DateTime.Today.Year - dateOfBirth.Year;
        //        if (dateOfBirth > DateTime.Today.AddYears(-age))
        //        {
        //            age--;
        //        }

        //        if (age >= 18)
        //        {
        //            context.Succeed(requirement);
        //        }
        //        else
        //        {
        //            context.Fail();
        //        }
        //    }
        //}
    


}
}
