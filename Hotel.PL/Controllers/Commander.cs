using System;
using System.Collections.Generic;
using Hotel.BLL.interfaces;
using Hotel.PL.Controllers.Account;
using Hotel.PL.Controllers.Order;
using Hotel.PL.Controllers.Room;
using Hotel.PL.Controllers.RoomCategory;
using Ninject;
using Hotel.DAL.Interfaces;
using Hotel.DAL.Repositories;
using Hotel.BLL.Services;

namespace Hotel.PL.Controllers
{
    public static class Commander
    {
        private static Dictionary<string, Command> Commands { get; set; }

        public static void Execute()
        {
            Init();
            while (true)
            {
                Console.Write("Enter a command: ");
                var command = Console.ReadLine();
                if (command == null)
                {
                    Console.WriteLine("Please, enter a command");
                    continue;
                }

                if (command == "exit")
                {
                    break;
                }

                if (!Commands.ContainsKey(command))
                {
                    Console.WriteLine("Wrong command!");
                }
                else
                {
                    Commands[command].Execute();
                }
            }
        }

        private static void Init()
        {
            IKernel ninjectKernel = new StandardKernel();

            ninjectKernel.Bind<IUserRepository>().To<UserRepository>();
            ninjectKernel.Bind<IRoomCategoryRepository>().To<RoomCategoryRepository>();
            ninjectKernel.Bind<IRoomRepository>().To<RoomRepository>();
            ninjectKernel.Bind<IOrderRepository>().To<OrderRepository>();
            ninjectKernel.Bind<IUnitOfWork>().To<EFUnitOfWork>();

            ninjectKernel.Bind<IUserService>().To<UserService>();
            ninjectKernel.Bind<IRoomCategoryService>().To<RoomCategoryService>();
            ninjectKernel.Bind<IRoomService>().To<RoomService>();
            ninjectKernel.Bind<IOrderService>().To<OrderService>();

            Commands = new Dictionary<string, Command>
            {
                {"signup", new SignUp(ninjectKernel.Get<UserService>())},
                {"create room category", new CreateRoomCategory(ninjectKernel.Get<RoomCategoryService>(), ninjectKernel.Get<UserService>())},
                {"edit room category", new EditRoomCategory(ninjectKernel.Get<RoomCategoryService>(), ninjectKernel.Get<UserService>())},
                {"delete room category", new DeleteRoomCategory(ninjectKernel.Get<RoomCategoryService>(), ninjectKernel.Get<UserService>())},
                {"create room", new CreateRoom(ninjectKernel.Get<UserService>(), ninjectKernel.Get<RoomService>())},
                {"edit room", new EditRoom(ninjectKernel.Get<UserService>(), ninjectKernel.Get<RoomService>())},
                {"delete room", new DeleteRoom(ninjectKernel.Get<UserService>(), ninjectKernel.Get<RoomService>())},
                {"book room", new BookRoom(ninjectKernel.Get<UserService>(), ninjectKernel.Get<OrderService>())},
                {"rent room", new RentRoom(ninjectKernel.Get<UserService>(), ninjectKernel.Get<OrderService>())},
                {"transform state room", new TransformStateOfRoom(ninjectKernel.Get<UserService>(), ninjectKernel.Get<OrderService>())},
                {"show rooms", new ShowRooms(ninjectKernel.Get<UserService>(), ninjectKernel.Get<OrderService>())}
            };
        }
    }
}
