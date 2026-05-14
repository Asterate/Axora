using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        [HttpGet("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}