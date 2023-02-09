using System;
using Microsoft.Office.Interop.Word;

namespace AdiesCiviltech
{
    public class WordEditor
    {
        public void EditDocument(string filePath)
        {
            Application wordApp = new Application();
            Document wordDoc = wordApp.Documents.Open(filePath);
            Range editRange = wordDoc.Content;
            editRange.Find.ClearFormatting();
            editRange.Find.Execute("Text to find");
            editRange.Text = "Edited text";
            wordDoc.Save();
            wordDoc.Close();
        }
    }
}
