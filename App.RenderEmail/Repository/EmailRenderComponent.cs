using App.EmailRender.Shared.Abstraction;
using App.RenderEmail.RenderExceptons;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.HtmlRendering;

namespace App.RenderEmail.Repository
{
    /// <summary>
    /// Provides functionality to render Blazor components into HTML strings for use in email templates.
    /// </summary>
    /// <remarks>This class is designed to render Blazor components into HTML strings, which can be embedded
    /// in email content. It supports rendering components with or without parameters. The rendering process is
    /// asynchronous and leverages the underlying <see cref="HtmlRenderer"/> to generate the HTML output.</remarks>
    public class EmailRenderComponent : IEmailRenderComponent
    {
        private readonly HtmlRenderer _htmlRenderer;

        public EmailRenderComponent(HtmlRenderer htmlRenderer)
        {
            _htmlRenderer = htmlRenderer;
        }
        /// <summary>
        /// Renders the specified component and returns its HTML output as a string.
        /// </summary>
        /// <remarks>This method renders the component with no parameters. To render a component with
        /// parameters, use an overload that accepts a <see cref="ParameterView"/>.</remarks>
        /// <typeparam name="TComponent">The type of the component to render. Must implement <see cref="IComponent"/>.</typeparam>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains the HTML
        /// output of the rendered component.</returns>
        public async Task<string> RenderComponent<TComponent>() where TComponent : IComponent
            => await RenderedComponent<TComponent>(ParameterView.Empty);

        /// <summary>
        /// Renders the specified Blazor component as a string using the provided parameters.
        /// </summary>
        /// <remarks>This method dynamically extracts the properties of the <typeparamref
        /// name="TParameters"/> object and passes them as parameters to the specified Blazor component. Ensure that the
        /// parameter object is properly populated before calling this method.</remarks>
        /// <typeparam name="TComponent">The type of the Blazor component to render. Must implement <see cref="IComponent"/>.</typeparam>
        /// <typeparam name="TParameters">The type of the parameters to pass to the component. Must implement <see cref="IEmailParameters"/>.</typeparam>
        /// <param name="parameters">An object containing the parameters to pass to the component. Each property of the object is treated as a
        /// parameter.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains the rendered
        /// component as a string.</returns>
        /// <exception cref="RenderComponentParametersNull">Thrown if <paramref name="parameters"/> is null or contains no properties.</exception>
        public async Task<string> RenderComponent<TComponent, TParameters>(TParameters parameters)
            where TComponent : IComponent
            where TParameters : IEmailParameters
        {
            var parameterView = typeof(TParameters)
                .GetProperties()
                .ToDictionary(pr => pr.Name, pr => pr.GetValue(parameters));
            if (parameterView is null || parameterView.Count == 0)
            {
                throw new RenderComponentParametersNull($"Parameters cannot be null or empty : {nameof(TParameters)}");
            }
            return await RenderedComponent<TComponent>(ParameterView.FromDictionary(parameterView));
        }

        /// <summary>
        /// Renders the specified Blazor component and returns its HTML output as a string.
        /// </summary>
        /// <typeparam name="TComponent">The type of the Blazor component to render. Must implement <see cref="IComponent"/>.</typeparam>
        /// <param name="parameters">A dictionary containing the parameters to pass to the component. The keys represent the parameter names, 
        /// and the values represent the corresponding parameter values. Cannot be null or empty.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous operation. The task result contains the 
        /// rendered HTML output of the component as a string.</returns>
        /// <exception cref="RenderComponentParametersNull">Thrown if <paramref name="parameters"/> is null or empty.</exception>
        public async Task<string> RenderComponet<TComponent>(Dictionary<string, object?> parameters) where TComponent : IComponent
        {
            if(parameters is null || parameters.Count == 0)
            {
                throw new RenderComponentParametersNull("Parameters cannot be null or empty");
            }
            return await RenderedComponent<TComponent>(ParameterView.FromDictionary(parameters));
        }

        private Task<string> RenderedComponent<T>(ParameterView parameters) where T : IComponent
        {
            return _htmlRenderer.Dispatcher.InvokeAsync(async () =>
            {
                HtmlRootComponent component = await _htmlRenderer.RenderComponentAsync<T>(parameters);
                return component.ToHtmlString();
            });
        }
    }
}
