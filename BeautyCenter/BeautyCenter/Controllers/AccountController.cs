﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BeautiyCenter.Entity.Concrete.Identity;
using BeautyCenter.Presentation.ViewModels;
using BeautiyCenter.DataAccess.Context;

namespace BeautyCenter.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly BeautyCenterContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, BeautyCenterContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Kullanıcıyı kontrol et
            var user = await _userManager.FindByEmailAsync(model.EmailAddress);

            if (user == null)
            {
                // Kullanıcı yoksa yeni bir kullanıcı oluştur
                user = new ApplicationUser
                {
                    UserName = model.EmailAddress,
                    Email = model.EmailAddress,
                    FullName = "Yeni Kullanıcı",
                    EmailConfirmed = true
                };

                // Varsayılan şifre ile kullanıcıyı oluştur
                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Kullanıcı oluşturulurken hata oluştu.");
                    return View(model);
                }
            }

            // Kullanıcı giriş yapmaya çalışıyor
            var signInResult = await _signInManager.PasswordSignInAsync(model.EmailAddress, model.Password, false, false);

            if (signInResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Şifre yanlış.");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var userExists = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (userExists != null)
            {
                TempData["Error"] = "Bu e-posta adresi zaten kullanılıyor.";
                return View(registerViewModel);
            }

            var newUser = new ApplicationUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress,
                FullName = registerViewModel.FullName
            };

            var result = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "User");
                TempData["Success"] = "Kayıt başarılı! Şimdi giriş yapabilirsiniz.";
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(registerViewModel);
        }

    [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}