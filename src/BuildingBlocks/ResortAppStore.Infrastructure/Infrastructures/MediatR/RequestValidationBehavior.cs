﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Common.Infrastructures.MediatR {
  public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
      where TRequest : IRequest<TResponse> {
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    //private readonly ILog _log;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators//,ILog log
      ) {
      _validators = validators;
      //_log = log;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {

      var context = new ValidationContext<TRequest>(request);
      var name = typeof(TRequest).Name;
      var failures = _validators
          .Select(v => v.Validate(context))
          .SelectMany(result => result.Errors)
          .Where(f => f != null)
          .ToList();

      if (failures.Count != 0) {
        var message = $"HelpApp Long Running Request: {name} \n";
        foreach (var failure in failures) {
          message += $" {failure.ErrorMessage} \n";
        }
        var errors = failures.Select(s => new ErrorResult(s.PropertyName, s.ErrorMessage)).ToArray();

        throw new ApiException(ApiExeptionType.ValidationError, errors);
      }

      return next();
    }
  }
}