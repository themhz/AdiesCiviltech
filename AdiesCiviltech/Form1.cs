using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Office.Interop.Word;

namespace AdiesCiviltech
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WordEditor we = new WordEditor();
            string filePath = @"C:\Users\themis\source\repos\AdiesCiviltech\ΦΟΡΜΑ ΑΙΤΗΜΑΤΟΣ ΑΔΕΙΑΣ.docx";
            Document EditWord = we.EditWord(filePath);
            // Replace with your own email address and password
            string from = "themis@civiltech.gr";
            // Replace with the recipient's email address
            string to = "themhz@gmail.com";
            // Replace with the subject and body of the email
            string subject = "Αιτηση αδείας";
            string body = "Αίτηση αδείας";

            GmailSender gs = new GmailSender();
            gs.SendEmailWithAttachment(from, "Ευθύμιος Θεοτοκάτος", to, subject, body, filePath);


        }
    }
}
