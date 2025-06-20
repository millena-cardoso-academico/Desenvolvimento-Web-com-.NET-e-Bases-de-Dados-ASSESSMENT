using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AgenciaTurismo.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O nome de usuário é obrigatório")]
            public string Username { get; set; }

            [Required(ErrorMessage = "A senha é obrigatória")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet(string returnUrl = "/")
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = "/")
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Lógica de autenticação com usuário e senha fixos ("hardcoded")
            if (Input.Username == "admin" && Input.Password == "12345")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Input.Username),
                    new Claim(ClaimTypes.Role, "Admin"),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return LocalRedirect(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Login inválido.");
            return Page();
        }
    }
}