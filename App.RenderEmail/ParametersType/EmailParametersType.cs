using App.EmailRender.Shared.Abstraction;
using App.EmailRender.Shared.Parameters;
using App.EmailRender.Shared.Strategy;

namespace App.RenderEmail.ParametersType
{
    public class EmailParametersType : IEmailParametersType
    {

        public Type GetEmailParametersType<TEnum, TEmailParameters, TEmailTemplate>(TEnum stategyEnum, Dictionary<TEnum, EmailBuilderMetadata<TEmailTemplate, TEmailParameters>> strategyDictionary)
            where TEnum : Enum
            where TEmailParameters : IEmailParameters
            where TEmailTemplate : IEmailTemplate
        {
            if (strategyDictionary is null)
            {
                throw new ArgumentNullException(nameof(strategyDictionary), "Issue with the strategy dictionary");
            }
            if (!strategyDictionary.TryGetValue(stategyEnum, out EmailBuilderMetadata<TEmailTemplate, TEmailParameters>? strategy))
            {
                throw new KeyNotFoundException($"Strategy not found for {stategyEnum} in Email Parameters");
            }
            return strategy.Parameters.GetType();
        }
    }
}
