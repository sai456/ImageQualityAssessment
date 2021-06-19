using FluentValidation;
using ImageQuality.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageQuality.Service
{
    public class ImagesQualitiesRequestValidator : AbstractValidator<ImagesQualitiesRequest>
    {
        public ImagesQualitiesRequestValidator()
        {
            RuleFor(x => x.Urls)
           .Cascade(CascadeMode.StopOnFirstFailure)
               .NotNull()
               .WithErrorCode(FaultCodes.MissingField)
               .WithMessage(ErrorMessages.MissingField("Urls"))
               .NotEmpty()
               .WithErrorCode(FaultCodes.NoUrl)
               .WithMessage(FaultMessages.NoUrl);
        }
    }
}
