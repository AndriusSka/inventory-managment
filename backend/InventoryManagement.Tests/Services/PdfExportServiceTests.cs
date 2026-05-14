using FluentAssertions;
using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Enums;
using InventoryManagement.Infrastructure.Repositories;
using InventoryManagement.Infrastructure.Services;
using InventoryManagement.Tests.TestHelpers;
using QuestPDF.Infrastructure;
using Xunit;

namespace InventoryManagement.Tests.Services;

public class PdfExportServiceTests
{
    public PdfExportServiceTests()
    {
        // QuestPDF licencija 
        QuestPDF.Settings.License = LicenseType.Community;
    }
    
    // Su Theory pabandžiau paleisti tą patį testą su skirtingais šablonais
    [Theory]
    [InlineData(PdfTemplateType.Table)]
    [InlineData(PdfTemplateType.GroupedByUser)]
    public async Task ExportAsync_GeneratesNonEmptyPdf(PdfTemplateType template)
    {
        using var context = TestDbContextFactory.Create();
        var (jonas, _) = TestDbContextFactory.SeedUsers(context);
        TestDbContextFactory.SeedItems(context, jonas);
        var repository = new InventoryItemRepository(context);
        var itemService = new InventoryItemService(repository);
        var exportService = new PdfExportService(itemService);

        var pdfBytes = await exportService.ExportAsync(
            new InventoryItemFilterDto(), template);

        pdfBytes.Should().NotBeNull();
        pdfBytes.Should().NotBeEmpty();
        var header = System.Text.Encoding.ASCII.GetString(pdfBytes.Take(5).ToArray());
        header.Should().Be("%PDF-");
    }

    [Fact]
    public async Task ExportAsync_UnknownTemplate_Throws()
    {
        using var context = TestDbContextFactory.Create();
        var repository = new InventoryItemRepository(context);
        var itemService = new InventoryItemService(repository);
        var exportService = new PdfExportService(itemService);

        var act = async () => await exportService.ExportAsync(
            new InventoryItemFilterDto(),
            (PdfTemplateType)999);

        await act.Should().ThrowAsync<ArgumentException>();
    }
}