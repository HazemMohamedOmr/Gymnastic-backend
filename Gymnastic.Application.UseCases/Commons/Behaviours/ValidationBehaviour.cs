﻿using FluentValidation;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Application.UseCases.Commons.Exceptions;
using MediatR;

namespace Gymnastic.Application.UseCases.Commons.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                Console.WriteLine(validationResults.SelectMany(r => r.Errors).ToList());
                var failures = validationResults
                    .Where(r => r.Errors.Any())
                    .SelectMany(r => r.Errors)
                    .Select(r => new BaseError()
                    {
                        PropertyMessage = r.CustomState is not null ? r.CustomState.ToString() : r.PropertyName,
                        ErrorMessage = r.ErrorMessage
                    })
                    .ToList();

                if (failures.Any())
                    throw new ValidationExceptionCustom(failures);
            }
            return await next();
        }
    }
}
