using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Appointments
{
    public class UpdateAppointmentStateCommand : IRequest<bool>
    {
        public int AppointmentId { get; set; }
        public string NewStateName { get; set; }

        public UpdateAppointmentStateCommand(int appointmentId, string newStateName)
        {
            AppointmentId = appointmentId;
            NewStateName = newStateName;
        }
    }
}
