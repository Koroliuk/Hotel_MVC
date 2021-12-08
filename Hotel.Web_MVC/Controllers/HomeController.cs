using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;
using Hotel.Web_MVC.Converter;
using Hotel.Web_MVC.Utils;
using System;
using System.Linq;
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
            bool isStartDateStringEmpty = StringUtils.IsBlank(startDateString);
            bool isEndDateStringEmpty = StringUtils.IsBlank(endDateString);

            if (isStartDateStringEmpty && isEndDateStringEmpty)
            {
                return View();
            }
            try
            {
                var startDate = isStartDateStringEmpty ? DateTime.Now : DateTime.Parse(startDateString);
                var endDate = isEndDateStringEmpty ? DateTime.MaxValue : DateTime.Parse(endDateString);
                var rooms = _orderService.GetFreeRooms(startDate, endDate)
                    .Select(room => RoomConverter.Room2Dto(room));
                ViewBag.MessageSuccess = CalcSucessMessage(startDate, endDate);
                ViewBag.Rooms = rooms;
            } catch (HotelException e)
            {
                ViewBag.MessageError = e.Message;
            }
            return View();
        }

        private string CalcSucessMessage(DateTime startDate, DateTime endDate)
        {
            string from = "from now";
            string to = "";

            if (startDate.Date != DateTime.Now.Date)
            {
                from = String.Format("from {0}", startDate.ToString("yyyy-MM-dd"));
            }

            if (endDate.Date != DateTime.MaxValue.Date)
            {
                to = String.Format("to {0}", startDate.ToString("yyyy-MM-dd"));
            }
   
            return String.Format("Free rooms {0} {1}", from, to);
        }
    }
}