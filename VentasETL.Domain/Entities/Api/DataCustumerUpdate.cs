using System.ComponentModel.DataAnnotations.Schema;

namespace VentasETL.Domain.Entities.Api
{
    [Table("DataCustumerUpdate", Schema = "dbo")]
    public class DataCustumerUpdate
    {
            public string FirstName { get; set; } = string.Empty;

            public string LastName { get; set; } = string.Empty;

            public string PhoneNumber { get; set; } = string.Empty;

            public string EmailAddress { get; set; } = string.Empty;

            public string AddressType { get; set; } = string.Empty;

            public string AddressLine1 { get; set; } = string.Empty;

            public string City { get; set; } = string.Empty;

            public string StateProvinceName { get; set; } = string.Empty;

            public string PostalCode { get; set; } = string.Empty;

            public string CountryRegionName { get; set; } = string.Empty;
        

    }
}
