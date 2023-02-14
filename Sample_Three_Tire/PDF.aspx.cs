using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using Oracle.ManagedDataAccess.Client;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using Sample_Three_Tire.CommonUtilities;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sample_Three_Tire
{
    public partial class PDF : System.Web.UI.Page
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



                    //StringWriter stringWrite = new System.IO.StringWriter();  test for commit one 
                    //HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                    //string content;


                    //gvPdf.RenderControl(htmlWrite);


                    //content = stringWrite.ToString();

                    //Bitmap m_Bitmap = new Bitmap(800, 400);

                    //PointF point = new PointF(0, 0);

                    //SizeF maxSize = new System.Drawing.SizeF(800, 800);

                    ////CssData css = null;
                    ////HtmlRender.Render(Graphics.FromImage(m_Bitmap), "<html><head></head><body>" + content + "</body></html>", point, maxSize);

                    //m_Bitmap.Save(@"D:\Thumbnail1.bmp");
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

        public void GeneratePDf()
        {
            //    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            Console.WriteLine("Generating PDF...");

            // Create a new PDF document
            var document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";

            // Create an empty page
            var page = document.AddPage();

            // Get an XGraphics object for drawing
            var gfx = XGraphics.FromPdfPage(page);

            // Create a font

            var font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true, PdfFontEmbedding.Always);
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
            // Draw the text
            gfx.DrawString("Hello, World!", font, XBrushes.Black,
            new XRect(0, 0, page.Width, page.Height),
            XStringFormats.Center);

            // Save the document...
            var filename = "HelloWorld.pdf";
            document.Save(filename);
            Console.WriteLine("PDF Generated!");
        }

        public void NewPDF()
        {
            try
            {
                try
                {
                   // PdfSharp.Fonts.GlobalFontSettings.FontResolver = new MyFontResolver();

                    PDFform pdfForm = new PDFform(GetTable(), Server.MapPath("img2.gif"));

                    // Create a MigraDoc document
                    Document document = pdfForm.CreateDocument();
                    document.UseCmykColor = true;

                    //#if DEBUG
                    //                    // for debugging only...
                    //                    MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(document, "MigraDoc.mdddl");
                    //                    document = MigraDoc.DocumentObjectModel.IO.DdlReader.DocumentFromFile("MigraDoc.mdddl");
                    //#endif

                    // Create a renderer for PDF that uses Unicode font encoding
                    PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);

                    // Set the MigraDoc document
                    pdfRenderer.Document = document;


                    // Create the PDF document
                    pdfRenderer.RenderDocument();

                    // Save the PDF document...
                    string filename = "PatientsDetail.pdf";
                    //#if DEBUG
                    //                    // I don't want to close the document constantly...
                    //                    filename = "Invoice-" + Guid.NewGuid().ToString("N").ToUpper() + ".pdf";
                    //#endif

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
            //GeneratePDf();
            NewPDF();
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

        protected void ExportToImage(object sender, EventArgs e)
        {
            string base64 = Request.Form[hfImageData.UniqueID].Split(',')[1];
            byte[] bytes = Convert.FromBase64String(base64);
            Response.Clear();
            Response.ContentType = "image/png";
            Response.AddHeader("Content-Disposition", "attachment; filename=GridView.png");
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
        }

    }
    //public class MyFontResolver : IFontResolver
    //{
    //    public FontResolverInfo ResolveTypeface(string familyName,
    //                                            bool isBold,
    //                                            bool isItalic)
    //    {
    //        // Ignore case of font names.
    //        var name = familyName.ToLower();

    //        // Add fonts here
    //        switch (name)
    //        {
    //            case "arial unicode ms":
    //                return new FontResolverInfo("ArialUnicodeMS#");
    //        }

    //        //Return a default font if the font couldn't be found
    //        //this is not a unicode font 
    //        return PlatformFontResolver.ResolveTypeface("Arial", isBold, isItalic);
    //    }

    //    // Return the font data for the fonts.
    //    public byte[] GetFont(string faceName)
    //    {
    //        switch (faceName)
    //        {
    //            case "ArialUnicodeMS#": return FontHelper.ArialUnicodeMS; break;
    //        }

    //        return null;
    //    }
    //}
    //public static class FontHelper
    //{
    //    public static byte[] ArialUnicodeMS
    //    {
    //        //the font is in the folder "/fonts" in the project
    //        get { return LoadFontData("MyApp.fonts.ARIALUNI.TTF"); }
    //    }

    //    /// Returns the specified font from an embedded resource.
    //    static byte[] LoadFontData(string name)
    //    {

    //        var assembly = Assembly.GetExecutingAssembly();

    //        using (Stream stream = assembly.GetManifestResourceStream(name))
    //        {
    //            if (stream == null)
    //                throw new ArgumentException("No resource with name " + name);

    //            int count = (int)stream.Length;
    //            byte[] data = new byte[count];
    //            stream.Read(data, 0, count);
    //            return data;
    //        }
    //    }
    //}


  
}