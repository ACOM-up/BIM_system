using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillageHut_web_service.API.Model;

namespace VillageHut_web_service.API.Helper
{
    public class Types
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();

        public List<Model.Type> retrieveTypes(string serId) {
            var query = from type in dbCache.tbl_types
                        where type.serId.Equals(serId)
                        select new Model.Type() {
                            typeName = type.typeName,
                            typePricePItem = (double)type.typePricePService,
                            typeAvailableQty = Convert.ToInt32(type.typeAvailableQty),
                            typeId = Convert.ToInt32(type.typeId),
                            typeMaxQty = Convert.ToInt32(type.typeMaxQty),
                            typeSerId = type.serId
                        };
            return query.ToList();
        }
    }
}