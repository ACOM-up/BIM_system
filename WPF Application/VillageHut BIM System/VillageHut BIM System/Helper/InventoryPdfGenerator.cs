using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Pdfa;
using iText.Kernel.Geom;
using iText.Kernel.Font;
using iText.IO.Font;
using iText.Layout.Properties;
using iText.Layout.Borders;
using iText.IO.Util;
using iText.Forms;
using System.IO;
using iText.Kernel.Colors;
using iText.Layout.Font;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Events;
using VillageHut_BIM_System.VillageHut_ServerApplication;


namespace VillageHut_BIM_System.Helper
{
    class InventoryPdfGenerator
    {
        public bool generateReport(List<VillageHut_ServerApplication.Service> recievedItems) {
            try
            {

                //document initialization
                var writer = new PdfWriter("..\\..\\..\\..\\..\\PDFs\\Inventory\\Inventory.pdf");
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf, PageSize.A4);

                //fonts
                var font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);
                var bold = PdfFontFactory.CreateFont(FontConstants.TIMES_BOLD);


                //Colors
                Color red = WebColors.GetRGBColor("#A00000");
                Color offWhite = WebColors.GetRGBColor("#EEEEEE");
                Color white = WebColors.GetRGBColor("#FFFFFF");
                Color lightRed = WebColors.GetRGBColor("#EA554E");
                Color lightBlue = WebColors.GetRGBColor("#2096B8");
                Color iceBlue = WebColors.GetRGBColor("#5979AD");
                Color black = WebColors.GetRGBColor("#000000");


                //New Line
                Paragraph newLine = new Paragraph("");

                document.Add(new Paragraph("VILLAGE HUT CATERS")
                   .SetFont(bold)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(18)
                   .SetFontColor(iceBlue));

                document.Add(new Paragraph("Inventory")
                    .SetFont(bold)
                    .SetFontColor(lightRed)
                    .SetFontSize(18));

                document.Add(new Paragraph("Date: " + DateTime.Now.ToString("dd/MM/yyyy   HH:mm"))
                    .SetFont(font)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetFontSize(11));

                document.Add(newLine);
                document.Add(newLine);
                document.Add(newLine);

                var viewTable = new Table(new float[] { 1, 1, 1, 1});
                viewTable.SetWidth(UnitValue.CreatePercentValue(100));

                viewTable.AddCell(new Cell()
                  .Add(new Paragraph("Service ID")
                  .SetFont(bold)
                  .SetTextAlignment(TextAlignment.CENTER)
                  .SetFontSize(12)));

                viewTable.AddCell(new Cell()
                   .Add(new Paragraph("Service Name")
                   .SetFont(bold)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));


                viewTable.AddCell(new Cell()
                   .Add(new Paragraph("Catogery")
                   .SetFont(bold)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));

                viewTable.AddCell(new Cell()
                   .Add(new Paragraph("Service / Items")
                   .SetFont(bold)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));



                foreach (var item in recievedItems)
                {

                viewTable.AddCell(new Cell()
                  .Add(new Paragraph(item.serId)
                  .SetFont(font)
                  .SetTextAlignment(TextAlignment.CENTER)
                  .SetFontSize(12)));

                viewTable.AddCell(new Cell()
                   .Add(new Paragraph(item.serName)
                   .SetFont(font)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));


                viewTable.AddCell(new Cell()
                   .Add(new Paragraph(item.catName)
                   .SetFont(font)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));

                viewTable.AddCell(new Cell()
                   .Add(new Paragraph(item.serIsItem)
                   .SetFont(font)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));
                }




                document.Add(viewTable);
                
                
                
                
                
                document.Close();



                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
