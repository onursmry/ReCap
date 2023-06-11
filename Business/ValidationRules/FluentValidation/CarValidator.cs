using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator: AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.BrandId).NotEmpty();
            RuleFor(c => c.ColorId).NotEmpty();
            RuleFor(c => c.DailyPrice).NotEmpty();
            RuleFor(c => c.DailyPrice).GreaterThan(0);
            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c => c.Description).MaximumLength(500);
            RuleFor(c => c.Description).MinimumLength(20);
            RuleFor(c => c.ModelYear).NotEmpty();
            RuleFor(c => c.ModelYear).Length(4);
            RuleFor(c => c.ModelYear).InclusiveBetween("2000","2022");
        }
    }
}
