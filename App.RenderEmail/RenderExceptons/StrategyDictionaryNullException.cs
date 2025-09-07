namespace App.RenderEmail.RenderExceptons
{
    public class StrategyDictionaryNullException : Exception
    {
        public StrategyDictionaryNullException()
        {
        }

        public StrategyDictionaryNullException(string? message) : base(message)
        {
        }
    }
}
