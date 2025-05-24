using System;

namespace Common.Entities
{
    /// <summary>
    /// Represents a chat message in the hotel booking system.
    /// </summary>
    public class ChatMessage
    {
        /// <summary>
        /// Gets or sets the unique identifier for the message.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the message sender.
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// Gets or sets the message content.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the session identifier for grouping related messages.
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the message was sent.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the message is from an admin.
        /// </summary>
        public bool IsFromAdmin { get; set; }
    }
}