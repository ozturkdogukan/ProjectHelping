using FluentValidation;
using ProjectHelping.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.Business.FluentValidation
{
    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Givenname).NotNull().NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull();
        }
    }
}
