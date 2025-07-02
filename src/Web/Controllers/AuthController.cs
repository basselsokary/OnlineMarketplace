using Application.Common.Interfaces.Messaging.Requests.Dispatchers;
using Application.Features.Users.Commands.Login;
using Application.Features.Users.Commands.Logout;
using Application.Features.Users.Commands.Register;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class AuthController(IRequestDispatcher dispatcher) : ApiController(dispatcher)
{
    public IActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await Dispatcher.SendAsync(command);
        if (result.Succeeded)
        {
            TempData["Message"] = "Login successful!";
            return RedirectToAction("Index", "Home");
        }

        AddModelErrors(result);

        return View(command);
    }

    public IActionResult Register() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var result = await Dispatcher.SendAsync(command);
        if (result.Succeeded)
        {
            TempData["Message"] = "Registration successful!";
            return RedirectToAction(nameof(Login));
        }

        AddModelErrors(result);

        return View(command);
    }

    public async Task<IActionResult> Logout()
    {
        await Dispatcher.SendAsync(new LogoutCommand());

        TempData["Message"] = "Logout successful!";
        return RedirectToAction(nameof(Login));
    }
}
