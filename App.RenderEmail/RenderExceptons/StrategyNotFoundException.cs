namespace App.RenderEmail.RenderExceptons
{
    public class StrategyNotFoundException : Exception
    {
        public StrategyNotFoundException()
        {
        }

        public StrategyNotFoundException(string? message) : base(message)
        {
        }
    }
}
