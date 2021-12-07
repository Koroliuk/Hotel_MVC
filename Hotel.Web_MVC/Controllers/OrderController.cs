using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;
using Hotel.Web_MVC.Converter;
using Hotel.Web_MVC.Utils;
using System;
using System.Linq;
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

        [Route("room/{roomId}/book")]
        [HttpGet]
        public ActionResult Book(int roomId)
        {
            var room = _roomService.FindById(roomId);
            var roomDto = RoomConverter.Room2Dto(room);
            ViewBag.Room = roomDto;
            return View();
        }

        [Route("room/{roomId}/book")]
        [HttpPost]
        public RedirectResult Book(int roomId, string startDateString, string endDateString, bool isPaid=false)
        {
            if (StringUtils.IsBlank(startDateString) || StringUtils.IsBlank(endDateString))
            {
                TempData["Message"] = "Please provide input dates";
            }
            try
            {
                var startDate = DateTime.Parse(startDateString);
                var endDate = DateTime.Parse(endDateString);
                if (!isPaid)
                {
                    _orderService.BookRoomById(roomId, null, startDate, endDate);
                }
                else
                {
                    _orderService.RentRoomById(roomId, null, startDate, endDate);

                }
                return Redirect("/orders");
            }
            catch (HotelException e)
            {
                TempData["Message"] = e.Message;
            }
            catch
            {
                TempData["Message"] = "Please, check input dates";
            }
            return Redirect(String.Format("/room/{0}/book", roomId));
        }

        public ActionResult List()
        {
            var orders = _orderService.GetAll()
                .Select(order => OrderConverter.Order2Dto(order));
            ViewBag.Orders = orders;
            return View();
        }

        [Route("room/{orderId}/pay")]
        public RedirectResult Pay(int orderId)
        {
            _orderService.TransformFromBookedToRentedById(orderId);
            return Redirect("/orders");
        }

        [Route("room/{orderId}/cancel")]
        public RedirectResult Cancel(int orderId)
        {
            _orderService.DeleteById(orderId);
            return Redirect("/orders");
        }

    }
}
