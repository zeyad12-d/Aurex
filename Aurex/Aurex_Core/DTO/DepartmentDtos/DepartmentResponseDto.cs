using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Core.DTO.DepartmentDtos
{
    public record  DepartmentResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } =string.Empty;

    }
}
