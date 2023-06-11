using Business.Adapters;
using Business.Concrete;
using DataAccess.Conrcrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //RentAddTest();
            //RentalDetailTest();
            //CarTest();
        }

        private static void RentAddTest()
        {
            RentalManager rentalManager = new RentalManager(new EfRentalDal(),new CarManager(new EfCarDal(),new FindeksAdapter()));
            var add = rentalManager.Add(new Rental
            {
                CarId = 3,
                CustomerId = 1,
                RentDate = DateTime.Now
            });
            Console.WriteLine(add.Message);
        }

        private static void RentalDetailTest()
        {
            RentalManager rentalManager = new RentalManager(new EfRentalDal(), new CarManager(new EfCarDal(), new FindeksAdapter()));
            var result = rentalManager.GetRentalDetails();
            foreach (var rental in result.Data)
            {
                Console.WriteLine(rental.ModelYear + "\n" + rental.Brand + "\n" +rental.CarDescription + "\n" + rental.CustomerName+ "\n" + rental.RentDate + "\n" + rental.ReturnDate);
                Console.WriteLine("*****************");
            }
        }
    }
}
