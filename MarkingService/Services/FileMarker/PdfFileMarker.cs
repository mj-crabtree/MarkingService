using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MarkingService.Entities;

namespace MarkingService.Services.FileMarker;

public class PdfFileMarker : IFileMarker
{
    private readonly IFileSystemService _fileSystemService;
    private const string FontName = StandardFonts.HELVETICA;
    private const int FontSize = 11;
    private const string OutputFilePath = "";

    public PdfFileMarker(IFileSystemService fileSystemService)
    {
        _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
    }

    public string HandlerFormat => "application/pdf";

    public MarkedFile Mark(UnmarkedFile unmarkedFile)
    {
        var wrappedPdf = new WrappedPdf(
            unmarkedFile.Path,
            OutputFilePath,
            unmarkedFile.ClassificationTier);
        
        ApplyVisualMarking(wrappedPdf);
        // ApplyMetadataMarking(wrappedPdf);
        throw new NotImplementedException();
    }

    private void ApplyVisualMarking(WrappedPdf wrappedPdf)
    {
        MarkHeaderSection(wrappedPdf);
        MarkFooterSection(wrappedPdf);
    }

    private void ApplyMetadataMarking(WrappedPdf wrappedPdf)
    {
        throw new NotImplementedException();
    }
    
    private void MarkSection(WrappedPdf wrappedPdf, Func<Rectangle, float> positionCalculator, VerticalAlignment alignment)
    {
        var pdf = wrappedPdf.Pdf;
        var doc = wrappedPdf.Document;
        var copyText = wrappedPdf.ClassificationTier.GetCopyText();
        var header = new Paragraph(copyText)
            .SetFont(PdfFontFactory.CreateFont(FontName))
            .SetFontSize(FontSize);

        for (var i = 1; i <= pdf.GetNumberOfPages(); i++)
        {
            var page = pdf.GetPage(i).GetPageSize();
            var x = page.GetWidth() / 2;
            var y = positionCalculator(page);
            doc.ShowTextAligned(header, x, y, i, TextAlignment.LEFT, alignment, 0);
        }
    }
    
    private void MarkHeaderSection(WrappedPdf pdf) => MarkSection(pdf, page => page.GetTop() - 20, VerticalAlignment.BOTTOM);

    private void MarkFooterSection(WrappedPdf pdf) => MarkSection(pdf, page => page.GetBottom() + 20, VerticalAlignment.TOP);

    private class WrappedPdf
    {
        public ClassificationTier ClassificationTier { get; }
        public PdfDocument Pdf { get; }
        public Document Document { get; set; }
        public WrappedPdf(string readerPath, string writerPath, ClassificationTier classificationTier)
        {
            ClassificationTier = classificationTier;
            Pdf = new PdfDocument(
                new PdfReader(readerPath),
                new PdfWriter(writerPath));
            Document = new Document(Pdf);
        }
    }
}