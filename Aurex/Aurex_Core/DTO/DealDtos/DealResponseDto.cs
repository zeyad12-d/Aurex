using Aurex_Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Core.DTO.DealDtos
{
    public record DealResponseDto
    {
        // we are using record type for immutability and simplicity 
        public int Id { get; init; }
        public decimal Amount { get; init; }

        public Currency Currency { get; init; }

        public DateTime Endtime { get; init; }

        public DealStatus Status { get; init; }

        public string ClientName { get; init; } = string.Empty;

        public string EmployeeName { get; init; } = string.Empty;

        public string ProjectTeamLeaderName { get; init; } = string.Empty;


    }
}
