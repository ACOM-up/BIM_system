using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

namespace VillageHut_BIM_System.Helper
{
    class InvoicePdfGenerator
    {
        public bool generateReceipt(PaymentDetails recievedPaymentDetails, List<VillageHut_ServerApplication.Cart> lstReceivedCart, VillageHut_ServerApplication.Customer recievedCustomer, VillageHut_ServerApplication.Employee recievedEmployee)
        {
            //Cart item count
            int itemCount = 0;

            //document initialization
            var writer = new PdfWriter("..\\..\\..\\..\\..\\PDFs\\Receipts\\" + recievedPaymentDetails.transId + ".pdf");
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

            //Seperator
            Paragraph seperator = new Paragraph("______________________________________________________________________________");



            //header
            var headerTable = new Table(new float[] { 1, 1 });
            headerTable.SetWidth(UnitValue.CreatePercentValue(100));

            headerTable.AddHeaderCell(new Cell()
                .Add(new Paragraph("VILLAGE HUT CATERS")
                .SetBackgroundColor(iceBlue)
                .SetFont(bold))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(18)
                .SetFontColor(white)
                .SetMaxWidth(100)
                );

            headerTable.AddHeaderCell(new Cell()
                .Add(new Paragraph("INVOICE")
                .SetFont(bold))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFontSize(18)
                .SetFontColor(lightRed)
                );

            headerTable.AddCell(new Cell()
                .Add(new Paragraph("NO 46,")
                .SetFixedLeading(12)
                .SetFont(font))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(11)
                );

            headerTable.AddCell(new Cell()
                .Add(new Paragraph("Transaction: " + recievedPaymentDetails.transId)
                .SetFixedLeading(12)
                .SetFont(font))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFontSize(11)
                );

            headerTable.AddCell(new Cell()
                .Add(new Paragraph("Mango Grove,").SetFixedLeading(12)
                .SetFont(font))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(11)
                );

            headerTable.AddCell(new Cell()
                .Add(new Paragraph("Customer Id: " + recievedCustomer.cusId)
                .SetFixedLeading(12)
                .SetFont(font))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFontSize(11)
                );

            headerTable.AddCell(new Cell()
                .Add(new Paragraph("Kadawatha,")
                .SetFixedLeading(12)
                .SetFont(font))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(11)
                );

            headerTable.AddCell(new Cell()
                .Add(new Paragraph("Name: " + recievedCustomer.cusName)
                .SetFixedLeading(12)
                .SetFont(font))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFontSize(11)
                );

            headerTable.AddCell(new Cell()
                .Add(new Paragraph("Gampaha,")
                .SetFixedLeading(12)
                .SetFont(font))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(11)
                );

            headerTable.AddCell(new Cell()
                .Add(new Paragraph("NIC: " + recievedCustomer.cusNIC)
                .SetFixedLeading(12)
                .SetFont(font))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFontSize(11)
                );

            headerTable.AddCell(new Cell()
                .Add(new Paragraph("071 456 6911")
                .SetFixedLeading(12)
                .SetFont(font))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(11)
                );

            headerTable.AddCell(new Cell()
                .Add(new Paragraph("Date: " + DateTime.Now.ToString("dd-MM-yyyy   HH:mm:ss")).SetFixedLeading(12)
                .SetFont(font))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFontSize(11)
                );

            document.Add(headerTable);



            document.Add(newLine);
            document.Add(newLine);


            //Table Cart
            var cartTable = new Table(new float[] { 2, 3, 3, 1, 1, 3 });
            cartTable.SetWidth(UnitValue.CreatePercentValue(100));


            //Table headers
            cartTable.AddHeaderCell(new Cell()
                .Add(new Paragraph("Service Id")
                .SetFont(font))
                //.SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(11)
                .SetBackgroundColor(iceBlue)
                .SetFontColor(white)
                );

            cartTable.AddHeaderCell(new Cell()
                .Add(new Paragraph("Service Name")
                .SetFont(font))
                //.SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(11)
                .SetBackgroundColor(iceBlue)
                .SetFontColor(white)
                );

            cartTable.AddHeaderCell(new Cell()
                .Add(new Paragraph("Reserved Date")
                .SetFont(font))
                //.SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(11)
                .SetBackgroundColor(iceBlue)
                .SetFontColor(white)
                );

            cartTable.AddHeaderCell(new Cell()
                .Add(new Paragraph("No Days")
                .SetFont(font))
                //.SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(11)
                .SetBackgroundColor(iceBlue)
                .SetFontColor(white)
                );

            cartTable.AddHeaderCell(new Cell()
                .Add(new Paragraph("QTY")
                .SetFont(font))
                //.SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(11)
                .SetBackgroundColor(iceBlue)
                .SetFontColor(white)
                );

            cartTable.AddHeaderCell(new Cell()
                .Add(new Paragraph("Price Per Item")
                .SetFont(font))
                //.SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(11)
                .SetBackgroundColor(iceBlue)
                .SetFontColor(white)
                );


            foreach (var item in lstReceivedCart) {
                //Table boby

                cartTable.AddCell(new Cell()
                .Add(new Paragraph(item.serId)
                .SetFont(font))
                //.SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(11)
                .SetBackgroundColor(white)
                .SetFontColor(black)
                );

                cartTable.AddCell(new Cell()
                .Add(new Paragraph(item.serName)
                .SetFont(font))
                //.SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(11)
                .SetBackgroundColor(white)
                .SetFontColor(black)
                );

                cartTable.AddCell(new Cell()
                .Add(new Paragraph(item.reservedDate + "")
                .SetFont(font))
                //.SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(11)
                .SetBackgroundColor(white)
                .SetFontColor(black)
                );

                cartTable.AddCell(new Cell()
                .Add(new Paragraph(item.noOfDays + "")
                .SetFont(font))
                //.SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(11)
                .SetBackgroundColor(white)
                .SetFontColor(black)
                );

                cartTable.AddCell(new Cell()
                .Add(new Paragraph(item.itemQty + "")
                .SetFont(font))
                //.SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(11)
                .SetBackgroundColor(white)
                .SetFontColor(black)
                );

                cartTable.AddCell(new Cell()
                .Add(new Paragraph("LKR " + item.itemTotalPrice.ToString("#,##0") + " .00")
                .SetFont(font))
                //.SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(11)
                .SetBackgroundColor(white)
                .SetFontColor(black)
                );

                itemCount++;
            }

            document.Add(cartTable);

            document.Add(newLine);
            document.Add(newLine);
            document.Add(newLine);
            document.Add(newLine);

            //Table paymentDetails
            var PaymentDetialsTable = new Table(new float[] { 1, 1, 1 }).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.RIGHT);


            string cartTablePaymentCat = "";
            string cartTablePaymentDetail = "";

            for (int i = 0; i < 5; i++) {

                switch (i) {
                    case 0:
                        cartTablePaymentCat = "SUBTOTAL";
                        cartTablePaymentDetail = "LKR " + (recievedPaymentDetails.totalPrice + recievedPaymentDetails.discount).ToString("#,##0") + " .00";
                        break;
                    case 1:
                        cartTablePaymentCat = "DISCOUNT";
                        cartTablePaymentDetail = "LKR " + (recievedPaymentDetails.discount).ToString("#,##0") + " .00";
                        break;
                    case 2:
                        cartTablePaymentCat = "TOTAL";
                        cartTablePaymentDetail = "LKR " + (recievedPaymentDetails.totalPrice).ToString("#,##0") + " .00";
                        break;
                    case 3:
                        cartTablePaymentCat = "AMOUNT PAYED";
                        cartTablePaymentDetail = "LKR " + (recievedPaymentDetails.pricePayed).ToString("#,##0") + " .00";
                        break;
                    case 4:
                        cartTablePaymentCat = "BALANCE";
                        cartTablePaymentDetail = "LKR " + (recievedPaymentDetails.balance).ToString("#,##0") + " .00";
                        break;
                    default:
                        break;
                }

                PaymentDetialsTable.AddCell(new Cell()
                .Add(new Paragraph(cartTablePaymentCat)
                .SetFont(font))
                //.SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(10)
                .SetBackgroundColor(white)
                .SetFontColor(black)
                .SetMaxWidth(100)
                .SetPadding(5)
                );

                PaymentDetialsTable.AddCell(new Cell()
                .Add(new Paragraph("-")
                .SetFont(font))
                //.SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(11)
                .SetBackgroundColor(white)
                .SetFontColor(black)
                .SetMaxWidth(40)
                .SetPadding(5)
                );

                PaymentDetialsTable.AddCell(new Cell()
                .Add(new Paragraph(cartTablePaymentDetail)
                .SetFont(font))
                //.SetBorder(Border.NO_BORDER)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFontSize(10)
                .SetBackgroundColor(white)
                .SetFontColor(black)
                .SetMaxWidth(80)
                .SetPadding(5)
                );
            }

            document.Add(PaymentDetialsTable);



            //Adding Padding
            var paddingTable = new Table(new float[] { 2, 3, 3, 1, 1, 3 });
            paddingTable.SetWidth(UnitValue.CreatePercentValue(100));


            if (itemCount < 72) {
                for (int i = 0; i <= 72 - (itemCount*6)-1; i++) {
                    paddingTable.AddCell(new Cell()
                    .Add(new Paragraph("A")
                    .SetFont(font))
                    .SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(11)
                    .SetBackgroundColor(white)
                    .SetFontColor(white)
                    );
                }
            }

            document.Add(paddingTable);
            





            //Table footer

            document.Add(newLine);
            document.Add(newLine);
            document.Add(newLine);
            document.Add(newLine);


            document.Add(seperator);
            document.Add(newLine);
            document.Add(newLine);


            var footerTable = new Table(new float[] { 1, 1 });
            footerTable.SetWidth(UnitValue.CreatePercentValue(100));


            footerTable.AddCell(new Cell()
            .Add(new Paragraph("Issued by: "+recievedEmployee.emName)
            .SetFont(font))
            .SetBorder(Border.NO_BORDER)
            .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
            .SetFontSize(11)
            );


            footerTable.AddCell(new Cell()
            .Add(new Paragraph("Customer: "+recievedCustomer.cusName)
            .SetFont(font))
            .SetBorder(Border.NO_BORDER)
            .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
            .SetFontSize(11)
            );


            footerTable.AddCell(new Cell()
            .Add(new Paragraph("Signature:  _____________________________________")
            .SetFont(font))
            .SetBorder(Border.NO_BORDER)
            .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
            .SetFontSize(11)
            );


            footerTable.AddCell(new Cell()
            .Add(new Paragraph("Signature:  _____________________________________")
            .SetFont(font))
            .SetBorder(Border.NO_BORDER)
            .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
            .SetFontSize(11)
            );


            document.Add(footerTable);


            document.Add(newLine);
            document.Add(newLine);
            document.Add(newLine);
            document.Add(newLine);


            var greetingTable = new Table(new float[] { 1});
            greetingTable.SetWidth(UnitValue.CreatePercentValue(100));

            greetingTable.AddCell(new Cell()
            .Add(new Paragraph("Thank you for your business...!   VILLAGE HUT CATERS")
            .SetFont(font))
            .SetBorder(Border.NO_BORDER)
            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
            .SetFontSize(11)
            .SetFontColor(white)
            .SetPadding(7)
            .SetBackgroundColor(iceBlue)
            );


            document.Add(greetingTable);

            document.Close();

            return true;
        }
    }
}
