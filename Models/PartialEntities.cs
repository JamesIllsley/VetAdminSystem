using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VetAdminSystem.Models
{
    [MetadataType(typeof(PatientMetaData))]
    public partial class Patient
    {

    }

    [MetadataType(typeof(ClientMetaData))]
    public partial class Client
    {

    }
    
    public class PatientMetaData
    {
        [DisplayName("Patient Name")]
        [Required(ErrorMessage = "Name is a required field. If Unknown, please type Unknown")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Breed is a required field. If Unknown, please type Unknown")]
        public string Breed { get; set; }
        [Required(ErrorMessage = "Age is a required field. If Unknown, please enter 0")]
        public Nullable<int> Age { get; set; }
    }

    public partial class ClientMetaData
    {
        [DisplayName("Client Name")]
        public string Name { get; set; }
    }
}