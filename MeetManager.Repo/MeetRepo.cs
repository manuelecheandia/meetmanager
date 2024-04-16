using DAL;
using MeetManager.Model;
using MeetManager.Types;
using System.Data;

namespace MeetManager.Repo
{

    public class MeetRepo
    {
        #region Fields

        private readonly DataAccess db = new();

        #endregion

        #region Public Methods

        public Meet GetMeet(int id)
        {
            List<Parm> parms = new List<Parm>
                {
                    new Parm("@MeetId",SqlDbType.Int, id)
                };

            DataTable dt = db.Execute("spGetMeet_SC", parms);
            Meet m = PopulateMeetDetails(dt.Rows[0]);

            dt = db.Execute("spGetMeetEvents_SC", parms);
            m.Events = dt.AsEnumerable().Select(row => new MeetEvent 
            {
                Id = (int)row["Id"],
                MeetId = (int)row["MeetId"],
                EventId = (int)row["EventId"],
                StartTime = row["StartTime"] != DBNull.Value ? (DateTime?)row["StartTime"] : null
            }).ToList();

            return m;
        }

        public List<MeetListDTO> GetMeetList()
        {
            DataTable dt = db.Execute("spGetMeets_SC");

            return dt.AsEnumerable().Select(row => new MeetListDTO((int)row["id"], row["MeetName"].ToString()!)).ToList();


            //List<MeetListDTO> meets = new ();
            //foreach (DataRow row in dt.Rows)
            //{
            //    meets.Add(new MeetListDTO((int)row["id"], row["MeetName"].ToString()!));
            //}

            //return meets;

        }
        public Meet UpdateMeet(Meet m)
        {
            List<Parm> parms = new List<Parm>
            {
                new Parm("@RecordVersion", SqlDbType.Timestamp, m.RecordVersion, 0, ParameterDirection.InputOutput),
                new Parm("@MeetId", SqlDbType.Int,m.Id),
                new Parm("@MeetName", SqlDbType.NVarChar,m.Name, 100),
                new Parm("@StartDate", SqlDbType.DateTime2, m.StartDate),
                new Parm("@EndDate", SqlDbType.DateTime2, m.EndDate),
                new Parm("@RegistrationDeadline", SqlDbType.DateTime2, m.RegistrationDeadline),
                new Parm("@EntryFee", SqlDbType.Money, m.EntryFee),
                new Parm("@FeePerEvent", SqlDbType.Money, m.FeePerEvent),
                new Parm("@Information", SqlDbType.NText, m.Information),
                new Parm("@VenueId", SqlDbType.Int, m.VenueId),
                new Parm("@MeetType", SqlDbType.Int, m.MeetType),
                new Parm("@MeetEvents", SqlDbType.Structured, CreateMeetEventsDT(m.Events))
            };

            if (db.ExcecuteNonQuery("spUpdateMeet_SC", parms) > 0)
                m.RecordVersion = (byte[]?)parms.FirstOrDefault(p => p.Name == "@RecordVersion")!.Value;
            else
                throw new DataException("There was an issue updating the record in the database");

            return m;
        }

        public Meet AddMeet(Meet m)
        {

            List<Parm> parms = new()
            {
                new Parm("@MeetId",SqlDbType.Int, m.Id,0, ParameterDirection.Output),
                new Parm("@RecordVersion",SqlDbType.Timestamp, m.RecordVersion,0, ParameterDirection.Output),
                new Parm("@MeetName", SqlDbType.NVarChar,m.Name, 100),
                new Parm("@StartDate", SqlDbType.DateTime2, m.StartDate),
                new Parm("@EndDate", SqlDbType.DateTime2, m.EndDate),
                new Parm("@RegistrationDeadline", SqlDbType.DateTime2, m.RegistrationDeadline),
                new Parm("@EntryFee", SqlDbType.Money, m.EntryFee),
                new Parm("@FeePerEvent", SqlDbType.Money, m.FeePerEvent),
                new Parm("@Information", SqlDbType.NText, m.Information),
                new Parm("@VenueId", SqlDbType.Int, m.VenueId),
                new Parm("@MeetType", SqlDbType.Int, m.MeetType),
                new Parm("@MeetEvents", SqlDbType.Structured, CreateMeetEventsDT(m.Events))
            };

            if (db.ExcecuteNonQuery("spInsertMeet_SC", parms) > 0)
            {
                //good
                m.Id = (int?)parms.FirstOrDefault(p => p.Name == "@MeetId")!.Value ?? 0;
                m.RecordVersion = (byte[]?)parms.FirstOrDefault(p => p.Name == "@RecordVersion")!.Value;

            }

            else
                        throw new DataException("There was an issue adding the record to the database.");

            return m;
        }

        public bool DeleteMeet(int id)
        {
            List<Parm> parms = new()
            {
                new Parm("@MeetId",SqlDbType.Int, id)
            };

            int retVal = db.ExcecuteNonQuery("spDeleteMeet_SC", parms);

            return retVal > 0;
        }

        public int CountVenueMeetsInDateRange(MeetDateValidInputDTO input)
        {
            List<Parm> parms = new()
            {
                new Parm("@StartDate", SqlDbType.DateTime2, input.StartDate),
                new Parm("@VenueId", SqlDbType.Int, input.VenueId),
                new Parm("@MeetId", SqlDbType.Int, input.MeetId)
            };

            string cmdText = @"
                            SELECT 
				                COUNT(*) 
			                FROM 
				                Meet 
			                WHERE 
				                VenueId = @VenueId 
				                AND EndDate < @StartDate
				                AND @StartDate < DATEADD(DAY,30,EndDate)
				                AND Id <> @MeetId";

            return Convert.ToInt32(db.ExecuteScalar(cmdText,parms,CommandType.Text));
        }

        public int CountProvinceMeetsPerYear(MeetDateValidInputDTO input)
        {
            List<Parm> parms = new()
            {
                new Parm("@StartDate", SqlDbType.DateTime2, input.StartDate),
                new Parm("@VenueId", SqlDbType.Int, input.VenueId),
                new Parm("@MeetId", SqlDbType.Int, input.MeetId)
            };

            string cmdText = @"SELECT
                  COUNT(*)
                FROM
                  Meet
                WHERE
                  VenueId IN (
                    SELECT 
                      Id
                    FROM
                      Venue
                    WHERE
                      ProvinceId = (
                        SELECT  
                          ProvinceId
                        FROM
                          Venue
                        WHERE
                          Id = @VenueId)
                  )
                  AND DATEPART(year, StartDate) = (SELECT DATEPART(year, @StartDate))
                  AND Id <> @MeetId";

            return Convert.ToInt32(db.ExecuteScalar(cmdText, parms, CommandType.Text));
        }



        #endregion
        #region Private Methods
        private Meet PopulateMeetDetails(DataRow row)
        {
            return new()
            {
                Id = (int)row["Id"],
                Name = row["Name"].ToString(),
                StartDate = (DateTime)row["StartDate"],
                EndDate = (DateTime)row["EndDate"],
                RegistrationDeadline = (DateTime)row["RegistrationDeadline"],
                EntryFee = (decimal)row["EntryFee"],
                FeePerEvent= (decimal)row["FeePerEvent"],
                Information = row["Information"].ToString(),
                VenueId = (int)row["VenueId"],
                MeetType = (MeetType)row["MeetType"],
                RecordVersion = (byte[])row["RecordVersion"]

            };
        }

        private DataTable CreateMeetEventsDT(List<MeetEvent> meetEvents)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id",typeof(int));
            dt.Columns.Add("MeetId", typeof(int));
            dt.Columns.Add("EventId", typeof(int));
            dt.Columns.Add("StartTIme", typeof(DateTime));

            foreach (MeetEvent meetEvent in meetEvents)
            {
                dt.Rows.Add(
                       meetEvent.Id,
                       meetEvent.MeetId,
                       meetEvent.EventId,
                       meetEvent.StartTime.HasValue ? meetEvent.StartTime : DBNull.Value);
            }

            return dt;
        }
        #endregion




    }
}