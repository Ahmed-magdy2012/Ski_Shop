using SKINET.Server.DTOS;
using SKINET.Server.Entities;

namespace SKINET.Server.NewFolder
{
    public  static class AddressMappingExtension
    {
        public static AddressDto ToDto(this Address address)
        {
            if (address == null) return null;

            return new AddressDto
            {
                Line1 = address.Line1,
                Line2 = address.Line2,
                City = address.City,
                Country = address.Country,
                PostalCode = address.PostalCode,
                State = address.State,



            };
        }
        public static Address ToEntity(this AddressDto addressdto)
        {
            if (addressdto == null) throw new ArgumentException(nameof(addressdto));

            return new Address
            {
                Line1 = addressdto.Line1,
                Line2 = addressdto.Line2,
                City = addressdto.City,
                Country = addressdto.Country,
                PostalCode = addressdto.PostalCode,
                State = addressdto.State,



            };
        }
        public static void UpdateFromDto(this Address address,AddressDto addressdto)
        {
            if (addressdto == null) throw new ArgumentException(nameof(addressdto));
            if (address == null) throw new ArgumentException(nameof(address));


            address.Line1 = addressdto.Line1;
            address.Line2 = addressdto.Line2;
            address.City = addressdto.City;
            address.Country = addressdto.Country;
            address.PostalCode = addressdto.PostalCode;
            address.State = addressdto.State;




        }
    }
}
