using App.EmailRender.Shared.Abstraction;

namespace App.RenderEmail.Strategy
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
        /// Renders an email template based on the specified strategy.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enumeration used to identify the strategy. Must be an enumeration type.</typeparam>
        /// <param name="stategyEnum">The enumeration value representing the strategy to use for rendering the email template.</param>
        /// <param name="strategyDictionary">A dictionary mapping enumeration values to their corresponding email templates. The dictionary must contain
        /// an entry for the specified <paramref name="stategyEnum"/>.</param>
        /// <returns>The email template associated with the specified <paramref name="stategyEnum"/>.</returns>
        IEmailTemplate RenderStrategy<TEnum>(TEnum stategyEnum, Dictionary<TEnum, IEmailTemplate> strategyDictionary) where TEnum : Enum;
    }
}
