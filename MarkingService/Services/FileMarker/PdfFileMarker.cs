using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MarkingService.Entities;
using MarkingService.Options;
using Microsoft.Extensions.Options;
using Path = System.IO.Path;

namespace MarkingService.Services.FileMarker;

public class PdfFileMarker : IFileMarker
{
    private const string FontName = StandardFonts.HELVETICA;
    private const int FontSize = 11;
    private readonly string _outputFilePath;
    private readonly IFileSystemService _fileSystemService;

    public PdfFileMarker(IFileSystemService fileSystemService, IOptions<FilePathOptions> options)
    {
        _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
        _outputFilePath = options.Value.Processed;
    }

    public string HandlerFormat => ".pdf";

    public MarkedFile Mark(UnmarkedFile unmarkedFile)
    {
        var fileName = Path.GetFileName(unmarkedFile.Path);
        var wrappedPdf = new WrappedPdf(
            unmarkedFile.Path,
            Path.Combine(_outputFilePath, fileName),
            unmarkedFile.ClassificationTier);

        ApplyVisualMarking(wrappedPdf);
        // ApplyMetadataMarking(wrappedPdf);
        wrappedPdf.Document.Close();
        return new MarkedFile();
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

    private void MarkSection(WrappedPdf wrappedPdf, Func<Rectangle, float> positionCalculator,
        VerticalAlignment alignment)
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

    private void MarkHeaderSection(WrappedPdf pdf)
    {
        MarkSection(pdf, page => page.GetTop() - 20, VerticalAlignment.BOTTOM);
    }

    private void MarkFooterSection(WrappedPdf pdf)
    {
        MarkSection(pdf, page => page.GetBottom() + 20, VerticalAlignment.TOP);
    }

    private class WrappedPdf
    {
        public WrappedPdf(string source, string dest, ClassificationTier classificationTier)
        {
            ClassificationTier = classificationTier;
            Pdf = new PdfDocument(
                new PdfReader(source),
                new PdfWriter(dest));
            Document = new Document(Pdf);
        }

        public ClassificationTier ClassificationTier { get; }
        public PdfDocument Pdf { get; }
        public Document Document { get; set; }
    }
}