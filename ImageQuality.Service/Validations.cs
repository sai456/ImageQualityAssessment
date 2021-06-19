using FluentValidation;
using ImageQuality.Model;
using System;
using System.Collections.Generic;
using System.Text;
using static ImageQuality.Model.Errors;

namespace ImageQuality.Service
{
    public static class Validations
    {
        public static void EnsureValid<TRequest>(TRequest request, IValidator<TRequest> validator)
        {
            var validationError = ClientSide.ValidationFailure();

            if (request == null)
                throw validationError;

            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                foreach (var validationFailure in validationResult.Errors)
                {
                    validationError.Infos.Add(new Info(validationFailure.ErrorCode, validationFailure.ErrorMessage));
                }
                throw validationError;
            }
        }
    }
}
