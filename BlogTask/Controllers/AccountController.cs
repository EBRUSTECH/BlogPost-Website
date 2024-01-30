using BlogTask.Authorization;
using BlogTask.Models.Entities;
using BlogTask.Models.ViewModels;
using BlogTask.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BlogTask.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)

                {
                    // add Role to new user
                    await _userManager.AddToRoleAsync(user, "regular");

                    //send a mail with email confirmation link                
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var link = Url.Action("ConfirmEmail", "Account", new {user.Email, token}, Request.Scheme);
                    var body = $@"Hi {user.FirstName},
                    Please click the link <a href='{link}'>here</a> to confirm your account's email";

                    await _emailService.SendEmailAsync(user.Email, "Confirm Email", body);

                    return RedirectToAction("RegisterCongrats", "Account", new {name=user.FirstName});
                }
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
            }
            
            return  View(model);
        }

        [HttpGet]
        public IActionResult RegisterCongrats(string name)
        {
            ViewBag.Name = name;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string Email, string token)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if(user != null)
            {
               var confirmEmailResult = await _userManager.ConfirmEmailAsync(user, token);
                if (confirmEmailResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var err in confirmEmailResult.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }

                return View();
            }

            ModelState.AddModelError("", "Email confirmation failed");
            return View();
            
        }
     
        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl)
        {
            if (returnUrl != null)
            
                ViewBag.ReturnUrl = returnUrl;

                return View();
                      
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if (ModelState.IsValid) 
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {

                    if(await _userManager.IsEmailConfirmedAsync(user))
                    {
                        var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                        if (loginResult.Succeeded)
                        {

                            if (!string.IsNullOrEmpty(returnUrl))
                            {
                                return LocalRedirect(returnUrl);
                            }

                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
                            ModelState.AddModelError("Login error", "Invalid credential");
                        }
                    }
                    else
                    {

                        ModelState.AddModelError("", "Email not confirmed yet!");
                   
                    }

                }

                ModelState.AddModelError("Login error", "Invalid credential");

            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if(user != null)
                {
                    //send a mail with email confirmation link                
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var link = Url.Action("PasswordReset", "Account", new { user.Email, token }, Request.Scheme);
                    var body = $@"Hi {user.FirstName},
                    You requested to reset your password. Click the link <a href='{link}'>here</a> to reset your password";

                    await _emailService.SendEmailAsync(user.Email, "Forgot Password", body);

                    ViewBag.Message = "Reset password link has been sent to the email provided. If correct you should already get it by now";

                    return View();
                }

                ModelState.AddModelError("", "Invalid Email");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult PasswordReset(string Email, string token)
        {
            var resetPasswordModel = new ResetPasswordViewModel { Email = Email, Token = token };    
            return View(resetPasswordModel);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (ModelState.IsValid)
            {
               var resetPasswordResult = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                if(resetPasswordResult.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach(var err in resetPasswordResult.Errors)
                    {
                        ModelState.AddModelError(err.Code, err.Description);
                    }
                }

                ModelState.AddModelError("", "Invalid Email");
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
