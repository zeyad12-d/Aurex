using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Core.Entites
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public ClientStatus Status { get; set; } = ClientStatus.Prospect;

        // Navigation property
        public ICollection<Deal> Deals { get; set; } = new List<Deal>();
    }
    public enum ClientStatus
    {
        Prospect,
        Active,
        Inactive,
        blocked
    }
}
