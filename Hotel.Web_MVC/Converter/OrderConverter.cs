using Hotel.DAL.Entities;
using Hotel.Web_MVC.DTO;

namespace Hotel.Web_MVC.Converter
{
    public class OrderConverter
    {
        public static OrderDto Order2Dto(Order order)
        {
            return new OrderDto
            {
                OrderNumber = order.Id,
                Start = order.Start,
                End = order.End,
                Type = order.Type,
                Room = RoomConverter.Room2Dto(order.Room)
            };
        }
    }
}