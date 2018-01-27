using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace UCMStore.Models
{
    public class DealModel
    {
        DBEcomEntities db;

        public DealModel()
        {
            db = new DBEcomEntities();
        }

        public int DealId { get; set; }
        public string Title { get; set; }
        public string DealImage { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public bool Active { get; set; }

        public List<DealModel> GetList()
        {
            List<DealModel> dealList = new List<Models.DealModel>();

            dealList = db.Deals.Select(m => new DealModel
            {
                DealId = m.DealId,
                Title = m.Title,
                DealImage = m.Image,
                Active = (bool)m.Active
            }).ToList();

            return dealList;
        }

        public DealModel GetDealById(int? Id)
        {
            var deal = GetList().FirstOrDefault(m => m.DealId == Id);
            return deal;
        }

        public int Add(DealModel model)
        {
            var deal = new Deal
            {
                Title = model.Title,
                Image = model.DealImage,
                Active = model.Active
            };

            db.Deals.Add(deal);
            return db.SaveChanges();
        }

        public int Update(DealModel model)
        {
            var deal = db.Deals.FirstOrDefault(m => m.DealId == model.DealId);
            if (deal != null)
            {
                deal.Title = model.Title;
                deal.Active = model.Active;
                deal.Image = model.DealImage;
                return db.SaveChanges();
            }

            return 0;
        }
    }
}