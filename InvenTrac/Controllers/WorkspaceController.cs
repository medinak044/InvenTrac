using AutoMapper;
using InvenTrac.Helpers;
using InvenTrac.Models;
using InvenTrac.Models.ViewModels;
using InvenTrac.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvenTrac.Controllers;

[Authorize(Roles = "AppUser")]
public class WorkspaceController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public WorkspaceController(
        IUnitOfWork unitOfWork,
        UserManager<AppUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper
    )
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> WorkspaceDashboard()
    {
        var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
        // Display list of dashboard cards

        var model = new LoginVM();
        return View(model);
    }


    // Create

    // Update
    [HttpGet]
    public async Task<IActionResult> WorkspaceEdit()
    {
        // Dashboard edit form

        var model = new LoginVM();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> WorkspaceEdit(LoginVM loginVM)
    {

        return RedirectToAction(nameof(WorkspaceDashboard));
    }

    // Delete
}
