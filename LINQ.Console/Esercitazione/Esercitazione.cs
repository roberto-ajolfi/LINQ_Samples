using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQ.ConsoleApp.Esercitazione
{
    public static class Esercizi
    {
        private static List<Persona> persone = new List<Persona>
        {
            new Persona { ID = 1, Nome = "Roberto", Cognome = "Ajolfi", Nazione = "Italia" },
            new Persona { ID = 2, Nome = "Alice", Cognome = "Colella", Nazione = "Italia" },
            new Persona { ID = 3, Nome = "Paolo", Cognome = "Rossi", Nazione = "Italia" },
            new Persona { ID = 4, Nome = "Mario", Cognome = "Bianchi", Nazione = "Italia" },
        };

        private static List<Veicolo> veicoli = new List<Veicolo>
        {
            new Veicolo { ID = 1, Colore = "Rosso", Peso = 1000, Prezzo = 1000, Targa = "YC888ZZ", ProprietarioID = 1 },
            new Veicolo { ID = 2, Colore = "Rosso", Peso = 1200, Prezzo = 2000, Targa = "AA967OP", ProprietarioID = 1 },
            new Veicolo { ID = 3, Colore = "Grigio", Peso = 500, Prezzo = 700, Targa = "ND999AA", ProprietarioID = 2 },
            new Veicolo { ID = 4, Colore = "Nero", Peso = 900, Prezzo = 1500, Targa = "HK086KK", ProprietarioID = 4 },
            new Veicolo { ID = 5, Colore = "Verde", Peso = 1900, Prezzo = 3000, Targa = "ZA876AA", ProprietarioID = 4 }
        };

        public static void Esegui()
        {
            // # macchine per colore
            Console.WriteLine("=== # macchine per colore ===");

            var macchinePerColoreMethod = veicoli.GroupBy(v => v.Colore, (key, v) => new { Colore = key, Count = v.Count() });

            var macchinePerColoreQuery = from v in veicoli
                          group v by v.Colore into g
                          select new { Colore = g.Key, Count = g.Count() };

            foreach (var item in macchinePerColoreMethod)
                Console.WriteLine($"{item.Colore} => {item.Count}");
            Console.WriteLine("=================================");
            Console.WriteLine();

            // Prezzo medio e Peso complessivo dei veicoli di ogni persona
            Console.WriteLine("=== Prezzo medio e Peso complessivo dei veicoli di ogni persona ===");

            var prezzoMedioEPesoComplessivoQuery = from v in veicoli
                                              group v by v.ProprietarioID into vpp
                                              select new { 
                                                  PersonaId = vpp.Key, 
                                                  PesoComplessivo = vpp.Sum(v => v.Peso), 
                                                  PrezzoMedio = vpp.Average(v => v.Prezzo) 
                                              };

            var prezzoMedioEPesoComplessivoMethod = veicoli.GroupBy(
                v => v.ProprietarioID, 
                (key, vpp) => new { PersonaId = key,
                    PesoComplessivo = vpp.Sum(v => v.Peso),
                    PrezzoMedio = vpp.Average(v => v.Prezzo)
                }
            );

            foreach (var item in prezzoMedioEPesoComplessivoQuery)
                Console.WriteLine($"{item.PersonaId} => Peso Complessivo: {item.PesoComplessivo} / Prezzo Medio: {item.PrezzoMedio}");


            var prezzoMedioEPesoComplessivoQuery2 = from v in veicoli
                                                    group v by v.ProprietarioID into vpp
                                                    join p in persone on vpp.Key equals p.ID
                                                    select new
                                                    {
                                                        PersonaId = vpp.Key,
                                                        Nome = $"{p.Cognome} {p.Nome}",
                                                        PesoComplessivo = vpp.Sum(v => v.Peso),
                                                        PrezzoMedio = vpp.Average(v => v.Prezzo)
                                                    };

            var prezzoMedioEPesoComplessivoMethod2 = persone.GroupJoin(
                veicoli,
                p => p.ID,
                v => v.ProprietarioID,
                (p, v) => new
                {
                    PersonaId = p.ID,
                    Nome = $"{p.Cognome} {p.Nome}",
                    PesoComplessivo = v.Any() ? v.Sum(v => v.Peso) : 0,   // <== necessari perchè GroupJoin restituisce comunque record anche dove non c'è corrispondenza
                    PrezzoMedio = v.Any() ? v.Average(v => v.Prezzo) : 0    // <==
                }).ToList();

            foreach (var item in prezzoMedioEPesoComplessivoMethod2)
                Console.WriteLine($"[{item.PersonaId}] {item.Nome} => Peso Complessivo: {item.PesoComplessivo} / Prezzo Medio: {item.PrezzoMedio}");

            Console.WriteLine("=================================");
            Console.WriteLine();

            // extension method della classe Persona ( VeicoliPosseduti(List<Veicoli> elencoVeicoli) ) 
            // che restituisca l’elenco dei veicoli posseduti (campi: ID, Targa, Prezzo)
            Console.WriteLine("=== Extension Method ===");

            foreach (var p in persone)
            {
                Console.WriteLine($"== Veicoli di {p.Nome} {p.Cognome} ==");
                foreach (var item in p.VeicoliPosseduti(veicoli))
                    Console.WriteLine($"{item.ID} - {item.Targa} - {item.Prezzo}");
                Console.WriteLine("====");
            }

            Console.WriteLine("=================================");
            Console.WriteLine();
        }
    }
}
