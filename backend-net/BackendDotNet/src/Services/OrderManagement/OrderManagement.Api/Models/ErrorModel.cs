namespace OrderManagement.Api.Models
{
    /// <summary>
    /// Model used for returning an error message.
    /// </summary>
    public class ErrorModel
    {
        public string Message { get; set; }

        public ErrorModel(string message)
        {
            Message = message;
        }
    }
}
