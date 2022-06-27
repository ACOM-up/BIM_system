using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillageHut_web_service.API.Model;

namespace VillageHut_web_service.API.Helper
{
    public class Services
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();

        public List<Service> retrieveAllServices()
        {
            var query = from ser in dbCache.tbl_services
                        join cat in dbCache.tbl_categories on ser.catId equals cat.catId
                        where ser.serIsDeleted.Equals("false")
                        select new Service(){
                            serId = ser.serId,
                            catId = ser.catId,
                            serName = ser.serName,
                            serImg = ser.serImg,
                            serIsItem = cat.catSerIsItem,
                            serIsDeleted = ser.serIsDeleted,
                            catName = cat.catName,
                            serDesc = ser.serDesc
                        };

            return query.ToList();
        }

        public List<Service> searchServices(string input)
        {
            var query = from ser in dbCache.tbl_services
                        join cat in dbCache.tbl_categories on ser.catId equals cat.catId
                        where ser.serIsDeleted.Equals("false") &&
                        (ser.serName.Contains(input) || 
                        ser.serId.Contains(input) || 
                        cat.catName.Contains(input))
                        select new Service()
                        {
                            serId = ser.serId,
                            catId = ser.catId,
                            serName = ser.serName,
                            serImg = ser.serImg,
                            serIsItem = cat.catSerIsItem,
                            serIsDeleted = ser.serIsDeleted,
                            catName = cat.catName,
                            serDesc = ser.serDesc
                        };

            return query.ToList();
        }

    }
}