using AutoMapper;
using InvenTrac.Helpers;
using InvenTrac.Models;
using InvenTrac.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvenTrac.Controllers;

public class WorkspaceController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public WorkspaceController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager, 
        IMapper mapper
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> WorkspaceDashboard()
    {
        // Display list of dashboard cards

        var model = new LoginVM();
        return View(model);
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

        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).ControllerName());
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

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).ControllerName());
    }

    [HttpGet("Default")]
    public async Task<ActionResult> Default()
    {
        #region Account Roles (Identity framework)
        var accountRoleNames = new List<string>();

        if (await _roleManager.FindByNameAsync("AppUser") == null)
        {
            accountRoleNames.Add("AppUser");
        }
        if (await _roleManager.FindByNameAsync("Admin") == null)
        {
            accountRoleNames.Add("Admin");
        }

        foreach (var accountRoleName in accountRoleNames)
        {
            await _roleManager.CreateAsync(new IdentityRole(accountRoleName));
        }
        #endregion

        #region AppUsers (Identity framework)
        var demoIdentityPassword = "Password!23";

        if (await _userManager.FindByEmailAsync("admin@example.com") == null)
        {
            var adminUser = new AppUser()
            {
                FirstName = "Admin_F",
                LastName = "Admin_L",
                UserName = "Admin_UserName",
                Email = "admin@example.com",
            };
            await _userManager.CreateAsync(adminUser, demoIdentityPassword);
            // After user is created, add role
            var foundUser = await _userManager.FindByEmailAsync(adminUser.Email);
            await _userManager.AddToRoleAsync(foundUser, "AppUser");
            await _userManager.AddToRoleAsync(foundUser, "Admin");
        }

        if (await _userManager.FindByEmailAsync("appuser@example.com") == null)
        {
            var appUser = new AppUser()
            {
                FirstName = "AppUser_F",
                LastName = "AppUser_L",
                UserName = "AppUser_UserName",
                Email = "appuser@example.com",
            };
            await _userManager.CreateAsync(appUser, demoIdentityPassword);
            // After user is created, add role
            var foundUser = await _userManager.FindByEmailAsync(appUser.Email);
            await _userManager.AddToRoleAsync(foundUser, "AppUser");
        }
        #endregion

        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).ControllerName());
    }

}
