using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CybageTest.Models
{
    public class CustomerList
    {
        [DisplayName("Id")]
        public string id { get; set; }
        public string salutation { get; set; }
        public string initials { get; set; }
        [DisplayName("First Name")]
        public string firstname { get; set; }
        public string firstname_ascii { get; set; }
        [DisplayName("Gender")]
        public string gender { get; set; }
        public string firstname_country_rank { get; set; }
        public string firstname_country_frequency { get; set; }
        [DisplayName("Last Name")]
        public string lastname { get; set; }
        public string lastname_ascii { get; set; }
        public string lastname_country_rank { get; set; }
        public string lastname_country_frequency { get; set; }
        [DisplayName("Email")]
        [Required]
        public string email { get; set; }
        public string password { get; set; }
        [DisplayName("Country Code")]
        public string country_code { get; set; }
        public string country_code_alpha { get; set; }
        public string country_name { get; set; }
        public string primary_language_code { get; set; }
        public string primary_language { get; set; }
        [DisplayName("Balance")]
        public double balance { get; set; }
        [DisplayName("Phone Number")]
        public string phone_Number { get; set; }
        public string currency { get; set; }
        public string partitionKey { get; set; }
        public string rowKey { get; set; }
        public DateTime timestamp { get; set; }
        public ETag eTag { get; set; }
    }
    public class ETag
    {

    }
}
