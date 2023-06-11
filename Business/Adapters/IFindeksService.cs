using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Adapters
{
    public interface IFindeksService
    {
        int CalculateCarFindeks(Car car);
    }
}
