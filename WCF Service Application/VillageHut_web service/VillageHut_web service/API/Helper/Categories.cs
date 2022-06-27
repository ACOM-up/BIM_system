using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillageHut_web_service.API.Model;


namespace VillageHut_web_service.API.Helper
{
    public class Categories
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();

        public List<Category> retrieveCategories()
        {
            var query = from cat in dbCache.tbl_categories
                        select new Category() {
                            catId = cat.catId,
                            catName = cat.catName
                        };
            return query.ToList();
        }
    }
}