using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF3.EntityModels
{
	public class Corso
	{
		public Corso(string nome, string docente, int numeroCrediti)
		{
			Nome = nome;
			Docente = docente;
			NumeroCrediti = numeroCrediti;
		}

		public string Nome { get; }
		public string Docente { get; }
		public int NumeroCrediti { get; }
		public TipoCorso TipoCorso { get; set; }
		public HashSet<Esame> Esami { get; private set; }
		public void AddEsame(Esame esame)
		{
			if (!Esami.Any(x => x.Codice == esame.Codice))
				Esami.Add(esame);
		}
		public CreditiExtra CreditiExtra { get; set; }
	}

	public enum TipoCorso
	{
		Obbligatorio,
		Facoltativo
	}

	public class CreditiExtra
	{
		public string Nome { get; set; }
		public short Crediti { get; set; }
		public int NumeroOre { get; set; }
	}
}
