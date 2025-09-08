namespace App.RenderEmail.RenderEmail
{
    public class EmailMessage(string message)
    {
        public string Message { get; set; } = message;

        public override string ToString()
        {
            return Message;
        }
    }
}
