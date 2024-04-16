using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetManager.Model
{
    public record MeetDateValidInputDTO(DateTime StartDate, int VenueId, int MeetId);
}
