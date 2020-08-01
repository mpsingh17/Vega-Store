using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using VegaStore.UI.Areas.Admin.ViewModels.UserViewModels;

namespace VegaStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            //IUserRepository userRepository,
            IMapper mapper)
        {
            //_userRepository = userRepository;
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var usersInDb = await _userManager.Users.ToListAsync();

            var result = usersInDb.Select(u => new ListUserViewModel
            {
                Id = u.Id,
                Email = u.Email,
                Username = u.UserName,
                Roles = _userManager.GetRolesAsync(u).Result
            });

            return View(result);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var userInDb = await _userManager.FindByIdAsync(id);                        

            if(userInDb is null)
            {
                return NotFound();
            }

            var editUserVM = new EditUserViewModel
            {
                Roles = await _userManager.GetRolesAsync(userInDb),
                RoleSLIs = _roleManager.Roles
                    .Select(r => new SelectListItem { Text = r.Name, Value = r.Name })
            };

            return View(editUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel vm, string id)
        {
            var userInDb = await _userManager.FindByIdAsync(id);

            if (userInDb is null)
            {
                return NotFound();
            }

            foreach (var role in vm.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    return NotFound($"{role} role not found");
            }

            var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            await _userManager.RemoveFromRolesAsync(userInDb, allRoles);

            await _userManager.AddToRolesAsync(userInDb, vm.Roles);

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var userInDb = await _userManager.FindByIdAsync(id);

            if (userInDb is null)
            {
                return NotFound("User not found.");
            }

            await _userManager.DeleteAsync(userInDb);

            return Ok($"{userInDb.UserName} User has been deleted.");
        }

    }
}