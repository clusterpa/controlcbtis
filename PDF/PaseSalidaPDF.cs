using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using controlcbtis.Models;
using System.IO;

namespace controlcbtis.PDF
{
    public class PaseSalidaPDF
    {
        public static byte[] Generar(PaseSalida pase)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var rutaLogo = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                "encabezado.jpeg");

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);

                    page.Content().Column(col =>
                    {
                        // ENCABEZADO
                        if (File.Exists(rutaLogo))
                        {
                            col.Item()
                               .Image(rutaLogo)
                               .FitWidth();
                        }

                        col.Item().PaddingTop(10);

                        col.Item()
                            .AlignCenter()
                            .Text("PASE DE SALIDA")
                            .Bold()
                            .FontSize(20);

                        col.Item().PaddingTop(20);

                        // DATOS

                        col.Item().Text(text =>
                        {
                            text.Span("Nombre del docente: ").Bold();
                            text.Span(pase.NombreDocente);
                        });

                        col.Item().PaddingTop(8);

                        col.Item().Text(text =>
                        {
                            text.Span("Fecha: ").Bold();
                            text.Span(pase.Fecha.ToString("dd/MM/yyyy"));
                        });

                        col.Item().PaddingTop(12);

                        col.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            tabla.Cell().Text(text =>
                            {
                                text.Span("Hora de salida: ").Bold();
                                text.Span(pase.HoraSalida);
                            });

                            tabla.Cell().AlignRight().Text(text =>
                            {
                                text.Span("Hora de regreso: ").Bold();
                                text.Span(pase.HoraRegreso);
                            });
                        });

                        col.Item().PaddingTop(20);

                        // ASUNTO

                        col.Item().Text("Asunto:")
                            .Bold();

                        col.Item()
                            .Border(1)
                            .Padding(10)
                            .Height(90)
                            .Text(pase.Asunto);

                        col.Item().PaddingTop(45);

                        // FIRMAS

                        col.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.ConstantColumn(60);
                                columns.RelativeColumn();
                            });

                            tabla.Cell().Column(c =>
                            {
                                c.Item().LineHorizontal(1);

                                c.Item()
                                    .AlignCenter()
                                    .Text("Vo. Bo. Jefe de Departamento")
                                    .FontSize(10);
                            });

                            tabla.Cell();

                            tabla.Cell().Column(c =>
                            {
                                c.Item().LineHorizontal(1);

                                c.Item()
                                    .AlignCenter()
                                    .Text("Autoriza salida")
                                    .FontSize(10);
                            });
                        });

                        col.Item().PaddingTop(45);

                        col.Item().AlignCenter().Column(c =>
                        {
                            c.Item()
                                .Width(220)
                                .LineHorizontal(1);

                            c.Item()
                                .AlignCenter()
                                .Text("Firma del Docente")
                                .FontSize(10);
                        });
                    });
                });
            }).GeneratePdf();
        }
    }
}