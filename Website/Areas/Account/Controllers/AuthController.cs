using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Website.ViewModels.Auth;

namespace Website.Areas.Account.Controllers
{
    [Area("Account")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;

        public AuthController(
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _emailSender = emailSender;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                    return View(model);
                }

                // Sign user in with his credentials
                if (await _signInManager.CheckPasswordSignInAsync(user, model.Password, true) == Microsoft.AspNetCore.Identity.SignInResult.Success)
                {
                    await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

                    // Get roles for the user
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                    }
                    //else if (roles.Contains("Manager"))
                    //{
                    //    return RedirectToAction("Index", "Manager", new { area = "Management" });
                    //}
                    else
                    {
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Email or password.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "User login unsuccessful!");
                return View(model);

            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // STEP 1: Show ForgotPassword form
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // STEP 2: Handle ForgotPassword request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Do not reveal that the user does not exist or is not confirmed
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // Generate reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Build reset link
            var resetLink = Url.Action("ResetPassword", "Auth",
                new { token, email = model.Email }, Request.Scheme);

            // TODO: Send resetLink by email
            Console.WriteLine(resetLink); // For testing

            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // STEP 3: Reset password page
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
                return RedirectToAction("Index", "Home");

            var model = new ResetPasswordViewModel { Token = token, Email = email };
            return View(model);
        }

        // STEP 4: Handle ResetPassword form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
                return RedirectToAction(nameof(ResetPasswordConfirmation));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View();
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // STEP 1: Show registration form
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // STEP 2: Register user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Create email confirmation token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                // Create link
                var confirmationLink = Url.Action("ConfirmEmail", "Auth",
                    new { userId = user.Id, token = token },
                    protocol: Request.Scheme);

                // Send email
                await _emailSender.SendEmailAsync(
                    model.Email,
                    "Confirm your email",
                    $"Please confirm your email by clicking <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>here</a>."
                );

                return RedirectToAction("RegistrationConfirmation");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }

        // After registration page
        [HttpGet]
        public IActionResult RegistrationConfirmation()
        {
            return View();
        }

        // STEP 3: Email confirmation endpoint
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
                return RedirectToAction("Index", "Home");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("User not found");

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
                return RedirectToAction("EmailConfirmed");

            return BadRequest("Invalid or expired token");
        }

        // Confirmation successful page
        [HttpGet]
        public IActionResult EmailConfirmed()
        {
            return View();
        }
    }
}
