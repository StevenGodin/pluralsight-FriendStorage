using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FriendStorage.UI.Wrapper
{
	public partial class FriendEmailWrapper
	{
		public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (string.IsNullOrWhiteSpace(Email))
				yield return new ValidationResult("Email is required", 
					new[] { nameof(Email) });

			if(!new EmailAddressAttribute().IsValid(Email))
				yield return new ValidationResult("Email is not a valid email address", 
					new[] { nameof(Email) });
		}
	}
}