namespace Common.Messages
{
    /// <summary>
    /// Represents a generic response message sent from the server to the client.
    /// </summary>
    /// <typeparam name="T">The type of the data included in the response.</typeparam>
    public class ResponseMessage<T>
    {
        /// <summary>
        /// Gets or sets the status code of the response (e.g., 200 for success, 404 for not found, etc.).
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the data payload of the response.
        /// </summary>
        public T Data { get; set; }
    }
}
