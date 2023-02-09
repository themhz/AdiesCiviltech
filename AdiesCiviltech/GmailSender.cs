using System;
using System.IO;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Mvc.Formatters;
using MimeKit;

namespace AdiesCiviltech
{
    class GmailSender
    {
        static string[] Scopes = { GmailService.Scope.GmailSend };
        static string ApplicationName = "Gmail API .NET Quickstart";
        static string credentialsFilePath = @"C:\Users\themis\Downloads\client_secret_216622909239-ier6ipoeoiupoahdvtpugeutlllcntpl.apps.googleusercontent.com (3).json";
        static string tokenFilePath = "token.json";

        public void SendEmailWithAttachment(string from, string fromName, string to, string subject, string body, string filePath)
        {
            UserCredential credential;

            using (var stream = new FileStream(credentialsFilePath, FileMode.Open, FileAccess.Read))
            {
                string credPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, tokenFilePath);
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            var gmailService = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromName, from));
            message.To.Add(new MailboxAddress("", to));            
            message.Subject = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(subject));


            var text = Encoding.UTF8.GetBytes(body);
            var textPart = new TextPart("plain") { Text = body, ContentTransferEncoding = ContentEncoding.QuotedPrintable };
            textPart.ContentType.Charset = Encoding.UTF8.WebName;
            var multipart = new Multipart("mixed");
            multipart.Add(textPart);
            multipart.Add(new MimePart("application/pdf") { Content = new MimeContent(File.OpenRead(filePath)), ContentDisposition = new ContentDisposition(ContentDisposition.Attachment), ContentTransferEncoding = ContentEncoding.Base64, FileName = Path.GetFileName(filePath) });
            message.Body = multipart;

            var builder = new BodyBuilder();                        
            builder.Attachments.Add(filePath);            

            var request = gmailService.Users.Messages.Send(new Message
            {
                Raw = Encode(message.ToString())
            }, "me");

            request.Execute();
        }

        private string Encode(string text)
        {
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(byteArray);
        }

    }
}
