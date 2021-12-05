using Hotel.BLL.Utils;
using Hotel.PL.Controllers;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System.Web.Mvc;

namespace Hotel.PL
{
    class Program
    {
        private static void Main()
        {
            //NinjectModule registrations = new NinjectRegistrations();
            //var kernel = new StandardKernel(registrations);
            //DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            Commander.Execute();
        }
    }
}
