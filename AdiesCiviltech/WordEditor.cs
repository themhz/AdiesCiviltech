using System;
using System.IO;
using Microsoft.Office.Interop.Word;
using System.Net.Mail;
using MailMessage = System.Net.Mail.MailMessage;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using Google.Apis.Gmail.v1.Data;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Word.Application;
using DocumentFormat.OpenXml.ExtendedProperties;
using Word = Microsoft.Office.Interop.Word;
using System.Linq;
using CheckBox = Microsoft.Office.Interop.Word.CheckBox;
using System.Diagnostics;

namespace AdiesCiviltech
{
    public class WordEditor
    {
        public Document EditWord(string filePath)
        {
            Application wordApp = new Application();
                        
            wordApp.Visible = false;

            foreach (Word.Document doc in wordApp.Documents)
            {
                doc.Close(false);
            }

            


            //wordApp.Visible = false;
            Document wordDoc = wordApp.Documents.Open(filePath);

            wordApp.Selection.Find.ClearFormatting();
            wordApp.Selection.Find.Replacement.ClearFormatting();
            wordApp.Selection.Find.Text = "{ApplicationDate}";
            wordApp.Selection.Find.Replacement.Text = "9/2/2023";
            wordApp.Selection.Find.Execute(Replace: WdReplace.wdReplaceAll);

            wordApp.Selection.Find.Replacement.ClearFormatting();
            wordApp.Selection.Find.Text = "{Name}";
            wordApp.Selection.Find.Replacement.Text = "Ευθύμιος Θεοτοκάτος";
            wordApp.Selection.Find.Execute(Replace: WdReplace.wdReplaceAll);


            wordApp.Selection.Find.Replacement.ClearFormatting();
            wordApp.Selection.Find.Text = "{From}";
            wordApp.Selection.Find.Replacement.Text = "10/2/2023";
            wordApp.Selection.Find.Execute(Replace: WdReplace.wdReplaceAll);


            wordApp.Selection.Find.Replacement.ClearFormatting();
            wordApp.Selection.Find.Text = "{To}";
            wordApp.Selection.Find.Replacement.Text = "12/2/2023";
            wordApp.Selection.Find.Execute(Replace: WdReplace.wdReplaceAll);

            wordApp.Selection.Find.Replacement.ClearFormatting();
            wordApp.Selection.Find.Text = "{NoOfDays}";
            wordApp.Selection.Find.Replacement.Text = "3";
            wordApp.Selection.Find.Execute(Replace: WdReplace.wdReplaceAll);

            var contentControls = wordDoc.ContentControls;
            

            //var checkBoxes = wordDoc.ContentControls.OfType<>();

            //foreach (var checkBox in checkBoxes)
            //{

            //}

            //wordDoc.Save();
            try
            {
                                
                var newDoc = wordApp.Documents.Add();                
                wordDoc.Range().Copy();                
                newDoc.Range().Paste();
                newDoc.SaveAs2(@"C:\Users\themis\source\repos\AdiesCiviltech\ΦΟΡΜΑ_ΑΙΤΗΜΑΤΟΣ_ΑΔΕΙΑΣ_New.docx");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            //// Save the document to a new file
            //using (WordprocessingDocument newWordDoc = WordprocessingDocument.Create(@"C:\Users\themis\source\repos\AdiesCiviltech\ΦΟΡΜΑ ΑΙΤΗΜΑΤΟΣ ΑΔΕΙΑΣ_New.docx", WordprocessingDocumentType.Document))
            //{
            //    newWordDoc.AddMainDocumentPart();
            //    newWordDoc.MainDocumentPart.Document = wordDoc;
            //    newWordDoc.MainDocumentPart.Document.Save();
            //}

            wordDoc.Close();
            wordApp.Quit();

            return wordDoc;
        }
    }
}
