namespace App.RenderEmail.RenderEmail
{
    public class EmailMessage
    {
        public EmailMessage(string message)
        {
            Message = GenerateMessage(message);
        }
        public string Message { get; private set; }

        private string GenerateMessage(string message) => $"<html>" +
                     $"<body>" +
                     $"{message}" +
                     $"</body>" +
                     $"</html>";

        public override string ToString()
        {
            return Message;
        }
    }
}
