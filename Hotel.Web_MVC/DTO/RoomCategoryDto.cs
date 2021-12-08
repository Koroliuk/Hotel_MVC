namespace Hotel.Web_MVC.DTO
{
    public class RoomCategoryDto
    {
        public int CategoryNumber { get; set; }
        public string Name { get; set; }
        public decimal PricePerDay { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
    }
}
