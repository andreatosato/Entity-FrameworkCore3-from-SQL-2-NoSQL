using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EF3.NoSQLContext
{
	public class StudentCollection
	{
		public StudentCollection(string freshman)
		{
			Freshman = freshman;
		}

		public string Freshman { get; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; private set; }
		public AddressCollection Address { get; set; }

		public void SetMail(string mail)
		{
			bool isEmail = Regex.IsMatch(mail,
				@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
				@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
				RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
			if (isEmail)
				Email = mail;
			else
				throw new ArgumentException($"{mail} is not email");
		}
		
	}

	public class AddressCollection
	{
		public AddressCollection(string street, int cap)
		{
			Street = street;
			Cap = cap;
		}
		public string Street { get; }
		public int Cap { get; }
		public string City { get; set; }
	}
}
