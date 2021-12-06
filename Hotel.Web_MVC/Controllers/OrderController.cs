using Hotel.BLL.interfaces;
using System;
using System.Web.Mvc;

namespace Hotel.Web_MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IOrderService _orderService;

        public OrderController(IRoomService roomService, IOrderService orderService) : base()
        {
            _roomService = roomService;
            _orderService = orderService;
        }
  
        public OrderController()
        {
        }

        [HttpGet]
        public ActionResult Book(int roomId)
        {
            var room = _roomService.FindById(roomId);
            ViewBag.Room = room;
            return View();
        }

        [HttpPost]
        public ActionResult Book(int roomId, string startDateString, string endDateString, bool isPaid=false)
        {
            var startDate = DateTime.Parse(startDateString);
            var endDate = DateTime.Parse(endDateString);
            if (isPaid)
            {
                _orderService.BookRoomById(roomId, null, startDate, endDate);
            }
            else
            {
                _orderService.RentRoomById(roomId, null, startDate, endDate);

            }
            return Redirect("/Home/Index");
        }
    }
}
