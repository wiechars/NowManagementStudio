
namespace NowManagementStudio.Models.Inventory
{
    public class Lots
    {
        public int Id { get; set; }
        public string serialNo { get; set; }
        public string brandId { get; set; }
        public string brand { get; set; }
        public string typeId { get; set; }
        public string type { get; set; }
        public double quantity { get; set; }
        public string uomID { get; set; }
        public string lotStatusId { get; set; }
        public string lotStatus { get; set; }
        public string locationId { get; set; }
        public string location { get; set; }
        public double weight { get; set; }
        public string weightId { get; set; }
        public double width { get; set; }
        public string widthId { get; set; }
        public double height { get; set; }
        public string heightId { get; set; }
        public double volume { get; set; }
        public string volumeId { get; set; }
        public double price { get; set; }
        public string purchaseDate { get; set; }
        public string expirationDate { get; set; }
        public string notes { get; set; }
        public string nextInvDate { get; set; }
        public string nextInvDateId { get; set; }
        public string lastInvDate { get; set; }
        public string lastInvDateId { get; set; }
    }
}