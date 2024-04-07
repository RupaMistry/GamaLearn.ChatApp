using System.Text.Json;

namespace GamaLearn.ChatApp.Domain.Chat
{
    /// <summary>
    /// Represents the current chat recipient in session.
    /// </summary>
    public class ChatRecipient
    {
        public string Name { get; set; }

        public bool IsChatRoom { get; set; }

        /// <summary>
        /// Returns JSON serialized value
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this); 
        }
    }
}
