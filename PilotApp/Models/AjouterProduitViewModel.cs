using PilotApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotApp.Models
{
    internal class AjouterProduitViewModel
    {
        public Produit Produit { get; set; }
        public ObservableCollection<TypePointe> LesTypesPointes => MainWindow.Instance.Pilot.LesTypesPointes;
        public ObservableCollection<Type> LesTypes => MainWindow.Instance.Pilot.LesTypes;
        public ObservableCollection<Couleur> LesCouleurs => MainWindow.Instance.Pilot.LesCouleurs;

        public AjouterProduitViewModel(Produit produit)
        {
            Produit = produit;
        }
    }
}
