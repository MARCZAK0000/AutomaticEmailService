namespace App.EmailBuilder.Options
{
    public class EmailSenderOptions
    {
        public string EmailName { get; set; }
        public string Password { get; set; }
        public string SmptHost { get; set; }
        public int Port { get; set; }

        public override string ToString()
        {
            return $"EmailSenderOptions {{ EmailName = {EmailName}, Password = *****, SmptHost = {SmptHost}, Port = {Port} }}";
        }
    }
}
