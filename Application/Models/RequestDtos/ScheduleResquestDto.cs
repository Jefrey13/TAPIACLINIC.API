using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.RequestDtos
{
    public class ScheduleResquestDto
    {
        /// <summary>
        /// ID of the specialty associated with the Schedule.
        /// </summary>
        public int SpecialtyId { get; set; }

        /// <summary>
        /// Day of the week for the Schedule.
        /// </summary>
        public string DayOfWeek { get; set; }

        /// <summary>
        /// Start time of the Schedule.
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// End time of the Schedule.
        /// </summary>
        public string EndTime { get; set; }
    }
}