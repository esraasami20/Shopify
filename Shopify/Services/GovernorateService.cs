using Shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Repository
{
    public class GovernorateService
    {
        ShopifyContext _db;
        public GovernorateService(ShopifyContext db)
        {
            _db = db;
        }
        //get all governorate
        public List<Governorate> GetAllGovernorate()
        {
            return _db.Governorates.Where(g=>g.Isdeleted==false).ToList();
            

        }
        // get Governorate by id
        public Governorate GetGovernorate(int id)
        {
            return _db.Governorates.Where(b => b.GovernorateId == id && b.Isdeleted == false).FirstOrDefault();

        }
        // add Governorate 
        public Governorate AddGovernorate(Governorate governorate)
        {
            _db.Governorates.Add(governorate);
            _db.SaveChanges();
            return governorate;
        }

        //  edit Governorate
        public Governorate EditStatus(Governorate governorate)
        {
            Governorate governorateDetails = GetGovernorate(governorate.GovernorateId);
            if (governorateDetails != null)
            {
                governorateDetails.GovernorateName = governorate.GovernorateName;
                governorateDetails.Duration = governorate.Duration;
                governorateDetails.ShippingValue = governorate.ShippingValue;
                _db.SaveChanges();
                return governorateDetails;
            }
            return null;

        }
        //  delete Governorate
        public Governorate DeleteGovernorate(int id)
        {
            Governorate governorateDetails = GetGovernorate(id);
            if (governorateDetails != null)
            {
                governorateDetails.Isdeleted = true;
                _db.SaveChanges();

            }
            return governorateDetails;
        }
    }
}
