using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillageHut_web_service.API.Model;

namespace VillageHut_web_service.API.Helper
{
    public class InsertUpdateDeleteServices
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();
        public bool IUDServiceDetails(Service recievedSerDetails, List<tbl_type> recievedLstTypes, byte mode)
        {

            var catId = (from cat in dbCache.tbl_categories
                         where cat.catName == recievedSerDetails.catName
                         select cat.catId).FirstOrDefault();

            if (mode == 0) {

                tbl_service serviceTable = new tbl_service();

                serviceTable.serId = recievedSerDetails.serId;
                serviceTable.serName = recievedSerDetails.serName;
                serviceTable.serImg = recievedSerDetails.serImg;
                serviceTable.serIsDeleted = "false";
                serviceTable.catId = catId.ToString();
                serviceTable.serDesc = recievedSerDetails.serDesc;

                dbCache.tbl_services.InsertOnSubmit(serviceTable);

                foreach (var item in recievedLstTypes) {
                    var typeTable = new tbl_type() {
                        serId = recievedSerDetails.serId,
                        typeName = item.typeName,
                        typePricePService = item.typePricePService,
                        typeAvailableQty = item.typeAvailableQty,
                        typeMaxQty = item.typeMaxQty
                    };

                    dbCache.tbl_types.InsertOnSubmit(typeTable);
                }

                dbCache.SubmitChanges();

                return true;

            } else if (mode == 1) {
                tbl_service serviceTable = (from ser in dbCache.tbl_services
                                            where ser.serId == recievedSerDetails.serId
                                            select ser).FirstOrDefault();

                serviceTable.serName = recievedSerDetails.serName;
                serviceTable.serImg = recievedSerDetails.serImg;
                serviceTable.catId = catId.ToString();
                serviceTable.serDesc = recievedSerDetails.serDesc;

                dbCache.tbl_types.Where(x => x.serId == recievedSerDetails.serId).ToList().ForEach(dbCache.tbl_types.DeleteOnSubmit);

                foreach (var item in recievedLstTypes) {
                    var typeTable = new tbl_type() {
                        serId = recievedSerDetails.serId,
                        typeName = item.typeName,
                        typePricePService = item.typePricePService,
                        typeAvailableQty = item.typeAvailableQty,
                        typeMaxQty = item.typeMaxQty
                    };

                    dbCache.tbl_types.InsertOnSubmit(typeTable);
                }

                dbCache.SubmitChanges();

                return true;
            } else if (mode == 2) {
                tbl_service serviceTable = (from ser in dbCache.tbl_services
                                            where ser.serId == recievedSerDetails.serId
                                            select ser).FirstOrDefault();

                serviceTable.serIsDeleted = "true";

                dbCache.SubmitChanges();

                return true;
            }

            return false;
        }
    }
}