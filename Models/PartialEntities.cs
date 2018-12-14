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
        public string Name { get; set; }
    }

    public partial class ClientMetaData
    {
        [DisplayName("Client Name")]
        public string Name { get; set; }
    }
}