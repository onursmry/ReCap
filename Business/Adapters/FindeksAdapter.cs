using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Adapters
{
    public class FindeksAdapter : IFindeksService
    {
        public int CalculateCarFindeks(Car car)
        {
            int findeksByModelYear =Convert.ToInt32(Math.Ceiling(decimal.Parse(car.ModelYear) / 10));
            int findeksByDailyPrice = Convert.ToInt32(Math.Ceiling(car.DailyPrice / 2));
            int carFindeks = findeksByDailyPrice + findeksByModelYear;
            if (car.DailyPrice<500)
            {
                carFindeks = 0;
            }else if (carFindeks > 1900)
            {
                carFindeks = 1900;
            }
            return carFindeks;
        }
    }
}
