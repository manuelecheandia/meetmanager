using MeetManager.Model;
using MeetManager.Repo;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MeetManager.Service
{
    public class MeetService
    {
        #region Fields

        private readonly MeetRepo repo = new MeetRepo();

        #endregion

        #region Public Methods
        
        public Meet GetMeet(int id)
        {
            return repo.GetMeet(id);
        }

        public List<MeetListDTO> GetMeetList()
        {
            return repo.GetMeetList();
        }

        public Meet AddMeet(Meet m)
        {
            if(Validate(m))
            return repo.AddMeet(m);

            return m;
        }


        public Meet UpdateMeet(Meet m)
        {
            if(Validate(m))
            return repo.UpdateMeet(m);


            return m;
        }

        public bool DeleteMeet(int id)
        {
            return repo.DeleteMeet(id);
        }
        #endregion

        #region Private Methods

        private bool Validate(Meet m)
        {
            // Validate Model

            List<ValidationResult> results = new();
            ValidationContext context = new(m);
            Validator.TryValidateObject(m, context, results, true);

            foreach(ValidationResult e in results) 
            {
                m.AddError(new ValidationError(e.ErrorMessage!));
            }

            if(m.Events.Count == 0)
            {
                m.AddError(new ValidationError("A meet must have at least 1 event."));
            }

            // Validate Business Rules
            MeetDateValidInputDTO input = new((DateTime)m.StartDate!, m.VenueId, m.Id);

            if (!IsStartDateWithinRange(input))
                m.AddError(new ValidationError("At least 1 meet at that venue has an end date less than 30 days" +
                    " from the start date of this meet."));

            if (!CanMeetBeHeldInProvince(input))
                m.AddError(new ValidationError("The venue's province already has 3 meets in the year"));

            return m.Errors.Count == 0;

            
        }

        private bool IsStartDateWithinRange(MeetDateValidInputDTO input)
        {
            return repo.CountVenueMeetsInDateRange(input) == 0;

        }

        private bool CanMeetBeHeldInProvince(MeetDateValidInputDTO input)
        {
            return repo.CountProvinceMeetsPerYear(input) < 3;
        }

        #endregion
    }
}