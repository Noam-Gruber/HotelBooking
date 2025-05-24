using Common.Entities;

namespace Common.Messages
{
    /// <summary>
    /// Represents a request message sent from the client to the server.
    /// </summary>
    public class RequestMessage
    {
        /// <summary>
        /// Gets or sets the command to be executed by the server (e.g., "get", "create", "update", "delete").
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the booking, used for operations that require an ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the booking object, used for create or update operations.
        /// </summary>
        public Booking Booking { get; set; }

        /// <summary>
        /// Gets or sets the chat message object, used for chat-related operations.
        /// </summary>
        public ChatMessage ChatMessage { get; set; }

        /// <summary>
        /// Gets or sets the session identifier, used to associate the request with a specific session.
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the number of minutes used for operations such as deleting old chat messages.
        /// </summary>
        public int Minutes { get; set; }
    }
}
