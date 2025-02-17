﻿using System;
using System.Collections.Generic;
using Hotel.DAL.Entities;

namespace Hotel.BLL.interfaces
{
    public interface IOrderService
    {
        void BookRoomById(int roomId, User user, DateTime startDate, DateTime endDate);
        void RentRoomById(int roomId, User user, DateTime startDate, DateTime endDate);
        void TransformFromBookedToRentedById(int orderId);
        IEnumerable<Room> GetFreeRooms(DateTime startDate, DateTime endDate);
        Order FindById(int id);
        bool IsExistsById(int id);
        IEnumerable<Order> GetAll();
        void DeleteById(int id);
    }
}
