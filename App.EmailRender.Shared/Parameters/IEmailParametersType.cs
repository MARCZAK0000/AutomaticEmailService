using App.EmailRender.Shared.Abstraction;
using App.EmailRender.Shared.Strategy;

namespace App.EmailRender.Shared.Parameters
{
    public interface IEmailParametersType
    {
        Type GetEmailParametersType<TEnum, TEmailParameters, TEmailTemplate>(TEnum stategyEnum, Dictionary<TEnum, EmailBuilderMetadata<TEmailTemplate, TEmailParameters>> strategyDictionary)
            where TEnum : Enum where TEmailParameters : IEmailParameters where TEmailTemplate : IEmailTemplate;
    }
}
