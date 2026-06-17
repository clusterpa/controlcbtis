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
                    page.Margin(30);

                    page.Content().Column(col =>
                    {
                        col.Item().Text("CBTIS 224")
                            .FontSize(22)
                            .Bold();

                        col.Item().Text("PASE DE SALIDA")
                            .FontSize(18)
                            .Bold();

                        col.Item().PaddingTop(20);

                        col.Item().Text($"Docente: {pase.NombreDocente}");
                        col.Item().Text($"Fecha: {pase.Fecha.ToShortDateString()}");
                        col.Item().Text($"Hora de salida: {pase.HoraSalida}");
                        col.Item().Text($"Hora de regreso: {pase.HoraRegreso}");

                        col.Item().PaddingTop(10);

                        col.Item().Text("Motivo:");
                        col.Item().Text(pase.Motivo);

                        col.Item().PaddingTop(10);

                        col.Item().Text("Observaciones:");
                        col.Item().Text(pase.Observaciones);

                        col.Item().PaddingTop(40);

                        col.Item().Text("____________________________");
                        col.Item().Text("Firma del Docente");

                        col.Item().PaddingTop(30);

                        col.Item().Text("____________________________");
                        col.Item().Text("Firma del Responsable");
                    });
                });
            }).GeneratePdf();
        }
    }
}
