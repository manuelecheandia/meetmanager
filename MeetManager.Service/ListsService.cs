using MeetManager.Model;
using MeetManager.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MeetManager.Service
{
    public class ListsService
    {
        #region Private Fields

        private readonly ListsRepo repo = new();

        #endregion

        #region Public Methods

        public List<VenueListDTO> GetVenueList()
        {
            return repo.GetVenueList();
        }

        public List<EventListDTO> GetEventList()
        {
            return repo.GetEventList();
        }

        #endregion
    }
}
