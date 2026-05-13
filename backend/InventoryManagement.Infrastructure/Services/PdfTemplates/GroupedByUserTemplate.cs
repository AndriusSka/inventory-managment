using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace InventoryManagement.Infrastructure.Services.PdfTemplates;

public class GroupedByUserTemplate : IPdfTemplate
{
    public byte[] Generate(IEnumerable<InventoryItemDto> items)
    {
        var grouped = items
            .GroupBy(i => i.UserFullName)
            .OrderBy(g => g.Key)
            .ToList();

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header()
                    .PaddingBottom(15)
                    .Column(col =>
                    {
                        col.Item().Text("Inventorius pagal vartotojus").FontSize(20).Bold();
                        col.Item().Text($"Sugeneruota: {DateTime.Now:yyyy-MM-dd HH:mm}")
                            .FontSize(10).FontColor(Colors.Grey.Darken1);
                    });

                page.Content().Column(column =>
                {
                    column.Spacing(20);

                    foreach (var userGroup in grouped)
                    {
                        column.Item().Column(userBlock =>
                        {
                            // Skyriaus antraštė su vartotojo vardu
                            userBlock.Item()
                                .Background(Colors.Blue.Lighten4)
                                .Padding(8)
                                .Text(userGroup.Key)
                                .FontSize(14).Bold();

                            // To vartotojo item'ai
                            foreach (var item in userGroup)
                            {
                                userBlock.Item()
                                    .BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                                    .Padding(8)
                                    .Row(row =>
                                    {
                                        row.RelativeItem(2).Text(item.Type).Bold();
                                        row.RelativeItem(5).Text(item.Comment);
                                        row.RelativeItem(3)
                                            .AlignRight()
                                            .Text(item.PurchaseDate.ToString("yyyy-MM-dd"))
                                            .FontColor(Colors.Grey.Darken1);
                                    });
                            }

                            // Suvestinė 
                            userBlock.Item()
                                .PaddingTop(5)
                                .AlignRight()
                                .Text($"Iš viso: {userGroup.Count()} įrašai")
                                .FontSize(10).Italic().FontColor(Colors.Grey.Darken2);
                        });
                    }
                });

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