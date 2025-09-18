using App.EmailRender.Shared.Abstraction;

namespace App.EmailRender.Shared.Strategy
{
    public class EmailBuilderMetadata<TEmailTemplate, TEmailParameters>
        where TEmailParameters : IEmailParameters
        where TEmailTemplate : IEmailTemplate
    {
        public EmailBuilderMetadata(TEmailTemplate template, TEmailParameters parameters)
        {
            Template = template;
            Parameters = parameters;
        }

        public TEmailTemplate Template { get; set; }
        public TEmailParameters Parameters { get; set; }
    }
}
