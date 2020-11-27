using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LINQ.ConsoleApp.Esercitazione
{
    public static class PersonaExtensions
    {
        public static List<VeicoliPosseduti> VeicoliPosseduti(this Persona persona, List<Veicolo> elencoVeicoli)
        {
            //var resultMethod = elencoVeicoli
            //    .Where(v => v.ProprietarioID == persona.ID)
            //    .Select(v => new VeicoliPosseduti { ID = v.ID, Targa = v.Targa, Prezzo = v.Prezzo })
            //    .ToList();

            //return resultMethod;

            var resultQuery = (from v in elencoVeicoli
                      where v.ProprietarioID == persona.ID
                      select new VeicoliPosseduti { ID = v.ID, Targa = v.Targa, Prezzo = v.Prezzo }).ToList();

            return resultQuery;
        }
    }

    public class VeicoliPosseduti
    {
        public int ID { get; set; }
        public string Targa { get; set; }
        public decimal Prezzo { get; set; }
    }
}
