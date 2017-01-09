using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using Android.Content.Res;

namespace TrainingHelper
{
    public class WordTableReader
    {
        private readonly string _filePath;
        private int _year;
        public WordTableReader(string filePath)
        {
            _filePath = filePath;
            _year = 2016;
        }

        public void SaveChangedDocument(List<TrainingDay> trainingDays)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(_filePath, true))
            {
                Table table = doc.MainDocumentPart.Document.Body.Elements<Table>().First();

                List<TableRow> tableRows = table.Elements<TableRow>().ToList();
                foreach (var tableRow in tableRows)
                {
                    if (tableRow.Elements<TableCell>().Count() >= 3)
                    {
                        var dateCell = tableRow.Elements<TableCell>().ElementAt(0);
                        var dateString = new string(dateCell.InnerText.SkipWhile(x => !char.IsDigit(x)).ToArray());
                        var trainingDay = trainingDays.Find(t => (t.ExcercisesDay.Day.ToString().PadLeft(2, '0') + "." + t.ExcercisesDay.Month.ToString().PadLeft(2, '0')) == dateString);
                        if (trainingDay != null)
                        {
                            var descCell = tableRow.Elements<TableCell>().ElementAt(2);
                            if (!string.IsNullOrEmpty(trainingDay.Description))
                            {
                                Paragraph p = descCell.Elements<Paragraph>().First();
                                Run r = p.Elements<Run>().First();
                                Text text = r.Elements<Text>().First();
                                text.Text = trainingDay.Description;
                            }
                            else
                            {
                                Paragraph para = descCell.AppendChild(new Paragraph());
                                Run run = para.AppendChild(new Run());
                                run.AppendChild(new Text(trainingDay.Description));
                            }
                        }
                    }
                }
            }
        }

        public List<TrainingDay> ChangeTextInCell()
        {
            List<TrainingDay> trainingDays = new List<TrainingDay>();
            using (WordprocessingDocument doc = WordprocessingDocument.Open(_filePath, true))
            {
                Table table =
                    doc.MainDocumentPart.Document.Body.Elements<Table>().First();

                List<TableRow> tableRows = table.Elements<TableRow>().ToList();
                foreach (var tableRow in tableRows)
                {
                    if (tableRow.Elements<TableCell>().Count() >= 3)
                    {
                        var dateCell = tableRow.Elements<TableCell>().ElementAt(0);
                        var excercisesCell = tableRow.Elements<TableCell>().ElementAt(1);
                        var descCell = tableRow.Elements<TableCell>().ElementAt(2);

                        var dateString = new string(dateCell.InnerText.SkipWhile(x => !char.IsDigit(x)).ToArray());
                        var excercise = excercisesCell.InnerText;
                        var desc = descCell.InnerText;
                        var year = GetYear(dateString);
                        var exerciseDate = DateTime.ParseExact(dateString + "." + year, "dd.MM.yyyy", null);
                        trainingDays.Add(new TrainingDay(exerciseDate, excercise, desc));
                    }

                }
                return trainingDays.OrderByDescending(t => t.ExcercisesDay).ToList();
            }
        }

        private string GetYear(string date)
        {
            var days = date.Split('.');
            if(days[1].Equals("12") && days[0].Equals("31"))
            {
                _year++;
                return (_year - 1).ToString();
            }
            return _year.ToString();
        }

    }
}
