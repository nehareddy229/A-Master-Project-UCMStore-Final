using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace UCMStore.Models
{
    public class BrandModel
    {
        DBEcomEntities db;

        public BrandModel()
        {
            db = new UCMStore.DBEcomEntities();
        }

        public int BrandId { get; set; }

        [Required]
        public string Name { get; set; }

        public List<BrandModel> GetList()
        {
            List<BrandModel> brandList = new List<Models.BrandModel>();

            brandList = db.Brands.Select(m => new BrandModel
            {
                BrandId = m.BrandId,
                Name = m.Name
            }).ToList();


            return brandList;
        }

        public BrandModel GetBrandById(int? BrandId)
        {
            var brand = GetList().FirstOrDefault(m => m.BrandId == BrandId);
            return brand;
        }

        public int Add(BrandModel model)
        {
            var brand = new Brand
            {
                Name = model.Name
            };

            db.Brands.Add(brand);
            return db.SaveChanges();
        }

        public int Update(BrandModel model)
        {
            var brand = db.Brands.FirstOrDefault(m => m.BrandId == model.BrandId);

            if (brand != null)
            {
                brand.Name = model.Name;
                return db.SaveChanges();
            }

            return 0;
        }
    }
}