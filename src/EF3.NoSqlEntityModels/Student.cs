using System;
using System.Text.RegularExpressions;

namespace EF3.NoSqlEntityModels
{

	// TODO: Extracredit metterli sullo studente come lista. (StudentCollection)

	public class Student
	{
		private readonly DateTimeOffset _createDate;
		private readonly DateTimeOffset _updateDate;
		public Student(string identificationNumber)
		{
			IdentificationNumber = identificationNumber;
			_createDate = DateTimeOffset.UtcNow;
			_updateDate = DateTimeOffset.UtcNow;
		}

		public string IdentificationNumber { get; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; private set; }
		public Address Address { get; set; }

		public DateTimeOffset CreateDate => _createDate;
		public DateTimeOffset UpdateDate => _updateDate;

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

	public class Address
	{
		public Address(string street, int zipCode)
		{
			Street = street;
			ZipCode = zipCode;
		}
		public string Street { get;  }
		public int ZipCode { get; }
		public string City { get; set; }
	}
}
