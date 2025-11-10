using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentasETL.Aplication.Dtos.Source.Api
{
    public class DataCustumerUpdatedDto
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
