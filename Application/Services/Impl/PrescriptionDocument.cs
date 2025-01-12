using Application.Models.ResponseDtos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Application.Services.Impl
{
    public class PrescriptionDocument : IDocument
    {
        private readonly PrescriptionResponseDto _prescription;

        public PrescriptionDocument(PrescriptionResponseDto prescription)
        {
            _prescription = prescription;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                // Tamaño de página más compacto (14 cm x 21 cm)
                page.Size(14, 21, Unit.Centimetre);
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontColor(Colors.Black).FontSize(10).LineHeight(1.5f));

                // Encabezado de la clínica
                page.Header()
                    .AlignCenter()
                    .Text("Clínica Oftalmológica Tapia y Especialidades")
                    .SemiBold()
                    .FontSize(14)
                    .FontColor("#007bff");

                // Contenido en párrafos
                page.Content()
                    .Padding(1, Unit.Centimetre)
                    .Column(column =>
                    {
                        column.Spacing(10); // Espaciado entre elementos

                        // Introducción
                        column.Item().Text(text =>
                        {
                            text.Span($"Fecha de emisión: {_prescription.PrescriptionDate:yyyy-MM-dd}. ");
                            text.Span($"Esta receta ha sido emitida por la Clínica Oftalmológica Tapia y Especialidades para el paciente ");
                            text.Span($"{_prescription.Patient.FirstName} {_prescription.Patient.LastName}").Bold();
                            text.Span($", quien asistió a nuestra clínica el día {_prescription.PrescriptionDate:dd MMMM yyyy}. ");
                            text.Span("Durante su consulta, el paciente fue evaluado cuidadosamente y se le brindó el siguiente diagnóstico y tratamiento recomendado.");
                        });

                        // Diagnóstico
                        column.Item().Text(text =>
                        {
                            text.Span("Diagnóstico: ").Bold();
                            text.Span($"{_prescription.Diagnosis}. ");
                            text.Span("Este diagnóstico refleja la evaluación médica realizada por nuestros especialistas, identificando las condiciones o problemas que afectan al paciente.");
                        });

                        // Tratamiento
                        column.Item().Text(text =>
                        {
                            text.Span("Tratamiento: ").Bold();
                            text.Span($"{_prescription.Treatment}. ");
                            text.Span("El tratamiento descrito se ha diseñado específicamente para abordar las condiciones diagnosticadas y mejorar la salud y el bienestar del paciente. ");
                            text.Span("Se recomienda seguir cuidadosamente estas indicaciones para obtener los mejores resultados.");
                        });

                        // Notas (si existen)
                        if (!string.IsNullOrEmpty(_prescription.Notes))
                        {
                            column.Item().Text(text =>
                            {
                                text.Span("Notas adicionales: ").Bold();
                                text.Span($"{_prescription.Notes}. ");
                                text.Span("Estas notas ofrecen información complementaria o recomendaciones adicionales importantes para la comprensión y seguimiento del tratamiento.");
                            });
                        }

                        // Observaciones (si existen)
                        if (!string.IsNullOrEmpty(_prescription.Observations))
                        {
                            column.Item().Text(text =>
                            {
                                text.Span("Observaciones: ").Bold();
                                text.Span($"{_prescription.Observations}. ");
                                text.Span("Las observaciones registradas incluyen aspectos relevantes que el médico considera importantes para el monitoreo continuo del paciente.");
                            });
                        }

                        // Cierre
                        column.Item().Text(text =>
                        {
                            text.Span("Si tiene preguntas adicionales sobre esta receta o el tratamiento prescrito, ");
                            text.Span("no dude en contactarnos en nuestra clínica. Estamos comprometidos con su salud y bienestar.");
                        });
                    });

                // Pie de página
                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Clínica Oftalmológica Tapia y Especialidades - Página ");
                        text.CurrentPageNumber();
                    });
            });
        }
    }
}