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

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Content().Column(col =>
                    {

                        var rutaLogo = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot",
                            "images",
                            "encabezado.jpeg");

                        col.Item()
                            .AlignCenter()
                            .Height(95)
                            .Image(rutaLogo);

                        col.Item().PaddingTop(8);

                        col.Item()
                            .AlignCenter()
                            .Text("PASE DE SALIDA")
                            .Bold()
                            .FontSize(22);

                        col.Item().PaddingTop(25);


                        col.Item().Text(text =>
                        {
                            text.Span("Nombre del docente: ").Bold();
                            text.Span(pase.NombreDocente);
                        });

                        col.Item().PaddingTop(10);

                        col.Item().Text(text =>
                        {
                            text.Span("Fecha: ").Bold();
                            text.Span(pase.Fecha.ToString("dd/MM/yyyy"));
                        });

                        col.Item().PaddingTop(15);

                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text(text =>
                            {
                                text.Span("Hora de salida: ").Bold();
                                text.Span(pase.HoraSalida);
                            });

                            row.RelativeItem().AlignRight().Text(text =>
                            {
                                text.Span("Hora de regreso: ").Bold();
                                text.Span(pase.HoraRegreso);
                            });
                        });

                        col.Item().PaddingTop(25);


                        col.Item().Text("Asunto:")
                            .Bold();

                        col.Item()
                            .Border(1)
                            .MinHeight(100)
                            .Padding(12)
                            .Text(pase.Asunto);

                        col.Item().PaddingTop(55);


                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().LineHorizontal(1);

                                c.Item()
                                    .AlignCenter()
                                    .Text("Vo. Bo. Jefe de Departamento")
                                    .FontSize(10);
                            });

                            row.ConstantItem(80);

                            row.RelativeItem().Column(c =>
                            {
                                c.Item().LineHorizontal(1);

                                c.Item()
                                    .AlignCenter()
                                    .Text("Autoriza salida")
                                    .FontSize(10);
                            });
                        });

                        col.Item().PaddingTop(45);

                        col.Item()
                            .AlignCenter()
                            .Width(230)
                            .LineHorizontal(1);

                        col.Item()
                            .AlignCenter()
                            .Text("Firma del Docente")
                            .FontSize(10);
                    });
                });
            }).GeneratePdf();
        }
    }
}