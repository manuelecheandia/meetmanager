using DAL;
using MeetManager.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetManager.Repo
{
    public class ListsRepo
    {
        #region Fields

        private readonly DataAccess db = new();


        #endregion

        public List<VenueListDTO> GetVenueList()
        {
            DataTable dt = db.Execute("spGetVenues_SC");
            return dt.AsEnumerable().Select(row => new VenueListDTO((int)row["Id"], row["VenueName"].ToString()!)).ToList();
        }

        public List<EventListDTO> GetEventList()
        {
            DataTable dt = db.Execute("spGetEvents_SC");

            return dt.AsEnumerable().Select(row =>
                new EventListDTO
                {
                    EventId = (int)row["Id"],
                    Name = row["EventName"].ToString(),
                    Discipline = row["Discipline"].ToString(),
                    Division = row["Division"].ToString(),
                    Gender = row["Gender"].ToString()
                }).ToList();
        }
    }
}
