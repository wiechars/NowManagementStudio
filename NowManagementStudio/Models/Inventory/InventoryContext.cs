using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NowManagementStudio.DAL;

namespace NowManagementStudio.Models.Inventory
{
    public class InventoryContext : DbContext
    {
        public List<Lots> Inventory
        {

            get
            {
                InventorySprocs sproc = new InventorySprocs();
                return sproc.GetLots();
            }
        }

        public List<Location> Locations
        {

            get
            {
                InventorySprocs sproc = new InventorySprocs();
                return sproc.GetLocations();
            }
        }

        public void UpdateLot(Lots lot)
        {
            InventorySprocs sproc = new InventorySprocs();
            //Build customProp Ids and Values
            string propValsId = lot.weightId + "," + lot.widthId + "," + lot.heightId + "," + lot.volumeId + "," + lot.nextInvDateId + "," + lot.lastInvDateId;
            string propVals = lot.weight + "," + lot.width + "," + lot.height + "," + lot.volume + "," + lot.nextInvDate + "," + lot.lastInvDate;
            sproc.UpdateLot(lot, propValsId, propVals);

        }

        public string AddLot(Lots lot)
        {
            InventorySprocs sproc = new InventorySprocs();
            //Build customProp Ids and Values
            string propValsId = "1,2,3,4,5,6";
            string propVals = lot.weight + "," + lot.width + "," + lot.height + "," + lot.volume + "," + lot.nextInvDate + "," + lot.lastInvDate;
            return sproc.InsertLot(lot.serialNo,lot.price.ToString(),
                    lot.purchaseDate, lot.expirationDate, propValsId, propVals, lot.notes);

        }

        public List<MatCategory> GetMatCategories()
        {
            InventorySprocs sproc = new InventorySprocs();
            return sproc.GetMatCategories();

        }

        public List<MatType> GetMatTypesByCategories(int brandID)
        {
            InventorySprocs sproc = new InventorySprocs();
            return sproc.GetMatTypesByCategories(brandID);

        }

        public void InsertImageMapping(string id, string fileName)
        {
            InventorySprocs sproc = new InventorySprocs();
            sproc.InsertImageMapping(id, fileName);

        }

        public List<ImageMapping> GetImagesByLot(int lotID)
        {
            InventorySprocs sproc = new InventorySprocs();
            return sproc.GetImagesByLot(lotID);

        }

        public List<Lots> SearchSerialNo(string serialNo)
        {
            InventorySprocs sproc = new InventorySprocs();
            return sproc.SearchSerialNo(serialNo);
        }

        public List<Lots> GetInventoryReport(string serialNo)
        {
            InventorySprocs sproc = new InventorySprocs();
            return sproc.GetInventoryReport(serialNo);
        }



    }
}