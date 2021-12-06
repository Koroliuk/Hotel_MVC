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
        public RedirectResult Book(int roomId, string startDateString, string endDateString, bool isPaid=false)
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
            return Redirect("/Order/Success");
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult List()
        {
            var orders = _orderService.GetAll();
            ViewBag.Orders = orders;
            return View();
        }

        public RedirectResult Rent(int orderId)
        {
            _orderService.TransformFromBookedToRentedById(orderId);
            return Redirect("/Order/List");
        }

        public RedirectResult Cancel(int orderId)
        {
            _orderService.DeleteById(orderId);
            return Redirect("/Order/List");
        }

    }
}
