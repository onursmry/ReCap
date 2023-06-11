using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICarService _carService;

        public RentalManager(IRentalDal rentalDal,ICarService carService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            IResult result= BusinessRules.Run(CheckIfRentAble(rental.CarId),CheckIfFindeksEnough(rental.CustomerId,rental.CarId));
            if (result!=null)
            {
                return result;
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }

        private IResult CheckIfRentAble(int carId)
        {
            var checkedRental = _rentalDal.Get(r => r.CarId == carId&&r.ReturnDate==null);
            if (checkedRental!=null)
            {
                return new ErrorResult(Messages.CarNotReturned);
            }
            return new SuccessResult();
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<List<Rental>> GetAllByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CustomerId == customerId),Messages.RentalDetailListed);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(), Messages.RentalDetailListed);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetailsById(int rentalId)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(r => r.Id == rentalId), Messages.RentalDetailListed);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }
        private IDataResult<int> GetCustomerFindeks(int customerId)
        {
            var customerRentals = GetAllByCustomerId(customerId);
            int customerFindeks = customerRentals.Data.Count * 275;
            if (customerFindeks>1900)
            {
                customerFindeks = 1900;
            }
            return new SuccessDataResult<int>(customerFindeks);
        }

        private IResult CheckIfFindeksEnough(int customerId,int carId)
        {
            int customerFindeks = GetCustomerFindeks(customerId).Data;
            int carFindeks = _carService.GetCarFindeks(carId).Data;
            if (customerFindeks>=carFindeks)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.FindeksNotEnough);
        }
    }
}
