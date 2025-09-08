using App.EmailRender.Shared.Abstraction;
using Microsoft.AspNetCore.Components;

namespace App.RenderEmail.Repository
{
    public interface IEmailRenderComponent
    {
        /// <summary>
        /// Renders the specified Blazor component and returns its HTML output as a string.
        /// </summary>
        /// <remarks>This method is typically used for server-side rendering scenarios where the HTML
        /// representation of a Blazor component is required.</remarks>
        /// <typeparam name="TComponent">The type of the Blazor component to render. Must implement <see cref="IComponent"/>.</typeparam>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains the HTML
        /// output of the rendered component.</returns>
        Task<string> RenderComponent<TComponent>() where TComponent : IComponent;

        /// <summary>
        /// Renders the specified Blazor component as a string.
        /// </summary>
        /// <typeparam name="TComponent">The type of the Blazor component to render. Must implement <see cref="IComponent"/>.</typeparam>
        /// <param name="parameters">A dictionary containing the parameters to pass to the component. Keys represent parameter names, and values
        /// represent their corresponding values. Can be empty, but cannot be <see langword="null"/>.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous operation. The task result contains the
        /// rendered component as an HTML string.</returns>
        Task<string> RenderComponet<TComponent>(Dictionary<string, object?> parameters) where TComponent : IComponent;

        /// <summary>
        /// Renders the specified component as a string using the provided parameters.
        /// </summary>
        /// <typeparam name="TComponent">The type of the component to render. Must implement <see cref="IComponent"/>.</typeparam>
        /// <typeparam name="TParameters">The type of the parameters to pass to the component. Must implement <see cref="IEmailParameters"/>.</typeparam>
        /// <param name="parameters">The parameters used to configure the component rendering. Cannot be <see langword="null"/>.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains the rendered
        /// component as a string.</returns>
        Task<string> RenderComponent<TComponent, TParameters>(TParameters parameters) where TComponent : IComponent where TParameters : IEmailParameters;
    }
}
