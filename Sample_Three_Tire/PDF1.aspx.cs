using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using Oracle.ManagedDataAccess.Client;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Sample_Three_Tire.CommonUtilities;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Web.UI.WebControls;


namespace Sample_Three_Tire
{
    public partial class PDF1 : System.Web.UI.Page
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["conStr"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            GetData();
        }
        public void GetData()
        {
            DataTable dt = new DataTable();


            try
            {
                OracleCommand cmd = new OracleCommand("GET_PDF_USP", con);
                cmd.Parameters.Add("P_RECORDS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                OracleDataAdapter adp = new OracleDataAdapter(cmd);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    gvPdf.Visible = true;
                    gvPdf.DataSource = dt;
                    gvPdf.DataBind();
                    gvPdf.HeaderRow.TableSection = TableRowSection.TableHeader;




                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
        }

        //public void GeneratePDf()
        //{
        //    //    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //    Console.WriteLine("Generating PDF...");

        //    // Create a new PDF document
        //    var document = new PdfDocument();
        //    document.Info.Title = "Created with PDFsharp";

        //    // Create an empty page
        //    var page = document.AddPage();

        //    // Get an XGraphics object for drawing
        //    var gfx = XGraphics.FromPdfPage(page);

        //    // Create a font

        //    var font = new XFont("Times New Roman", 20, XFontStyle.BoldItalic);
        //    PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true, PdfFontEmbedding.Always);
        //    XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
        //    // Draw the text
        //    gfx.DrawString("Hello, World!", font, XBrushes.Black,
        //    new XRect(0, 0, page.Width, page.Height),
        //    XStringFormats.Center);

        //    // Save the document...
        //    var filename = "HelloWorld.pdf";
        //    document.Save(filename);
        //    Console.WriteLine("PDF Generated!");
        //}

        public void NewPDF()
        {
            try
            {
                try
                {
                    // PdfSharp.Fonts.GlobalFontSettings.FontResolver = new MyFontResolver();

                    PDFform pdfForm = new PDFform(GetTable(), Server.MapPath("~/ImagesFolder/PDF.jpg"));

                    // Create a MigraDoc document
                    Document document = pdfForm.CreateDocument();
                    document.UseCmykColor = true;



                    // Create a renderer for PDF that uses Unicode font encoding
                    PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);

                    //  PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true, PdfFontEmbedding.Always);
                    // XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
                    // Set the MigraDoc document
                    pdfRenderer.Document = document;


                    // Create the PDF document
                    pdfRenderer.RenderDocument();

                    //pdfRenderer.Save(path);

                    // Process.Start(filename);

                    pdfRenderer.Save("D:\\test.pdf");
                    // ...and start a viewer.
                    Process.Start("D:\\test.pdf");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void bntGeneratePDf_Click(object sender, EventArgs e)
        {
            string base64 = Request.Form[hfImageData.UniqueID].Split(',')[1];
            byte[] bytes = Convert.FromBase64String(base64);


            string folderPath = Server.MapPath("~/ImagesFolder/");  //Create a Folder in your Root directory on your solution.
            string fileName = "PDF.jpg";
            string imagePath = folderPath + fileName;


            MemoryStream ms = new MemoryStream(bytes);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            img.Save(imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);

            //NewPDF();
            GeneratePDF("","");
        }

        public DataTable GetTable()
        {
            try
            {
                DataTable dt = new DataTable();
                OracleCommand cmd = new OracleCommand("GET_PDF_USP", con);
                cmd.Parameters.Add("P_RECORDS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                OracleDataAdapter adp = new OracleDataAdapter(cmd);
                adp.Fill(dt);

                return dt;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
        }


        private void GeneratePDF(string filename, string imageLoc)
        {
            PdfDocument document = new PdfDocument();

            // Create an empty page or load existing
            PdfPage page = document.AddPage();

            string folderPath = Server.MapPath("~/ImagesFolder/PDF.jpg");
            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);
            DrawImage(gfx, folderPath, 50, 50, 450, 450);

            // Save and start View
            document.Save("D:\\test.pdf");
            Process.Start("D:\\test.pdf");
        }

        void DrawImage(XGraphics gfx, string jpegSamplePath, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(jpegSamplePath);
            gfx.DrawImage(image, x, y, width, height);
        }
    }
}