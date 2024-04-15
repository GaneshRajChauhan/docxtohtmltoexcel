//using Spire.Doc;

//class Program
//{
//    static void Main(string[] args)
//    {
//        // Replace "input.docx" with the path to your Word document
//        string inputFilePath = "test_ganesh.docx";

//        // Replace "output.html" with the desired path for the HTML output
//        string outputFilePath = "output.html";

//        // Convert Word document to HTML
//        ConvertToHtml(inputFilePath, outputFilePath);
//    }

//    static void ConvertToHtml(string inputFilePath, string outputFilePath)
//    {
//        // Load the Word document
//        Document doc = new Document();
//        doc.LoadFromFile(inputFilePath);

//        // Save the document as HTML
//        doc.SaveToFile(outputFilePath, FileFormat.Html);
//    }
//}

using Aspose.Words.Tables;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Data;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Replace "input.html" with the path to your HTML file
        string filePath = "output.html";

        // Read HTML file
        string htmlContent = ReadHtmlFile(filePath);

        // Output HTML content
        Console.WriteLine(htmlContent);
    }

    static string ReadHtmlFile(string filePath)
    {
        try
        {
            List<string> columnsdata= new List<string>();
            Dictionary<string,string> columns = new Dictionary<string,string>();
            List<System.Data.DataTable> datatables = new List<System.Data.DataTable>();
            System.Data.DataTable dt = null;
        
            // Read all text from HTML file
            string htmlContent = File.ReadAllText(filePath);
            var splittedtable = htmlContent.Split("<table");
            foreach (var item in splittedtable.Skip(1))
            {
                dt = new System.Data.DataTable();
                int count = 0;
                var splittedtr = item.Split("<tr");
                foreach (var item1 in splittedtr.Skip(1))
                {
                    count += 1;
                    var splittedtd = item1.Split("<td");
                    var columncunt = 0;
                    DataRow dr = null;
                    if (count > 0)
                        dr = dt.NewRow();
                    foreach (var itm2 in splittedtd.Skip(1))
                    {

                        var splittedtags = itm2.Split("\">");
                        var actualstringdata = splittedtags.Last();
                        var actualstring = actualstringdata.Substring(0, actualstringdata.IndexOf("</"));
                        columnsdata.Add(actualstring);
                        if (count == 1)
                        {
                            dt.Columns.Add(actualstring, typeof(string));
                        }
                        else
                        {
                            dr[columncunt] = actualstring;
                            columncunt += 1;

                        }
                    }
                    dt.Rows.Add(dr);

                }
                datatables.Add(dt);
            }
            
            return htmlContent;
        }
        catch (Exception ex)
        {
            // Handle any exceptions
            Console.WriteLine("Error reading HTML file: " + ex.Message);
            return null;
        }
    }
}

