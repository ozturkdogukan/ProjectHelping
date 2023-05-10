using FluentValidation;
using ProjectHelping.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Business.FluentValidation
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Username).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
            RuleFor(x => x.Phone).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Surname).NotNull().NotEmpty();
            RuleFor(x => x.Role).NotNull().NotEmpty().Must(x => x.Equals("Developer") || x.Equals("Employer")).WithMessage("The \"Role\" field must be specified as \"Developer\" or \"Employer\".");
            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull();
        }
    }
}
