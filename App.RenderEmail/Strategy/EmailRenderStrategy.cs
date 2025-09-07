
using App.RenderEmail.RenderExceptons;

namespace App.RenderEmail.Strategy
{
    public class EmailRenderStrategy : IEmailRenderStrategy
    {
        private static EmailRenderStrategy _instance = null;
        private static object _instanceLock = new object();
        private EmailRenderStrategy() { }

        public static EmailRenderStrategy GetInstance()
        {
            if (_instance == null)
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new EmailRenderStrategy();
                    }
                }
            }
            return _instance;

        }

        public T RenderStrategy<T, TEnum>(TEnum stategyEnum, Dictionary<TEnum, T> strategyDictionary) where TEnum : Enum
        {
            if (strategyDictionary is null)
            {
                throw new StrategyDictionaryNullException("Issue with the strategy dictionary");
            }
            if (!strategyDictionary.TryGetValue(stategyEnum, out T? strategy))
            {
                throw new StrategyNotFoundException($"Strategy not found for {stategyEnum.ToString()}");
            }
            return strategy;

        }
    }
}
