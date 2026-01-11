namespace SKINET.Server.Entities
{
    public class Address:BaseEntity
    {
        public required String Line1  { get; set; }
        public String? Line2 { get; set; }
        public required String City { get; set; }

        public required String State { get; set; }

        public required String PostalCode { get; set; }
        public required String Country { get; set; }


    }
}
