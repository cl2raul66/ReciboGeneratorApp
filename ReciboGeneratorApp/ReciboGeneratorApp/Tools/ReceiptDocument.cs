using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ReceiptGeneratorApp.Models;
using Colors = QuestPDF.Helpers.Colors;
using IContainer = QuestPDF.Infrastructure.IContainer;

namespace AgroGestor360.Client.Tools.ReportsTemplate;

public class ReceiptDocument : IDocument
{
    readonly Receipt data;

    public ReceiptDocument(Receipt entity)
    {
        data = entity;
    }

    public DocumentMetadata GetMetaData() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(25);
                page.Size(PageSizes.Letter);
                page.DefaultTextStyle(x => x.FontSize(9).FontColor(Colors.Black));

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);

                page.Footer().MaxHeight(30).Element(ComposeFooter);
            });
    }

    private void ComposeHeader(IContainer container)
    {
        container.Column(c =>
        {
            c.Spacing(15);
            c.Item().AlignCenter().Text("RECIBO").FontSize(11).SemiBold();
            c.Item().AlignLeft().Row(row =>
            {
                row.ConstantItem(52).Column(c =>
                {
                    c.Item().PaddingHorizontal(5).Image(data.WorkshopDetails!.PathLogo!).FitArea().WithCompressionQuality(ImageCompressionQuality.High);
                });

                row.RelativeItem().Column(c1 =>
                {
                    c1.Item().Text(text =>
                    {
                        text.Span("TALLER: ").SemiBold();
                        text.Span($"{data.WorkshopDetails!.Name}");
                    });
                    c1.Item().Text(text =>
                    {
                        text.Span("TELÉFONO: ").SemiBold();
                        text.Span($"{data.WorkshopDetails!.Phone}");
                    });
                    c1.Item().Text(text =>
                    {
                        text.Span("CORREO ELECTRÓNICO: ").SemiBold();
                        text.Span($"{data.WorkshopDetails!.Email}");
                    });
                    c1.Item().Text(text =>
                    {
                        text.Span("DIRECCIÓN: ").SemiBold();
                        text.Span($"{data.WorkshopDetails!.Address}");
                    });
                });

                row.ConstantItem(100).Column(c2 =>
                {
                    c2.Item().AlignLeft().Text(text =>
                    {
                        text.Span("EMISIÓN: ").SemiBold();
                        text.Span($"{data.ReceiptDetails!.IssueDate:dd MMM yyyy}");
                    });
                    c2.Item().AlignLeft().Text(text =>
                    {
                        text.Span("EXPEDIENTE: ").SemiBold();
                        text.Span($"{data.ReceiptDetails!.FileNumber:00}");
                    });
                    c2.Item().AlignLeft().Text(text =>
                    {
                        text.Span("IMPORTE: ").SemiBold();
                        text.Span($"{data.ReceiptDetails!.TotalPrice:C}");
                    });
                });
            });
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.Column(c =>
        {
            c.Item().MaxHeight(10);
            c.Item().AlignCenter().Text("CLIENTE").FontSize(11).SemiBold();
            c.Item().PaddingTop(2).Text(text =>
            {
                text.Span("NOMBRE: ").SemiBold();
                text.Span($"{data.ClientDetails!.Name}");
            });
            c.Item().Text(text =>
            {
                text.Span("TELÉFONO: ").SemiBold();
                text.Span($"{data.ClientDetails!.Phone}");
            });
            c.Item().Text(text =>
            {
                text.Span("VEHÍCULO: ").SemiBold();
                text.Span($"{data.ClientDetails!.Vehicle}");
            });
            c.Item().MaxHeight(10);
            c.Item().AlignCenter().Text("DESCRIPCIÓN").FontSize(11).SemiBold();
            c.Item().PaddingTop(2).Element(ComposeTable);
        });
    }

    private void ComposeTable(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(c =>
            {
                c.RelativeColumn();
                c.ConstantColumn(100);
            });

            foreach (var item in data.ServiceDetails!)
            {
                table.Cell().Element(CellStyle).ExtendHorizontal().AlignLeft().Text(item.Description);
                table.Cell().Element(CellStyle).AlignRight().Text(item.Price?.ToString("N2") ?? "0.00");
            }

            table.Cell().Element(CellStyle).ExtendHorizontal().AlignLeft().Text("TOTAL").FontSize(11).SemiBold();
            table.Cell().Element(CellStyle).AlignRight().Text(data.ReceiptDetails?.TotalPrice?.ToString("N2") ?? "0.00").FontSize(11).SemiBold();

            static IContainer CellStyle(IContainer container)
            {
                return container.AlignMiddle().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(4);
            }
        });
    }

    private void ComposeFooter(IContainer container)
    {
        container.Row(row =>
        {
            row.ConstantItem(150)
                .ExtendVertical()
                .Text(t =>
                {
                    t.Span("Página ");
                    t.CurrentPageNumber();
                    t.Span(" de ");
                    t.TotalPages();
                });
        });
    }
}
