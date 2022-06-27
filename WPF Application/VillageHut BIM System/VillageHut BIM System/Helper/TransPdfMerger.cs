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
    class TransPdfMerger
    {
        public bool mergeReport() {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
