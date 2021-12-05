using Hotel.DAL.Repositories;
using System;

namespace ConsoleApp1
{
    class Program : IDisposable
    {
        static void Main(string[] args) 
        {
            using (EFUnitOfWork un = new EFUnitOfWork())
            {
                Console.WriteLine("Kind of oK");
            }
        }

        public void Dispose()
        {
       
        }
    }
}
