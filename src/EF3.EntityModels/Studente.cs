using System;
using System.Text.RegularExpressions;

namespace EF3.EntityModels
{
	public class Studente
	{
		public Studente(string matricola)
		{
			Matricola = matricola;
		}

		public string Matricola { get; }
		public string Nome { get; set; }
		public string Cognome { get; set; }
		public string Email { get; private set; }
		public Indirizzo Indirizzo { get; set; }

		public void SetMail(string mail)
		{
			bool isEmail = Regex.IsMatch(mail,
				@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
				@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
				RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
			if (isEmail)
				Email = mail;
			else
				throw new ArgumentException($"{mail} non è un indirizzo di posta elettronica");
		}
	}

	public class Indirizzo
	{
		public Indirizzo(string strada, int cap)
		{
			Strada = strada;
			Cap = cap;
		}
		public string Strada { get;  }
		public int Cap { get; }
		public string Comune { get; set; }
	}
}
