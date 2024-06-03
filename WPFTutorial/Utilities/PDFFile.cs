using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangLang.Utilities
{
    public class PDFFile
    {
        public bool Create(string filename, string content)
        {
            try
            {
                var file = File.Create(filename);
                var writer = new PdfWriter(file);
                var pdfDocument = new PdfDocument(writer.SetSmartMode(true));

                Document d = new Document(pdfDocument, iText.Kernel.Geom.PageSize.LETTER);
                d.Add(new Paragraph(content));
                d.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
