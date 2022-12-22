
using iText.Layout.Font;
using iText.Layout.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Document = iTextSharp.text.Document;

namespace Sample_Three_Tire
{
    public partial class PDF2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)

        {
            GeneratePdfFromHtml();
        }

        //public void GenerateImage()
        //{
        //    try

        //    {

        //        //Open a file dialog for saving map documents

        //        SaveFileDialog1.Title = "Save As BMP File";

        //        SaveFileDialog1.Filter = "Bitmap File (*.bmp)|*.bmp";



        //        if (SaveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)

        //        {

        //            return;

        //        }

        //    }

        //    catch (Exception)

        //    {

        //        return;

        //    }



        //    //Exit if no map document is selected

        //    string sFilePath;

        //    sFilePath = SaveFileDialog1.FileName;

        //    if (sFilePath == "")

        //    {

        //        return;

        //    }

        //    else

        //    {

        //        //DataGrid2Bitmap.ConvertDG2BMP(DataGridView1, sFilePath);

        //    }
        //}


        //public void printpdf()
        //{


        //    string[] sources = new string[] { "english.xml", "arabic.xml", "hindi.xml", "tamil.xml" };
        //    //PdfWriter writer = new PdfWriter(DEST);
        //    //PdfDocument pdfDocument = new PdfDocument(writer);
        //    //Document document = new Document(pdfDocument);

        //    PdfWriter writer = new PdfWriter(DES);
        //    PdfDocument pdfdocument = new PdfDocument(writer);
        //    Document document = new Document(pdfdocument);
        //    FontSet set = new FontSet();
        //    set.AddFont("NotoNaskhArabic-Regular.ttf");
        //    set.AddFont("NotoSansTamil-Regular.ttf");
        //    set.AddFont("FreeSans.ttf");
        //    //document.SetFontProvider(new FontProvider(set));
        //    //document.SetProperty(Property.FONT, new String[] { "MyFontFamilyName" });
        //    foreach (string source in sources)
        //    {
        //        XmlDocument doc = new XmlDocument();
        //        var stream = new FileStream(source, FileMode.Open);
        //        doc.Load(stream);
        //        XmlNode element = doc.GetElementsByTagName("text").Item(0);
        //        Paragraph paragraph = new Paragraph();
        //        XmlNode textDirectionElement = element.Attributes.GetNamedItem("direction");
        //        Boolean rtl = textDirectionElement != null && textDirectionElement.InnerText.Equals("rtl");
        //        //if (rtl)
        //        //{
        //        //    paragraph.SetTextAlignment(TextAlignment.RIGHT);
        //        //}
        //        paragraph.Add(element.InnerText);
        //        document.Add(paragraph);
        //    }
        //    document.Close();



        //}


       public void GeneratePdfFromHtml()
        {
            //const string outputFilename = @".\Files\report.pdf";
            //const string inputFilename = @".\Files\report.html";

            string outputFilename = Server.MapPath("/Files/report.pdf");
            string inputFilename = Server.MapPath("/Files/report.html");//D:\MY_Works\Working_22\Practise\sample_threetire\Sample_Three_Tire\Files\report.html

            using (var input = new FileStream(inputFilename, FileMode.Open))
            using (var output = new FileStream(outputFilename, FileMode.Create))
            {
                CreatePdf(input, output);
            }
        }

        void CreatePdf(Stream htmlInput, Stream pdfOutput)
        {
            using (var document = new Document(PageSize.A4, 30, 30, 30, 30))
            {
                var writer = PdfWriter.GetInstance(document, pdfOutput);
                var worker = XMLWorkerHelper.GetInstance();

                document.Open();
                worker.ParseXHtml(writer, document, htmlInput, null, Encoding.UTF8, new UnicodeFontFactory());

                document.Close();
            }
        }

        public class UnicodeFontFactory : FontFactoryImp
        {
            private static readonly string FontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
              "arialuni.ttf");

            private readonly BaseFont _baseFont;

            public UnicodeFontFactory()
            {
                _baseFont = BaseFont.CreateFont(FontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            }

            public override Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color,
              bool cached)
            {
                return new Font(_baseFont, size, style, color);
            }
        }
    }
}