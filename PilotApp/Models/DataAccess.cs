

using System.Collections.Generic;
using System.Data;
using System.Windows;
using Microsoft.Extensions.Logging;
using Npgsql;
using PilotApp.Views;


namespace PilotApp.Models
{

    public class DataAccess
    {
        private static readonly DataAccess instance = new DataAccess();
        private LoginWindow loginWindow = (LoginWindow)Application.Current.MainWindow;
        private string connectionString;
        private NpgsqlConnection connection;

        public static DataAccess Instance
        {
            get
            {
                return instance;
            }
        }

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }

            set
            {
                this.connectionString = value;
            }
        }

        //  Constructeur privé pour empêcher l'instanciation multiple
        private DataAccess()
        {
            this.ConnectionString = $"Host=srv-peda-new;Port=5433;Username={MainWindow.Instance.login};Password={MainWindow.Instance.mdp};Database=sae201_pilot;Options='-c search_path=lourdama'";
            try
            {
                connection = new NpgsqlConnection(ConnectionString);
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb de connexion GetConnection \n" + ConnectionString);
                throw;
            }
        }


        // pour récupérer la connexion (et l'ouvrir si nécessaire)
        public NpgsqlConnection GetConnection()
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                try
                {
                    this.ConnectionString = $"Host=srv-peda-new;Port=5433;Username={MainWindow.Instance.login} ;Password= {MainWindow.Instance.mdp};Database=sae201_pilot;Options='-c search_path=lourdama'";
                    connection = new NpgsqlConnection(ConnectionString);
                }
                catch{ }
            }

        
            return connection;
        }

        //  pour requêtes SELECT et retourne un DataTable ( table de données en mémoire)
        public DataTable ExecuteSelect(NpgsqlCommand cmd)
        {
            DataTable dataTable = new DataTable();
            try
            {
                cmd.Connection = GetConnection();
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return dataTable;
        }

        //   pour requêtes INSERT et renvoie l'ID généré

        public int ExecuteInsert(NpgsqlCommand cmd)
        {
            int nb = 0;
            try
            {
                cmd.Connection = GetConnection();

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    nb = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb avec une requete insert " + cmd.CommandText);
                throw;
            }
            return nb;
        }




        //  pour requêtes UPDATE, DELETE
        public int ExecuteSet(NpgsqlCommand cmd)
        {
            int nb = 0;
            try
            {
                cmd.Connection = GetConnection();
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                nb = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) {
                LogError.Log(ex, "Pb avec une requete set " + cmd.CommandText);
                throw;
            }
            return nb;

        }

        // pour requêtes avec une seule valeur retour  (ex : COUNT, SUM) 
        public object ExecuteSelectUneValeur(NpgsqlCommand cmd)
        {
            object res = null;
            try
            {
                cmd.Connection = GetConnection();
                res = cmd.ExecuteScalar();
            }
            catch (Exception ex) { 
                LogError.Log(ex, "Pb avec une requete select " + cmd.CommandText);
                throw;
            }
            return res;

        }

        //  Fermer la connexion 
        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}



