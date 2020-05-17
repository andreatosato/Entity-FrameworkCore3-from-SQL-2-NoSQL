using System;
using System.Collections.Generic;
using System.Linq;

namespace EF3.EntityModels
{
	public class Esame
	{
		public Esame(string codice, TipoEsame tipo, DateTimeOffset dataProva)
		{
			Codice = codice;
			Tipo = tipo;
			DataProva = dataProva;
		}

		public string Codice { get;  }
		public TipoEsame Tipo { get; }
		public DateTimeOffset DataProva { get; }
		public string Aula { get; set; }

		public HashSet<Studente> Studenti { get; private set; }
		public void AddStudente(Studente studente)
		{
			if(!Studenti.Any(x => x.Matricola == studente.Matricola))
				Studenti.Add(studente);
		}
	}

	public enum TipoEsame
	{
		Preappello,
		Appello,
		PrimaMetà,
		SecondaMetà,
		ProvaOrale
	}
}
