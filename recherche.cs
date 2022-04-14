﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPE_Desktop
{
    public partial class recherche : Form
    {
        public recherche()
        {
            InitializeComponent();
        }

        private void bt_recherche_Click(object sender, EventArgs e)
        {
            lb_sortie.Text = "";
            DBConnection dbCon = new DBConnection();
            dbCon.Server = "localhost";
            dbCon.DatabaseName = "ppe";
            dbCon.UserName = "root";
            dbCon.Password = "";
            if (dbCon.IsConnect())
            {
                String NomParticipant = tb_recherche.Text;

            String query = "select nom,prenom,mail FROM participant where nom = ?nom";
            query = Tools.PrepareLigne(query, "?nom", Tools.PrepareChamp(NomParticipant, "Chaine"));
            var cmd = new MySqlCommand(query, dbCon.Connection);
            List<Participant> LesParticipantTrouves = new List<Participant>();
            MySqlDataReader TheReader = cmd.ExecuteReader();
            while (TheReader.Read())
            {

                Participant unParticipant = new Participant
                {
                    ParticipantNom = (string)TheReader["nom"],
                    ParticipantPrenom = (string)TheReader["prenom"],
                    ParticipantMail = (string)TheReader["mail"]
                };
                LesParticipantTrouves.Add(unParticipant);
            }
            if (LesParticipantTrouves.Count > 0)
            {
                foreach (Participant leParticipant in LesParticipantTrouves)
                    lb_sortie.Text += (" nom : " + leParticipant.ParticipantNom + Environment.NewLine
                                        + " prenom : " + leParticipant.ParticipantPrenom + Environment.NewLine
                                        + " mail :" + leParticipant.ParticipantMail);
            }
            else
            {
                MessageBox.Show("pas de résutal");
                TheReader.Close();
            }
            }
            else
            {
                MessageBox.Show("pas de connexion");
            }
        }
    }
}
