using Aurex_Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Core.DTO.DealDtos
{
    public record  UpdateDealDto
    {
        public int Id { get; set; }
        public decimal? Amount { get; set; }
        public Currency? Currency { get; set; }
        public DateTime? EndTime { get; set; }
        public DealStatus? Status { get; set; }

        public int? ClientId { get; set; }
        public int? EmployeeId { get; set; }
        public int? ProjectId { get; set; }

    }
}
