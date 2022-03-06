using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static OpenSaludSecurity.Models.Constants;

namespace OpenSaludSecurity.Models
{
    public class Request
    {
        // user ID from AspNetUser table.
        public string? OwnerID { get; set; }
        public int RequestId { get; set; }
        public string? Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public RequestStatus Status { get; set; }
    }


}
