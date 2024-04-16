using MeetManager.Model;
using System.ComponentModel.DataAnnotations;

namespace MeetManager.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        #region Private Methods
        private List<ValidationResult> Validate(object model)
        {
            List<ValidationResult> results = new();
            ValidationContext context = new(model);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }
        #endregion

        #region Entire Model

        [TestMethod]
        public void Meet_ExpectNoErrors()
        {
            Meet m = new Meet()
            {
                Name = "Test Meet",
                VenueId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                RegistrationDeadline = DateTime.Now.AddHours(-24),
                EntryFee = 100.00m,
                FeePerEvent = 15.00m
            };

            List<ValidationResult> results = Validate(m);

            Assert.IsTrue(results.Count == 0);
        }
        #endregion

        #region MeetName

        [TestMethod]
        public void MeetName_Empty_Expect1Error()
        {
            Meet m = new Meet()
            {
                Name = "",
                VenueId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                RegistrationDeadline = DateTime.Now.AddHours(-24),
                EntryFee = 100.00m,
                FeePerEvent = 15.00m
            };

            List<ValidationResult> results = Validate(m);

            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results[0].ErrorMessage == "Meet name is required.");
        }

        [TestMethod]
        public void MeetName_TooLong_Expect1Error()
        {
            Meet m = new Meet()
            {
                Name = new string('x', 101),
                VenueId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                RegistrationDeadline = DateTime.Now.AddHours(-24),
                EntryFee = 100.00m,
                FeePerEvent = 15.00m
            };

            List<ValidationResult> results = Validate(m);

            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results[0].ErrorMessage == "Meet name cannot exceed 100 characters.");
        }
        #endregion

        #region StartDate

        /// <summary>
        /// Check to make sure that a StartDate has been provided when creating an instance
        /// of the model.
        /// </summary>
        [TestMethod]
        public void StartDate_Empty_Expect1Error()
        {
            Meet m = new Meet()
            {
                Name = new string('x', 99),
                StartDate = null,
                VenueId = 1,
                EndDate = DateTime.Now.AddDays(1),
                RegistrationDeadline = DateTime.Now.AddHours(-24),
                EntryFee = 100.00m,
                FeePerEvent = 15.00m
            };

            List<ValidationResult> results = Validate(m);

            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results[0].ErrorMessage == "Start date is required.");
        }

        #endregion

        #region EndDate

        [TestMethod]
        public void EndDate_Empty_Expect1Error()
        {
            Meet m = new Meet()
            {
                Name = new string('x', 99),
                StartDate = DateTime.Now,
                VenueId = 1,
                EndDate = null,
                RegistrationDeadline = DateTime.Now.AddHours(-24),
                EntryFee = 100.00m,
                FeePerEvent = 15.00m
            };

            List<ValidationResult> results = Validate(m);

            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results[0].ErrorMessage == "End date is required.");
        }

        [TestMethod]
        public void EndDate_NotGreaterThanStartDate_Expect1Error()
        {
            Meet m = new Meet()
            {
                Name = new string('x', 99),
                VenueId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                RegistrationDeadline = DateTime.Now.AddHours(-24),
                EntryFee = 100.00m,
                FeePerEvent = 15.00m
            };

            List<ValidationResult> results = Validate(m);

            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results[0].ErrorMessage == "End date must be greater than the start date.");
        }
        #endregion

        #region RegistrationDeadline

        [TestMethod]
        public void RegistrationDeadline_Empty_Expect1Error()
        {
            Meet m = new Meet()
            {
                Name = new string('x', 99),
                StartDate = DateTime.Now,
                VenueId = 1,
                EndDate = DateTime.Now.AddDays(1),
                RegistrationDeadline = null,
                EntryFee = 100.00m,
                FeePerEvent = 15.00m
            };

            List<ValidationResult> results = Validate(m);

            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results[0].ErrorMessage == "Registration deadline date is required.");
        }

        [TestMethod]
        public void RegistrationDeadline_Not24HoursBeforeStartDate_Expect1Error()
        {
            Meet m = new Meet()
            {
                Name = new string('x', 99),
                VenueId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                RegistrationDeadline = DateTime.Now.AddHours(-23),
                EntryFee = 100.00m,
                FeePerEvent = 15.00m
            };

            List<ValidationResult> results = Validate(m);

            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results[0].ErrorMessage == "Registration Deadline must be at least 24 hours before start date.");
        }
        #endregion

        #region EntryFee

        [TestMethod]
        public void EntryFee_Not2DecimalPlaces_Expect1Error()
        {
            Meet m = new Meet()
            {
                Name = "Test Meet",
                VenueId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                RegistrationDeadline = DateTime.Now.AddHours(-24),
                EntryFee = 100.000m,
                FeePerEvent = 15.00m
            };

            List<ValidationResult> results = Validate(m);

            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results[0].ErrorMessage == "Entry fee cannot have more than 2 decimal places.");
        }

        #endregion

        #region FeePerEvent

        [TestMethod]
        public void FeePerEvent_Not2DecimalPlaces_Expect1Error()
        {
            Meet m = new Meet()
            {
                Name = "Test Meet",
                VenueId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                RegistrationDeadline = DateTime.Now.AddHours(-24),
                EntryFee = 100.00m,
                FeePerEvent = 15.000m
            };

            List<ValidationResult> results = Validate(m);

            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results[0].ErrorMessage == "Fee per event cannot have more than 2 decimal places.");
        }

        #endregion

        #region Venue

        [TestMethod]
        public void Venue_NotEmpty_Expect1Error()
        {
            Meet m = new Meet()
            {
                Name = "Test Meet",
                VenueId = 0,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                RegistrationDeadline = DateTime.Now.AddHours(-24),
                EntryFee = 100.00m,
                FeePerEvent = 15.00m
            };

            List<ValidationResult> results = Validate(m);

            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results[0].ErrorMessage == "Venue is required.");
        }

        #endregion
    }
}