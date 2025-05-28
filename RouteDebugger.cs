using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace AnywhereTecnologiaSystem.Areas.Admin.App.Classes
{
    public static class RouteDebugger
    {
        // Extensões comuns de arquivos estáticos a serem ignorados
        private static readonly string[] IgnoredExtensions = new[]
        {
            ".css", ".js", ".map", ".ico", ".png", ".jpg", ".jpeg", ".svg", ".woff", ".woff2", ".ttf", ".gz", ".txt", ".md", ".81b7ukuj9c"
        };

        // Padrões de rotas a ignorar (ex: fallback para arquivos)
        private static readonly string[] IgnoredRoutePatterns = new[]
        {
            "{**path:file}", "lib/bootstrap/LICENSE" // fallback genérico para arquivos estáticos
        };

        public static string ListAllRoutes(HttpContext httpContext)
        {
            if (httpContext == null)
                return "HttpContext está nulo. Use este método dentro de uma Controller ou Middleware.";

            var endpointSources = httpContext.RequestServices.GetService<IEnumerable<EndpointDataSource>>();
            if (endpointSources == null)
                return "EndpointDataSource não disponível. Verifique a configuração de serviços.";

            var sb = new StringBuilder();
            sb.AppendLine("MÉTODO\tROTA\tCONTROLLER\tACTION");
            sb.AppendLine("------\t----\t----------\t------");

            foreach (var endpoint in endpointSources.SelectMany(e => e.Endpoints))
            {
                var method = endpoint.Metadata.GetMetadata<HttpMethodMetadata>()?.HttpMethods?.FirstOrDefault() ?? "ANY";
                var route = (endpoint as RouteEndpoint)?.RoutePattern.RawText ?? string.Empty;

                // FILTRO: ignora rotas de arquivos estáticos e rotas genéricas
                if (IsStaticAsset(route) || IsIgnoredPattern(route))
                    continue;

                var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
                sb.AppendLine($"{method}\t{route}\t{actionDescriptor?.ControllerName}\t{actionDescriptor?.ActionName}");
            }

            return sb.ToString();
        }

        private static bool IsStaticAsset(string? route)
        {
            if (string.IsNullOrWhiteSpace(route))
                return false;

            return IgnoredExtensions.Any(ext => route.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }

        private static bool IsIgnoredPattern(string? route)
        {
            if (string.IsNullOrWhiteSpace(route))
                return false;

            return IgnoredRoutePatterns.Any(pattern => string.Equals(pattern, route, StringComparison.OrdinalIgnoreCase));
        }
    }
}
