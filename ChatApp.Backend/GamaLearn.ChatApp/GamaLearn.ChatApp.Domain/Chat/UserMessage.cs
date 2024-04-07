using GamaLearn.ChatApp.Domain.Core;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GamaLearn.ChatApp.Domain.Chat
{
    /// <summary>
    /// Represents the User message details
    /// </summary>
    public class UserMessage : Entity
    {
        [Required]
        public string Sender { get; set; }

        public string Recipient { get; set; }

        public string ChatRoomName { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        [Bindable(false)]
        public string MessageDate => CreatedDate.ToString("dd MMM hh:mm tt");

        public bool IsRead { get; set; }

        public bool IsDelivered { get; set; }
    }
}