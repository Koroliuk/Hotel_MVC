using Hotel.BLL.interfaces;
using System;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Web_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderService _orderService;

        public HomeController(IOrderService orderService) : base()
        {
            _orderService = orderService;
        }

        public HomeController()
        {
        }

        public ActionResult Index(string startDateString=null, string endDateString=null)
        {
            if (startDateString == null && endDateString == null)
            {
                return View();
            }

            var startDate = startDateString == null ? DateTime.MinValue : DateTime.Parse(startDateString);
            var endDate = endDateString == null ? DateTime.MaxValue : DateTime.Parse(endDateString);

            var rooms = _orderService.GetFreeRooms(startDate, endDate);
            foreach (var room in rooms)
            {
                Console.WriteLine(room.Id);
            }
            ViewBag.Rooms = rooms;
            return View();
        }
    }
}