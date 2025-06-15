using Microsoft.AspNetCore.Mvc;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;
using MyPdfFillerApi.Models;
using PdfSharp.Fonts;

namespace MyPdfFillerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PdfController : ControllerBase
    {
        // Construtor para configurar o FontResolver
        public PdfController()
        {
            
            if (GlobalFontSettings.FontResolver == null)
            {
                GlobalFontSettings.FontResolver = new SimpleFontResolver();
            }
        }

        [HttpPost("fill")]
        public IActionResult FillPdf([FromBody] FormData data)
        {

            if (data == null)
            {
                return BadRequest(new { error = "Dados do formulário não enviados ou malformados." });
            }


            if (!System.IO.File.Exists("./PDFs/empty.pdf"))
            {
                return StatusCode(500, new { error = "Arquivo PDF base não encontrado." });
            }

            try
            {
                using (var input = System.IO.File.OpenRead("./PDFs/empty.pdf"))
                using (var output = new System.IO.MemoryStream())
                {
                    var document = PdfReader.Open(input, PdfDocumentOpenMode.Modify);
                    var page = document.Pages[0];
                    var gfx = XGraphics.FromPdfPage(page);

                    
                    var font = new XFont("Arial", 10);
                    var brush = XBrushes.Black;

                    // Posições aproximadas
                    gfx.DrawString(data.CompanyName ?? "", font, brush, new XPoint(128, 207));
                    gfx.DrawString(data.CompanyCnpj ?? "", font, brush, new XPoint(155, 224));
                    gfx.DrawString(data.CompanyAddress ?? "", font, brush, new XPoint(150, 242));
                    gfx.DrawString(data.CompanyCity ?? "", font, brush, new XPoint(150, 258));
                    gfx.DrawString(data.CompanyEmail ?? "", font, brush, new XPoint(137, 275));

                    gfx.DrawString(data.PersonName ?? "", font, brush, new XPoint(128, 323));
                    gfx.DrawString(data.PersonCpf ?? "", font, brush, new XPoint(155, 338));
                    gfx.DrawString(data.PersonAddress ?? "", font, brush, new XPoint(150, 355));
                    gfx.DrawString(data.PersonCity ?? "", font, brush, new XPoint(150, 372));
                    gfx.DrawString(data.PersonEmail ?? "", font, brush, new XPoint(133, 389));

                    gfx.DrawString(data.VehicleId ?? "", font, brush, new XPoint(128, 438));
                    gfx.DrawString(data.TransactionId ?? "", font, brush, new XPoint(146, 455));
                    gfx.DrawString(data.Value ?? "", font, brush, new XPoint(195, 470));

                    gfx.DrawString(data.Day ?? "", font, brush, new XPoint(327, 516));
                    gfx.DrawString(data.Month ?? "", font, brush, new XPoint(352, 516));
                    gfx.DrawString(data.Year ?? "", font, brush, new XPoint(378, 516));
                    gfx.DrawString(data.DocumentCity ?? "", font, brush, new XPoint(180, 560));
                    gfx.DrawString(data.MonthName ?? "", font, brush, new XPoint(400, 560));
                    gfx.DrawString(data.SignatureDay ?? "", font, brush, new XPoint(320, 560));
                    gfx.DrawString(data.SignatureYear ?? "", font, brush, new XPoint(537, 560));

                    document.Save(output, false);
                    return File(output.ToArray(), "application/pdf", "Requerimento_Preenchido.pdf");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao preencher o PDF: {ex}");
                return StatusCode(500, new { error = "Erro ao preencher o PDF", details = ex.ToString() });
            }
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(new { message = "PDF Controller is working!" });
        }
    }

  public class SimpleFontResolver : IFontResolver
{
    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        // Sempre retorna uma fonte padrão
        return new FontResolverInfo("DefaultFont");
    }

    public byte[] GetFont(string faceName)
    {
        // Retorna uma fonte básica embutida
        try
        {
            // Tenta carregar uma fonte do sistema Windows
            var fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            if (File.Exists(fontPath))
            {
                return File.ReadAllBytes(fontPath);
            }
            
            // Se não encontrar Arial, tenta Calibri
            fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "calibri.ttf");
            if (File.Exists(fontPath))
            {
                return File.ReadAllBytes(fontPath);
            }
            
            // Se não encontrar nenhuma, tenta uma fonte mais básica
            fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "tahoma.ttf");
            if (File.Exists(fontPath))
            {
                return File.ReadAllBytes(fontPath);
            }
        }
        catch
        {
            // Se der erro, ignora e continua
        }
        
        return null;
    }

    }
}