using Microsoft.EntityFrameworkCore;
using Shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Repository
{
    public class StatusRepo
    {
        ShopifyContext _db;
        public StatusRepo(ShopifyContext db)
        {
            _db = db;
        }
        public List<Status> GetAllStatus()
        {
            return _db.Statuses.ToList();

        }
        // get Status by id
        public Status GetStatus(int id)
        {
            return _db.Statuses.Where(b => b.StatusId == id && b.Isdeleted == false).FirstOrDefault();

        }
        // add Status 
        public Status AddStatus(Status status)
        {
            _db.Statuses.Add(status);
            _db.SaveChanges();
            return status;
        }

        //  edit Status
        public Status EditStatus(Status status)
        {
            Status statusDetails = GetStatus(status.StatusId);
            if (status != null)
            {
                statusDetails.StatusName = status.StatusName;
                _db.SaveChanges();
                return statusDetails;
            }
            return null;

        }
        //  delete Status
        public Status DeleteStatus(int id)
        {
            Status statusDetails = GetStatus(id);
            if (statusDetails != null)
            {
                statusDetails.Isdeleted = true;
                _db.SaveChanges();

            }
            return statusDetails;
        }

    }
}
