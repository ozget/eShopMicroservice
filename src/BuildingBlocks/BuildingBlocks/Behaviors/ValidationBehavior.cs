using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.Cqrs;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors
{
    //where komut ile Requiest lerin sadece command(create,update,delete) lar için olacagını söylüyoruz
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
    {
        //tüm hataları buradan kontrol ediyoruz
        //ilk parametre client dan gelir,2. pipeline dan gelir
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);//requesten gelen içerige erişiyoruz
            var validationResults= await Task.WhenAll(validators.Select(v=>v.ValidateAsync(context,cancellationToken)));// tüm validation ları validationResults da topluyoruz
        
        
            var failures= validationResults.Where(r=>r.Errors.Any())
                                            .SelectMany(r=>r.Errors)
                                            .ToList();

            if (failures.Any())
                throw new ValidationException(failures);

            return await next();
        }
    }
}
