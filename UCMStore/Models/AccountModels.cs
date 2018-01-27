using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace UCMStore.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public bool ChangePassword(string OldPassword, string NewPassword)
        {
            DBEcomEntities db = new UCMStore.DBEcomEntities();
            string userName = HttpContext.Current.User.Identity.Name;
            var user = db.Users.Where(m => m.Active == true).FirstOrDefault(m => m.UserName == userName && m.Password == OldPassword);
            if (user != null)
            {
                // System.Web.HttpContext.Current.Session["UserId"] = user.UserId;
                user.Password = NewPassword;
                db.SaveChanges();

            }
            return user == null ? false : true;
        }

    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public bool ValidateUser(string userName, string password)
        {
            DBEcomEntities db = new UCMStore.DBEcomEntities();
            var user = db.Users.Where(m => m.Active == true).FirstOrDefault(m => m.UserName == userName && m.Password == password);
            if (user != null)
                System.Web.HttpContext.Current.Session["UserId"] = user.UserId;
            return user == null ? false : true;
        }        
    }
    

    public class RegisterModel
    {
        DBEcomEntities db;

        public RegisterModel()
        {
            db = new UCMStore.DBEcomEntities();
        }

        [Required]
        [Display(Name = "User name")]
        [RegularExpression("^[700]{3}\\d{6}", ErrorMessage = "User name must be start with 700 and length must be 9.")]
        public string UserName { get; set; }

        [Required]  
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [RegularExpression(".+@ucmo.edu", ErrorMessage = "You should use UCM Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mobile No")]
        [MaxLength(10)]
        [MinLength(10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile No must be numeric")]
        public string Mobile { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; }

        public int Register(RegisterModel model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                Mobile = model.Mobile,
                Name = model.Name,
                Active = true
            };

            db.Users.Add(user);
            int result = db.SaveChanges();
            if (result > 0)
                System.Web.HttpContext.Current.Session["UserId"] = user.UserId;

            return result;
        }
    }
}
