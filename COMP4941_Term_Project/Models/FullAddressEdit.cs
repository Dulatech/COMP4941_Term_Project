using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COMP4941_Term_Project.Models
{
    public class FullAddressEdit
    {
        public Guid faID { get; set; }
        [DisplayName("Room No")]
        public string RoomNo { get; set; }
        public string POBox { get; set; }
        public string Unit { get; set; }

        [Range(1, 200)]
        public string Floor { get; set; }
        public string Wing { get; set; }
        public string Building { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }

        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        public string Cell { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }

        public FullAddress UpdateFullAddress(FullAddress address)
        {
            System.Diagnostics.Debug.WriteLine("Address Before: " + address.Street + " " + address.City);
            address.RoomNo = this.RoomNo;
            address.POBox = this.POBox;
            address.Unit = this.Unit;
            address.Floor = this.Floor;
            address.Wing = this.Wing;
            address.Building = this.Building;
            address.Street = this.Street;
            address.City = this.City;
            address.Province = this.Province;
            address.Country = this.Country;
            address.PostalCode = this.PostalCode;
            address.Cell = this.Cell;
            address.Phone = this.Phone;
            address.Fax = this.Fax;
            address.Email = this.Email;
            System.Diagnostics.Debug.WriteLine("Address After: " + address.Street + " " + address.City);

            return address;
        }
    }
}