using Hotel.DAL.Entities;
using Hotel.Web_MVC.DTO;

namespace Hotel.Web_MVC.Converter
{
    public class RoomConverter
    {
        public static RoomDto Room2Dto(Room room)
        {
            return new RoomDto
            {
                RoomNumber = room.Id,
                Category = RoomCategoryConverter.RoomCagetory2Dto(room.RoomCategory)
            };
        }
    }
}