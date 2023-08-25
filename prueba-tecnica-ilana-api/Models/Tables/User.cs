using System;
using System.Collections.Generic;

namespace prueba_tecnica_ilana_api.Models.Tables
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Pass { get; set; } = null!;
    }
}
