using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using GestorPagosApi.DTOs;
using GestorPagosApi.Identity;
using GestorPagosApi.Models.Entities;
using GestorPagosApi.Models.ViewModels.ResponsableViewModels;
using GestorPagosApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GestorPagosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = IdentityData.ResponsableUserPolicyName)]
    public class ResponsableViewsController : ControllerBase
    {
        private readonly RepositoryUsuarios repositoryUsuarios;
        private readonly RepositoryTemporadas repositoryTemporadas;
        private readonly IMapper mapper;
        private readonly IConverter _converter;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;


        public ResponsableViewsController(ITempDataDictionaryFactory tempDataDictionaryFactory, RepositoryUsuarios repositoryUsuarios, RepositoryTemporadas repositoryTemporadas, IMapper mapper, IConverter converter, ICompositeViewEngine viewEngine)
        {
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
            this.repositoryUsuarios = repositoryUsuarios;
            this.repositoryTemporadas = repositoryTemporadas;
            this.mapper = mapper;
            _converter = converter;
            _viewEngine = viewEngine;
            //DinkToPdf.NativeLibrary.Load("ruta/a/libwkhtmltox.dll");
            
        }
        [AllowAnonymous]
        [HttpGet("Recibo")]
        public async Task<IActionResult> GetRecibo()
        {
            // Renderizar la vista Razor a HTML
            var htmlContent = await RenderViewToStringAsync("Recibo");

            // Crear el PDF a partir del HTML
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Recibo"
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            // Retornar el PDF generado
            return File(file, "application/pdf", "Recibo.pdf");
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var context = HttpContext;
            var claim = User.FindFirst("id");
            if (claim == null) return BadRequest();
            string id = claim.Value;
            Usuarios? user = await repositoryUsuarios.GetUserIncludeJugadoresPagosAsync(int.Parse(id));
            if (user == null) return NotFound();
            var jugs = user.Jugador.ToList();
            var costototal = user.Jugador.Select(x => x.IdTemporadaNavigation.Costo).Sum();
            var saldopagado = user.Jugador.Select(x => x.IdTemporadaNavigation.Costo - x.Deuda).Sum();
            var listapagos = user.Pago.Select(x => mapper.Map<HistorialPagos>(x)).ToList();
            DashboardResponsableViewModel vm = new DashboardResponsableViewModel()
            {
                costoTotalTemporada = costototal,
                saldoPagado = saldopagado,
                listaPagos = listapagos,
            };
            vm.saldoPendiente = vm.costoTotalTemporada - vm.saldoPagado;
            return Ok(vm);
        }
        private async Task<string> RenderViewToStringAsync(string viewName)
        {
            using (var writer = new StringWriter())
            {
                var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} no se encontró.");
                }

                var viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()),
                    _tempDataDictionaryFactory.GetTempData(ControllerContext.HttpContext),
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return writer.GetStringBuilder().ToString();
            }
        }
    }
}