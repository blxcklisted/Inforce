using Inforce.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inforce.Controllers;

public class DI_BaseController : Controller
{
    protected ApplicationDbContext Context { get; }
    protected IAuthorizationService AuthorizationService { get; }
    protected UserManager<IdentityUser> UserManager { get; }

    public DI_BaseController(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
    {
        Context = context;
        AuthorizationService = authorizationService;
        UserManager = userManager;
    }
}

