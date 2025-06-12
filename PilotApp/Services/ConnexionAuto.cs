using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace PilotApp.Services
{
    public class FichierParametres
    {
        public bool ResterConnecte { get; set; }
        public string NomUtilisateur { get; set; } = "";
        public string MotDePasse { get; set; } = "";
    }
    public static class GestionnaireParametres
    {
        private static readonly string CheminFichier = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "\\data\\connexion.json");
        public static FichierParametres Charger()
        {
            try
            {

                if (!File.Exists(CheminFichier))
                    return new FichierParametres();

                var json = File.ReadAllText(CheminFichier);
                return JsonSerializer.Deserialize<FichierParametres>(json)
                       ?? new FichierParametres();
            }
            catch
            {
                return new FichierParametres();
            }

        }
        public static void Sauvegarder(FichierParametres p)
        {

            var json = JsonSerializer.Serialize(p, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(CheminFichier, json);
        }
    }
    
}
