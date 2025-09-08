namespace App.EmailRender.Shared.Abstraction
{
    public interface IEmailTemplate
    {
        Task<string> RenderTemplateAsync<TParameters>(TParameters parameters) where TParameters : IEmailParameters;
    }
}
