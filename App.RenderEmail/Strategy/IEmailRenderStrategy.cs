namespace App.RenderEmail.Strategy
{
    public interface IEmailRenderStrategy
    {
        T RenderStrategy<T, TEnum>(TEnum stategy, Dictionary<TEnum, T> strategyDictionar) where TEnum : Enum;
    }
}
