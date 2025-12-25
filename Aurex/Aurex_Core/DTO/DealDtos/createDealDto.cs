using Aurex_Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Core.DTO.DealDtos
{
    public record createDealDto
    {
        public decimal Amount { get; init; }
        public Currency Currency { get; init; }
        public DateTime Starttime { get; init; }
        public DealStatus Status { get; init; }
        public DateTime Endtime { get; init; }
        public int ClientId { get; init; }
        public int EmployeeId { get; init; }
        public int? ProjectId { get; init; }
    }
}
