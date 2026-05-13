using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace InventoryManagement.Infrastructure.Services.PdfTemplates;

public class TableTemplate : IPdfTemplate
{
    public byte[] Generate(IEnumerable<InventoryItemDto> items)
    {
        var itemList = items.ToList();

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(11));

                // Header'is
                page.Header()
                    .PaddingBottom(15)
                    .Column(col =>
                    {
                        col.Item().Text("Inventoriaus ataskaita").FontSize(20).Bold();
                        col.Item().Text($"Sugeneruota: {DateTime.Now:yyyy-MM-dd HH:mm}")
                            .FontSize(10).FontColor(Colors.Grey.Darken1);
                        col.Item().Text($"Iš viso įrašų: {itemList.Count}")
                            .FontSize(10).FontColor(Colors.Grey.Darken1);
                    });

                // Table turinys
                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(2);  // Tipas
                        columns.RelativeColumn(3);  // Komentaras
                        columns.RelativeColumn(3);  // Vartotojas
                        columns.RelativeColumn(2);  // Data
                    });

                    // Table antraštė
                    table.Header(header =>
                    {
                        header.Cell().Element(HeaderStyle).Text("Tipas");
                        header.Cell().Element(HeaderStyle).Text("Komentaras");
                        header.Cell().Element(HeaderStyle).Text("Vartotojas");
                        header.Cell().Element(HeaderStyle).Text("Pirkimo data");

                        static IContainer HeaderStyle(IContainer c) =>
                            c.Background(Colors.Grey.Lighten2)
                             .Padding(5)
                             .DefaultTextStyle(x => x.Bold());
                    });

                    // Lentelės eilutės
                    foreach (var item in itemList)
                    {
                        table.Cell().Element(CellStyle).Text(item.Type);
                        table.Cell().Element(CellStyle).Text(item.Comment);
                        table.Cell().Element(CellStyle).Text(item.UserFullName);
                        table.Cell().Element(CellStyle).Text(item.PurchaseDate.ToString("yyyy-MM-dd"));
                    }

                    static IContainer CellStyle(IContainer c) =>
                        c.BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Padding(5);
                });

                // Apačia
                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Puslapis ");
                        text.CurrentPageNumber();
                        text.Span(" / ");
                        text.TotalPages();
                    });
            });
        }).GeneratePdf();
    }
}