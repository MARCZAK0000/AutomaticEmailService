using App.EmailRender.Shared.Abstraction;

namespace App.EmailRender.Shared.Strategy
{
    /// <summary>
    /// Defines a strategy for rendering email templates based on a specified enumeration value.
    /// </summary>
    /// <remarks>This interface allows for the selection and rendering of email templates using a strategy
    /// pattern. Implementations of this interface should provide the logic to map an enumeration value to the
    /// corresponding email template.</remarks>
    public interface IEmailRenderStrategy
    {
        /// <summary>
        /// Renders an email template based on the specified strategy and metadata.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enumeration used to identify the strategy. Must be an enumeration type.</typeparam>
        /// <typeparam name="TEmailParameters">The type of the email parameters used by the email template. Must implement <see cref="IEmailParameters"/>.</typeparam>
        /// <param name="stategyEnum">The enumeration value representing the strategy to use for rendering the email template.</param>
        /// <param name="strategyDictionary">A dictionary mapping enumeration values to their corresponding email template metadata. The metadata
        /// includes the template and its associated parameters.</param>
        /// <returns>The rendered email template of type <see cref="IEmailTemplate"/> corresponding to the specified strategy.</returns>
        IEmailTemplate RenderStrategy<TEnum, TEmailParameters>(TEnum stategyEnum, Dictionary<TEnum, EmailBuilderMetadata<IEmailTemplate, TEmailParameters>> strategyDictionary) 
            where TEnum : Enum where TEmailParameters : IEmailParameters ;


        
    }
}
