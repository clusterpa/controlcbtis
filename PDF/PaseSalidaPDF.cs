using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using controlcbtis.Models;

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
                    page.Margin(35);

                    page.Content().Column(col =>
                    {
                        col.Item().AlignCenter().Text("CENTRO DE BACHILLERATO TECNOLÓGICO")
                            .Bold().FontSize(16);

                        col.Item().AlignCenter().Text("industrial y de servicios No. 224")
                            .FontSize(13);

                        col.Item().PaddingTop(8);

                        col.Item().AlignCenter().Text("PASE DE SALIDA")
                            .Bold()
                            .FontSize(20);

                        col.Item().PaddingTop(25);



                        col.Item().Text($"Nombre del docente: {pase.NombreDocente}");

                        col.Item().PaddingTop(10);


                        col.Item().Text($"Fecha: {pase.Fecha:dd/MM/yyyy}");

                        col.Item().PaddingTop(10);

                        col.Item().Row(row =>
                        {
                            row.RelativeItem()
                                .Text($"Hora de salida: {pase.HoraSalida}");

                            row.RelativeItem()
                                .Text($"Hora de regreso: {pase.HoraRegreso}");
                        });

                        col.Item().PaddingTop(20);

                        col.Item().Text("Asunto:")
                            .Bold();

                        col.Item().Border(1)
                            .Padding(10)
                            .Text(pase.Asunto);

                        col.Item().PaddingTop(35);

                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().Text("________________________");
                                c.Item().AlignCenter().Text("Vo. Bo. Jefe de Departamento");
                            });

                            row.RelativeItem().Column(c =>
                            {
                                c.Item().Text("________________________");
                                c.Item().AlignCenter().Text("Autoriza salida");
                            });
                        });

                        col.Item().PaddingTop(40);

                        col.Item().AlignCenter().Text("_______________________________");

                        col.Item().AlignCenter().Text("Firma del Docente");
                    });
                });
            }).GeneratePdf();
        }
    }
}