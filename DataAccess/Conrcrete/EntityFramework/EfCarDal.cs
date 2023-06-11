using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace DataAccess.Conrcrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join co in context.Colors
                             on c.ColorId equals co.ColorId
                             select new CarDetailDto
                             {
                                 Id=c.Id,
                                 CarDescription = c.Description,
                                 BrandName = b.BrandName,
                                 ModelYear = c.ModelYear,
                                 ColorName = co.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 Findeks=c.Findeks
                             };
                return filter == null ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
