using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace traininghelper
{
    public class WordTableReader
    {
        private readonly string _filePath;
        public WordTableReader(string filePath)
        {
            _filePath = filePath;
        }

        public void ChangeTextInCell()
        {
            // Use the file name and path passed in as an argument to 
            // open an existing document.            
            using (WordprocessingDocument doc = WordprocessingDocument.Open(_filePath, true))
            {
                Table table =
                    doc.MainDocumentPart.Document.Body.Elements<Table>().First();

                List<TableRow> tableRows = table.Elements<TableRow>().ToList();

                foreach (var tableRow in tableRows)
                {
                    var cell = tableRow.Elements<TableCell>().ElementAt(0);
                    var text = cell.InnerText;
                    var date = new string(text.SkipWhile(x => !char.IsDigit(x)).ToArray());
                    string a = date;
                }

                //// Find the first paragraph in the table cell.
                //Paragraph p = cell.Elements<Paragraph>().First();

                //// Find the first run in the paragraph.
                //Run r = p.Elements<Run>().First();

                //// Set the text for the run.
                //Text t = r.Elements<Text>().First();
                //t.Text = txt;
            }
        }

        public string GetPlainText(OpenXmlElement element)
        {
            StringBuilder PlainTextInWord = new StringBuilder();
            foreach (OpenXmlElement section in element.Elements())
            {
                switch (section.LocalName)
                {
                    // Text 
                    case "t":
                        PlainTextInWord.Append(section.InnerText);
                        break;


                    case "cr":                          // Carriage return 
                    case "br":                          // Page break 
                        PlainTextInWord.Append(Environment.NewLine);
                        break;


                    // Tab 
                    case "tab":
                        PlainTextInWord.Append("\t");
                        break;


                    // Paragraph 
                    case "p":
                        PlainTextInWord.Append(GetPlainText(section));
                        PlainTextInWord.AppendLine(Environment.NewLine);
                        break;


                    default:
                        PlainTextInWord.Append(GetPlainText(section));
                        break;
                }
            }


            return PlainTextInWord.ToString();
        }
    }
}
