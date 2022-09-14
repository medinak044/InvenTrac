using AutoMapper;
using InvenTrac.Helpers;
using InvenTrac.Models;
using InvenTrac.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvenTrac.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMapper _mapper;

    public AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IMapper mapper
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        var model = new LoginVM();
        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).ControllerName());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        if (!ModelState.IsValid)
            return View(loginVM);

        // Check if user exists
        var existingUser = await _userManager.FindByEmailAsync(loginVM.Email);
        if (existingUser == null)
        {
            TempData["error"] = "Email doesn't exist";
            return View(loginVM);
        }

        // Verify password
        var passwordIsCorrect = await _userManager.CheckPasswordAsync(existingUser, loginVM.Password);
        if (!passwordIsCorrect)
        {
            TempData["error"] = "Invalid credentials";
            return View(loginVM);
        }

        // Log user in
        var signInResult = await _signInManager.PasswordSignInAsync(existingUser, loginVM.Password, false, false);
        if (!signInResult.Succeeded)
        {
            TempData["error"] = "Invalid credentials";
            return View(loginVM);
        }

        //return RedirectToAction("Index", "Home");
        return RedirectToAction(nameof(HomeController.Index));

    }
}
