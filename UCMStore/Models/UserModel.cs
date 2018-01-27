using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCMStore.Models
{
    public class UserModel
    {
        DBEcomEntities db;
        public UserModel()
        {
            db = new DBEcomEntities();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }

        public List<UserModel> GetList()
        {
            List<UserModel> userList = new List<UserModel>();

            userList = db.Users.Select(m => new UserModel
            {
                UserId = m.UserId,
                Name = m.Name,
                Email = m.Email,
                Mobile = m.Mobile,
                UserName = m.UserName,
                Active = (bool)m.Active
            }).ToList();


            return userList;
        }

        public UserModel GetUserById(int? UserId)
        {
            var user = GetList().FirstOrDefault(m => m.UserId == UserId);
            return user;
        }

        public int Update(UserModel model)
        {
            var user = db.Users.FirstOrDefault(m => m.UserId == model.UserId);

            if (user != null)
            {
                user.Name = model.Name;
                user.Email = model.Email;
                user.Mobile = model.Mobile;
                user.UserName = model.UserName;
                user.Active = model.Active;
                return db.SaveChanges();
            }

            return 0;
        }
    }
}