using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Appointments.Api.Models
{
    // Models used as parameters to AccountController actions.

    /// <summary>
    /// AddExternalLoginBindingModel
    /// </summary>
    public class AddExternalLoginBindingModel
    {
        /// <summary>
        /// External Access Token
        /// </summary>
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

    /// <summary>
    /// ChangePasswordBindingModel
    /// </summary>
    public class ChangePasswordBindingModel
    {
        /// <summary>
        /// Old Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        /// <summary>
        /// New Password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Confirm Password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// RegisterBindingModel
    /// </summary>
    public class RegisterBindingModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Confirm Password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// RegisterWithRoleBindingModel
    /// </summary>
    public class RegisterWithRoleBindingModel {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Confirm Password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Role: Admin / Manager / Client
        /// <see cref="Utils.AppRoles"/>
        /// </summary>
        [Display(Name = "Role")]
        public string Role { get; set; }
    }

    /// <summary>
    /// RegisterExternalBindingModel
    /// </summary>
    public class RegisterExternalBindingModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    /// <summary>
    /// RemoveLoginBindingModel
    /// </summary>
    public class RemoveLoginBindingModel
    {
        /// <summary>
        /// LoginProvider
        /// </summary>
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        /// <summary>
        /// ProviderKey
        /// </summary>
        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }

    /// <summary>
    /// SetPasswordBindingModel
    /// </summary>
    public class SetPasswordBindingModel
    {
        /// <summary>
        /// New Password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Confirm Password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
