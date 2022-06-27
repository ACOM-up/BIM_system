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
    class OverDPdfGenerator
    {
        public bool generateReport(List<VillageHut_BIM_System.VillageHut_ServerApplication.OverdueItem> lstOverDItems)
        {
            try
            {
                var writer = new PdfWriter("..\\..\\..\\..\\..\\PDFs\\OverDue Items\\OverDueItems Report.pdf");
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

                Paragraph newline = new Paragraph("");

                Paragraph seperator = new Paragraph("________________________________________________________________________________");

                document.Add(new Paragraph("VILLAGE HUT CATERS")
                    .SetFont(bold)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(18)
                    .SetFontColor(iceBlue));

                document.Add(new Paragraph("Overdue items")
                    .SetFont(bold)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFontSize(18)
                    .SetFontColor(red));

                document.Add(new Paragraph("Date: " + DateTime.Now.ToString("dd/MM/yyyy   HH:mm"))
                    .SetFont(font)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetFontSize(11));

                document.Add(newline);
                document.Add(newline);
                document.Add(newline);

                var viewTable = new Table(new float[] { 1, 1, 1, 1, 1, 1, 1 });
                viewTable.SetWidth(UnitValue.CreatePercentValue(100));

                viewTable.AddCell(new Cell()
                    .Add(new Paragraph("Service ID")
                    .SetFont(bold)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(12)));

                viewTable.AddCell(new Cell()
                   .Add(new Paragraph("Transaction ID")
                   .SetFont(bold)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));


                viewTable.AddCell(new Cell()
                   .Add(new Paragraph("Service Name")
                   .SetFont(bold)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));

                viewTable.AddCell(new Cell()
                   .Add(new Paragraph("Due Date")
                   .SetFont(bold)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));


                viewTable.AddCell(new Cell()
                   .Add(new Paragraph("Quantity")
                   .SetFont(bold)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));


                viewTable.AddCell(new Cell()
                   .Add(new Paragraph("Customer Name")
                   .SetFont(bold)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));


                viewTable.AddCell(new Cell()
                   .Add(new Paragraph("Customer NIC")
                   .SetFont(bold)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));

                foreach (var item in lstOverDItems)
                {
                    viewTable.AddCell(new Cell()
                   .Add(new Paragraph(item.serId)
                   .SetFont(font)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));

                    viewTable.AddCell(new Cell()
                   .Add(new Paragraph(item.transId)
                   .SetFont(font)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));

                    viewTable.AddCell(new Cell()
                   .Add(new Paragraph(item.serName)
                   .SetFont(font)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));

                    viewTable.AddCell(new Cell()
                   .Add(new Paragraph(item.dueDate.ToString())
                   .SetFont(font)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));

                    viewTable.AddCell(new Cell()
                   .Add(new Paragraph(item.qty.ToString())
                   .SetFont(font)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));

                    viewTable.AddCell(new Cell()
                   .Add(new Paragraph(item.cusName)
                   .SetFont(font)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));

                    viewTable.AddCell(new Cell()
                   .Add(new Paragraph(item.cusNIC)
                   .SetFont(font)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12)));
                }

                document.Add(viewTable);
                document.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
