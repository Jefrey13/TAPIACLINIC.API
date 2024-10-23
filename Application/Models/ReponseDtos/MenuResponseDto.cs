using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ReponseDtos
{
    public class MenuResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }

        // Lista para los submenús
        public List<MenuResponseDto> Children { get; set; } = new List<MenuResponseDto>();
    }
}
