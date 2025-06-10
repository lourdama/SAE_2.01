using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Models
{
    public class MiseEnForme
    {
        // Vérifie si la valeur est comprise entre deux bornes (inclusivement)
        public static bool EstEntre<T>(T valeur, T min, T max) where T : IComparable<T>
        {
            return valeur.CompareTo(min) >= 0 && valeur.CompareTo(max) <= 0;
        }

        public static bool EstEntre<T>(T valeur, T min) where T : IComparable<T>
        {
            return valeur.CompareTo(min) >= 0;
        }

        // Vérifie si la chaîne n'est pas null ou whitespace
        public static bool NEstPasNullOuWhitespace(string valeur)
        {
            return !string.IsNullOrWhiteSpace(valeur);
        }

        public static bool NEstPasNull<T>(T valeur)
        {
            return valeur != null;
        }

        // Met en forme la chaîne : première lettre en majuscule, le reste en minuscules sauf celles après un espace
        public static string FormaterString(string valeur)
        {
            if (string.IsNullOrWhiteSpace(valeur))
                return valeur;

            var textInfo = CultureInfo.CurrentCulture.TextInfo;
            StringBuilder chaineFormatee = new StringBuilder();

            string[] mots = valeur.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string mot in mots)
            {
                if (chaineFormatee.Length > 0)
                {
                    chaineFormatee.Append(' ');
                }

                if (mot.Length > 0)
                {
                    string motFormate = textInfo.ToLower(mot);
                    motFormate = textInfo.ToTitleCase(motFormate);
                    chaineFormatee.Append(motFormate);
                }
            }

            return chaineFormatee.ToString();
        }
    }
}
