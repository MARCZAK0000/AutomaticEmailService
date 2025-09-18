
using App.EmailRender.Shared.Abstraction;
using App.EmailRender.Shared.Strategy;
using App.RenderEmail.RenderExceptons;

namespace App.RenderEmail.Strategy
{
    /// <summary>
    /// Provides functionality for rendering email templates based on a specified strategy.
    /// </summary>
    /// <remarks>This class implements the <see cref="IEmailRenderStrategy"/> interface and allows users to
    /// retrieve email templates associated with specific strategies. Strategies are identified using enumeration
    /// values, which are mapped to their corresponding templates in a dictionary.</remarks>
    public class EmailRenderStrategy : IEmailRenderStrategy
    {
        public EmailRenderStrategy() { }

        /// <summary>
        /// Retrieves and renders an email template based on the specified strategy enumeration value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enumeration used to identify the strategy. Must derive from <see cref="System.Enum"/>.</typeparam>
        /// <typeparam name="TEmailParameters">The type of the email parameters associated with the strategy. Must implement <see
        /// cref="IEmailParameters"/>.</typeparam>
        /// <param name="stategyEnum">The enumeration value representing the desired strategy.</param>
        /// <param name="strategyDictionary">A dictionary mapping enumeration values to their corresponding email template metadata.  The dictionary must
        /// contain the specified <paramref name="stategyEnum"/> key.</param>
        /// <returns>The email template associated with the specified strategy.</returns>
        /// <exception cref="StrategyDictionaryNullException">Thrown if <paramref name="strategyDictionary"/> is <see langword="null"/>.</exception>
        /// <exception cref="StrategyNotFoundException">Thrown if the specified <paramref name="stategyEnum"/> does not exist in <paramref
        /// name="strategyDictionary"/>.</exception>
        public IEmailTemplate RenderStrategy<TEnum, TEmailParameters>(TEnum stategyEnum, Dictionary<TEnum, EmailBuilderMetadata<IEmailTemplate, TEmailParameters>> strategyDictionary)
            where TEnum : Enum
            where TEmailParameters : IEmailParameters
        {
            if(strategyDictionary is null)
            {
                throw new StrategyDictionaryNullException("Issue with the strategy dictionary");
            }
            if (!strategyDictionary.TryGetValue(stategyEnum, out EmailBuilderMetadata<IEmailTemplate, TEmailParameters>? strategy))
            {
                throw new StrategyNotFoundException($"Strategy not found for {stategyEnum}");
            }
            return strategy.Template;
        }
    }
}
