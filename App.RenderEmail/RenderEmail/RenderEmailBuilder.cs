using App.EmailRender.Shared.Abstraction;
using App.EmailRender.Shared.Parameters;
using App.EmailRender.Shared.Strategy;
using App.RenderEmail.RenderExceptons;
using System.Text.Json;

namespace App.RenderEmail.RenderEmail
{
    /// <summary>
    /// Provides a fluent API for constructing an email message by rendering its header, body, and footer using
    /// customizable templates and rendering strategies.
    /// </summary>
    /// <remarks>This class allows you to build an email message by setting its components (header, body, and
    /// footer) using specific templates and parameters. The rendering process is driven by an <see
    /// cref="IEmailRenderStrategy"/>, which determines how templates are selected and rendered.  After setting the
    /// desired components, call <see cref="Build"/> to generate the final <see cref="EmailMessage"/>.</remarks>
    public class RenderEmailBuilder
    {
        private string Header { get; set; }
        private string Body { get; set; }
        private string Footer { get; set; }
        public RenderEmailBuilder(IEmailRenderStrategy strategy, IEmailParametersType emailParametersType)
        {
            Header = string.Empty;
            Body = string.Empty;
            Footer = string.Empty;
            _strategy = strategy;
            _emailParametersType = emailParametersType;
        }
        private readonly IEmailRenderStrategy _strategy;

        private readonly IEmailParametersType _emailParametersType;

        /// <summary>
        /// Sets the header of the email by rendering a specified component using the provided template strategy and
        /// parameters.
        /// </summary>
        /// <remarks>The method renders the header component asynchronously based on the specified
        /// enumeration value, template strategy, and parameters. Ensure that the <paramref
        /// name="renderTemplateStrategy"/> contains a valid mapping for the provided <paramref
        /// name="headerEnum"/>.</remarks>
        /// <typeparam name="TEnum">The type of the enumeration used to identify the header component.</typeparam>
        /// <typeparam name="TParameters">The type of the parameters used for rendering the header component.</typeparam>
        /// <param name="headerEnum">An enumeration value that specifies the header component to render.</param>
        /// <param name="renderTemplateStrategy">A dictionary mapping enumeration values to email templates, used to determine the rendering strategy for the
        /// header component.</param>
        /// <param name="parameters">The parameters required to render the header component. This must be a reference type.</param>
        /// <returns>A <see cref="RenderEmailBuilder"/> instance with the header set, allowing for method chaining.</returns>
        public async Task<RenderEmailBuilder> SetHeader<TEnum, TParameters>(TEnum headerEnum, Dictionary<TEnum, EmailBuilderMetadata<IEmailTemplate, TParameters>> renderTemplateStrategy, JsonElement parameters)
            where TEnum : Enum where TParameters : IEmailParameters
        {
            Header = await RenderComponent(headerEnum, renderTemplateStrategy, parameters);
            return this;
        }
        /// <summary>
        /// Sets the body of the email by rendering the specified template with the provided parameters.
        /// </summary>
        /// <remarks>This method renders the email body asynchronously using the specified template and
        /// parameters.  The <paramref name="renderTemplateStrategy"/> dictionary must contain a mapping for the
        /// provided <paramref name="bodyEnum"/> value.</remarks>
        /// <typeparam name="TEnum">The type of the enumeration used to identify the email template.</typeparam>
        /// <typeparam name="TParameters">The type of the parameters used for rendering the template.</typeparam>
        /// <param name="bodyEnum">An enumeration value that identifies the template to render.</param>
        /// <param name="renderTemplateStrategy">A dictionary mapping enumeration values to their corresponding email templates.</param>
        /// <param name="parameters">The parameters to use when rendering the selected template. Must be a reference type.</param>
        /// <returns>A <see cref="RenderEmailBuilder"/> instance with the rendered body set, allowing for method chaining.</returns>
        public async Task<RenderEmailBuilder> SetBody<TEnum, TParameters>(TEnum bodyEnum, Dictionary<TEnum, EmailBuilderMetadata<IEmailTemplate, TParameters>> renderTemplateStrategy, JsonElement parameters)
            where TEnum : Enum where TParameters : IEmailParameters
        {
            Body = await RenderComponent(bodyEnum, renderTemplateStrategy, parameters);
            return this;
        }

        /// <summary>
        /// Sets the footer of the email by rendering a specified component using the provided template strategy and
        /// parameters.
        /// </summary>
        /// <remarks>This method renders the footer component asynchronously based on the specified
        /// template and parameters, and assigns it to the <c>Footer</c> property.</remarks>
        /// <typeparam name="TEnum">The type of the enumeration used to identify the footer template. Must be an <see cref="Enum"/>.</typeparam>
        /// <typeparam name="TParameters">The type of the parameters used for rendering the footer template. Must be a reference type.</typeparam>
        /// <param name="footerEnum">The enumeration value that identifies the footer template to render.</param>
        /// <param name="renderTemplateStrategy">A dictionary mapping enumeration values to their corresponding email templates. Used to determine the
        /// template to render.</param>
        /// <param name="parameters">The parameters required to render the footer template. Can include data specific to the template.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the current instance of <see
        /// cref="RenderEmailBuilder"/>, allowing for method chaining.</returns>
        public async Task<RenderEmailBuilder> SetFooter<TEnum, TParameters>(TEnum footerEnum, Dictionary<TEnum, EmailBuilderMetadata<IEmailTemplate, TParameters>> renderTemplateStrategy, JsonElement parameters)
            where TEnum : Enum where TParameters : IEmailParameters
        {
            Footer = await RenderComponent(footerEnum, renderTemplateStrategy, parameters);
            return this;
        }


        private Task<string> RenderComponent<TEnum, TParameters>(TEnum enumValue, Dictionary<TEnum, EmailBuilderMetadata<IEmailTemplate, TParameters>> renderTemplateStrategy, JsonElement parameters)
            where TEnum : Enum where TParameters : IEmailParameters
        {
            IEmailTemplate template = _strategy.RenderStrategy(enumValue, renderTemplateStrategy);
            Type parametersType = _emailParametersType.GetEmailParametersType(enumValue, renderTemplateStrategy);
            IEmailParameters parameter = Deserialization<IEmailParameters>(parameters, parametersType);
            return template.RenderTemplateAsync(parameter);
        }


        private TParameters Deserialization <TParameters>(JsonElement parameters, Type parameterType)
            where TParameters : IEmailParameters
        {
            TParameters? parameter = (TParameters?)JsonSerializer.Deserialize(parameters, parameterType);
            if (parameter is null)
            {
                throw new DeserializationNullException($"Parameters cannot be null : {typeof(TParameters)}");
            }
            return parameter;
        }
        /// <summary>
        /// Constructs an <see cref="EmailMessage"/> instance by combining the header, body, and footer.
        /// </summary>
        /// <remarks>The header, body, and footer are concatenated using the system's newline separator.
        /// Ensure that the header, body, and footer are properly set before calling this method.</remarks>
        /// <returns>An <see cref="EmailMessage"/> containing the combined content of the header, body, and footer.</returns>
        public EmailMessage Build()
        {
            return new EmailMessage(string.Join(Environment.NewLine, Header, Body, Footer));
        }
    }
}
