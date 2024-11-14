using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global

namespace CHPanel.Models.Dtos {
    public class AccountInfo {
        public string @object { get; set; } = string.Empty;
        public Attributes? attributes { get; set; }
        
        public class Attributes {
            public int id { get; set; }
            public bool admin { get; set; }
            public string username { get; set; } = string.Empty;
            public string email { get; set; } = string.Empty;
            public string first_name { get; set; } = string.Empty;
            public string last_name { get; set; } = string.Empty;
            public string language { get; set; } = string.Empty;
        }
    }
}
