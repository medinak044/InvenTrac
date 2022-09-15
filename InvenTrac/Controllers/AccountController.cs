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
        return View(model);
    }

    #region Demo Login
    [HttpGet("Login/{email}")]
    public async Task<IActionResult> Login(string email)
    {
        var model = new LoginVM()
        {
            Email = email,
            Password = "Password!23" // Default demo user password
        };
        return View(model);
    } 
    #endregion

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



    [HttpGet]
    public async Task<IActionResult> SignUp()
    {
        var model = new SignUpVM();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpVM registerVM)
    {
        if (!ModelState.IsValid)
            return View(registerVM);

        // Check if email already exists
        var existingUser = await _userManager.FindByEmailAsync(registerVM.Email);
        if (existingUser != null)
        {
            TempData["error"] = "This email is already in use";
            return View(registerVM);
        }

        // Map values
        var newUser = _mapper.Map<AppUser>(registerVM);

        var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
        if (!newUserResponse.Succeeded)
        {
            TempData["error"] = "Server error";
            return View(registerVM);
        }

        // Assign default role to new user (Make sure roles exist in database first)
        await _userManager.AddToRoleAsync(newUser, "AppUser");

        // Log user in as a convenience
        var user = await _userManager.FindByEmailAsync(registerVM.Email); // Track new user from db
        var isSignedIn = await _signInManager.PasswordSignInAsync(user, registerVM.Password, false, false);
        if (!isSignedIn.Succeeded)
        {
            TempData["error"] = "Something went wrong while logging in. Please try again";
            return RedirectToAction("Login");
        }

        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).ControllerName());
    }
}
