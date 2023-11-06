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
        private string dateFrom;
        private string dateTo;
        private string applicationDate;
        private string numberOfDays;

        public Form1()
        {
            InitializeComponent();
            monthCalendar1.MaxSelectionCount = 31; // Allow up to a month to be selected
            applicationDate = DateTime.Now.ToString("yyyy-MM-dd");
            lblApplicationDate.Text = applicationDate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WordEditor we = new WordEditor();
            string filePath = @"C:\Users\themis\source\repos\AdiesCiviltech\ΦΟΡΜΑ ΑΙΤΗΜΑΤΟΣ ΑΔΕΙΑΣ.docx";
            Document EditWord = we.EditWord(filePath);
            we.dateFrom = dateFrom;
            we.dateTo = dateTo;            
            we.applicationDate = lblApplicationDate.Text;
            we.numberOfDays = numberOfDays;
            //we.applicationDate =;
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

        
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            

            // The user has selected a range of dates.
            // Get the start and end dates from the selection range.
            dateFrom = e.Start.ToString("yyyy-MM-dd"); // Format the date as you need
            dateTo = e.End.ToString("yyyy-MM-dd");     // Format the date as you need
            lblFrom.Text = dateFrom;
            lblTo.Text = dateTo;
            TimeSpan dateDifference = e.End - e.Start ;
            lblNumberOfDays.Text = (dateDifference.Days + 1).ToString();
            numberOfDays = lblNumberOfDays.Text;
            // Now you can use dateFrom and dateTo as needed
            //MessageBox.Show("Date From: " + dateFrom + "\nDate To: " + dateTo);
        }
    }
}
