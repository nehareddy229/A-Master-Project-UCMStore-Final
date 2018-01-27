using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace UCMStore.Models
{
    public class ProductModel
    {
        DBEcomEntities db;

        public ProductModel()
        {
            db = new UCMStore.DBEcomEntities();
        }

        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal? Price { get; set; }
        public string ProductImage { get; set; }
        public HttpPostedFileBase Image { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int? BrandId { get; set; }
        public string BrandName { get; set; }
        public bool Active { get; set; }
        public bool IsAccessories { get; set; }
        public string CreatedOn { get; set; }
        public List<BrandModel> BrandList { get; set; }
        public IEnumerable<ProductModel> ProductList { get; set; }
        public IEnumerable<ProductModel> AccessoriesList { get; set; }

        public List<ProductModel> GetList()
        {
            List<ProductModel> productList = new List<ProductModel>();
            var list = db.Products.AsQueryable();
            productList = list.Select(m => new ProductModel
            {
                ProductId = m.ProductId,
                Name = m.Name,
                Price = m.Price,
                Description = m.Description,
                ProductImage = m.Image,
                BrandId = m.BrandId,
                BrandName = m.Brand.Name,
                Active =(bool)m.Active,
                IsAccessories = (bool)m.IsAccessories
            }).ToList();

            return productList;
        }

        public ProductModel GetProductById(int? Id)
        {
            var product = GetList().FirstOrDefault(m => m.ProductId == Id);
            return product;
        }

        public int Add(ProductModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Image = model.ProductImage,
                BrandId = model.BrandId,
                Active = model.Active,
                IsAccessories = model.IsAccessories
            };

            db.Products.Add(product);
            return db.SaveChanges();
        }

        public int Update(ProductModel model)
        {
            var product = db.Products.FirstOrDefault(m => m.ProductId == model.ProductId);
            if (product != null)
            {
                product.Name = model.Name;
                product.Price = model.Price;
                product.Description = model.Description;
                product.Image = model.ProductImage;
                product.BrandId = model.BrandId;
                product.Active = model.Active;
                product.IsAccessories = model.IsAccessories;

                return db.SaveChanges();
            }

            return 0;
        }
    }
}