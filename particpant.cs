using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using QRCoder;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PPE_Desktop
{
    public partial class particpant : Form
    {
        public particpant()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            String QRBrut ="@ "+lbMail.Text+" Le nom : "+lbNom.Text+" le prénom :"+lbPrenom.Text;
            QRCodeData.qrCodeData = qrGenerator.CreateQrCode(QRBrut, QRCodeGenerator.ECCLevel.Q);

            Base64QRCode qrCode = new Base64QRCode(qrCodeData);
            string qrCodeImageAsBase64 = qrCode.GetGraphic(20);

            StreamWriter monStreamWriter = new StreamWriter(@"BadgeSalon.html");//Necessite using System.IO;

            String strImage = "<img src = \"data:image/png;base64," + qrCodeImageAsBase64 + "\">";
            monStreamWriter.WriteLine("<html>");
            monStreamWriter.WriteLine("<body>");
            string temptext = "<P>" + "Le contenue du QRcode" + "</P>";
            monStreamWriter.WriteLine(temptext);
            temptext = "<P>" + QRBrut + "</P>";
            monStreamWriter.WriteLine(temptext);
            monStreamWriter.WriteLine(strImage);    //Ecriture de l'image base 64 dans le fichier
            monStreamWriter.WriteLine("</body>");
            monStreamWriter.WriteLine("</html>");

            // Fermeture du StreamWriter (Très important) 
            monStreamWriter.Close();
            QRBrut = "QRcode crée";
        }
              
        }
    }
}
