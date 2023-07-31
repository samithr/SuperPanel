using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SuperPanel.Service.Interfaces;
using System.Threading.Tasks;

namespace SuperPanel.App.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<IActionResult> Index(int? page)
        {
            page = page == null ? 1 : page;
            var users = await _userService.GetAll(page.Value);
            return View(users);
        }

        public async Task<IActionResult> Delete(int page, int userId)
        {
            var response = await _userService.DeleteUser(userId);
            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                TempData["deleteError"] = response.ErrorMessage;
            }
            return RedirectToAction("Index", new { page });
        }
    }
}