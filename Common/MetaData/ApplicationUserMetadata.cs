using System.ComponentModel;

namespace Lapi4ka.Common.MetaData
{
    public class ApplicationUserMetadata
    {
        [DisplayName("Емайл")]
        public string Email { get; set; }

        [DisplayName("Телефонен номер")]
        public string PhoneNumber { get; set; }

        [DisplayName("Потребителско име")]
        public string UserName { get; set; }
    }
}
