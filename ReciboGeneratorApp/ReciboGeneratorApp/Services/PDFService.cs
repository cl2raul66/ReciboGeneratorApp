using ReceiptGeneratorApp.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using AgroGestor360.Client.Tools.ReportsTemplate;

namespace ReciboGeneratorApp.Services;

public interface IPDFService
{
    Task<string> GeneratedPDF(Receipt doc);
}

public class PDFService : IPDFService
{
    public PDFService()
    {
        QuestPDF.Settings.EnableCaching = true;
        QuestPDF.Settings.EnableDebugging = true;
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public async Task<string> GeneratedPDF(Receipt entity)
    {
        string fileName = $"Recibo_{entity.ClientDetails!.Name}_{entity.ReceiptDetails!.IssueDate:yyyyMMdd}_{entity.ReceiptDetails!.TotalPrice}.pdf";
        string filePath = Path.Combine(FileSystem.Current.CacheDirectory, fileName);

        IDocument document = new ReceiptDocument(entity!);

        document.GeneratePdf(filePath);

        await Task.CompletedTask;

        return filePath;
    }
}
