using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Conrcrete.EntityFramework
{
    public class EfColorDal:EfEntityRepositoryBase<Color,RentACarContext>,IColorDal
    {

    }
}
