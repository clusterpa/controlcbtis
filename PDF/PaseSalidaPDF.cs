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
                "encabezado.PNG");

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);

                    page.Content().Column(col =>
                    {
                        col.Item().Row(row =>
                        {
                            row.ConstantItem(90).Image(rutaLogo).FitWidth();

                            row.RelativeItem().Column(text =>
                            {
                                text.Item().Text("SECRETARÍA DE EDUCACIÓN PÚBLICA")
                                    .Bold().FontSize(10);

                                text.Item().Text("DIRECCIÓN GENERAL DE EDUCACIÓN TECNOLÓGICA INDUSTRIAL Y DE SERVICIOS")
                                    .FontSize(9);

                                text.Item().Text("CENTRO DE BACHILLERATO TECNOLÓGICO INDUSTRIAL Y DE SERVICIO No. 224")
                                    .Bold().FontSize(9);
                            });
                        });

                        col.Item().PaddingTop(10);

                        col.Item()
                            .Border(1)
                            .Padding(5)
                            .AlignCenter()
                            .Text("PASE DE SALIDA")
                            .Bold()
                            .FontSize(18);

                        col.Item().PaddingTop(15);

                        col.Item().Text(t =>
                        {
                            t.Span("FECHA: ").Bold();
                            t.Span(pase.Fecha.ToString("dd/MM/yyyy"));
                        });

                        col.Item().PaddingTop(10);

                        col.Item().Text(t =>
                        {
                            t.Span("NOMBRE Y FIRMA DE QUIEN SOLICITA: ").Bold();
                            t.Span(pase.NombreDocente);
                        });

                        col.Item().PaddingTop(10);

                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text(t =>
                            {
                                t.Span("HORA DE SALIDA: ").Bold();
                                t.Span(pase.HoraSalida);
                            });

                            row.RelativeItem().AlignRight().Text(t =>
                            {
                                t.Span("HORA DE REGRESO: ").Bold();
                                t.Span(pase.HoraRegreso);
                            });
                        });

                        col.Item().PaddingTop(15);

                        col.Item().Text("ASUNTO:").Bold();

                        col.Item()
                            .Border(1)
                            .Height(80)
                            .Padding(5)
                            .Text(pase.Asunto);

                        col.Item().PaddingTop(25);

                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().LineHorizontal(1);
                                c.Item().AlignCenter().Text("Vo.Bo. Jefe de Departamento").FontSize(9);
                            });

                            row.RelativeItem();

                            row.RelativeItem().Column(c =>
                            {
                                c.Item().LineHorizontal(1);
                                c.Item().AlignCenter().Text("Autoriza Salida Directo").FontSize(9);
                            });
                        });

                        col.Item().PaddingTop(40);

                        col.Item().AlignCenter().Column(c =>
                        {
                            c.Item().Width(200).LineHorizontal(1);
                            c.Item().Text("Firma del Docente").FontSize(9).AlignCenter();
                        });
                    });
                });
            }).GeneratePdf();
        }
    }
}