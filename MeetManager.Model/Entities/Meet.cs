using MeetManager.Types;
using System;
using System.ComponentModel.DataAnnotations;

namespace MeetManager.Model
{
    public class Meet : BaseEntity
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Meet name is required.")]
        [StringLength(100, ErrorMessage = "Meet name cannot exceed 100 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        //[Range(typeof(DateTime), "01/01/1900", "01/01/2100", ErrorMessage = "Start date is required.")] // could do it this way and NOT make it nullable
        public DateTime? StartDate { get; set; } // Made nullable in order to use the Required DataAnnotation

        [Required(ErrorMessage = "End date is required.")]
        [EndDate]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Registration deadline date is required.")]
        [RegistrationDeadline]
        public DateTime? RegistrationDeadline { get; set; }

        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage = "Entry fee cannot have more than 2 decimal places.")] // ensures that the number only has 2 decimal places
        public decimal EntryFee { get; set; }

        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage = "Fee per event cannot have more than 2 decimal places.")] // ensures that the number only has 2 decimal places
        public decimal FeePerEvent { get; set; }

        public string? Information { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Venue is required.")]
        public int VenueId { get; set; }

        [Required(ErrorMessage = "Meet Type is required")]
        public MeetType? MeetType { get; set; } // Made nullable in order to use the Required DataAnnotation

        public List<MeetEvent> Events { get; set; } = new List<MeetEvent>();

        public byte[]? RecordVersion { get; set; }


    }

    /**
     * 
     * Default Value: For value types (like enums), the default value is 0 (for the underlying integral type)
     * or the first enum value. By making it nullable, you can have a default state of null, which is more indicative
     * that no selection has been made.
     * 
     * Required Attribute Behavior: The Required attribute checks for null to determine if the property has been provided
     * a value. If you apply Required to a non-nullable type like an enum, it would always be considered as
     * having a value (because enums have default values), and the Required validation wouldn't be effective.
     */
}