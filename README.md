
# 🔍 RouteDebugger para ASP.NET Core

Uma classe utilitária que lista todas as rotas registradas em uma aplicação ASP.NET Core, com seus respectivos métodos HTTP, caminhos, controllers e actions. Ideal para depuração e diagnóstico de rotas em ambientes de desenvolvimento.

---

## 📦 Instalação

Adicione o arquivo `RouteDebugger.cs` à pasta desejada no seu projeto, por exemplo:
```
/Areas/Admin/App/Classes/RouteDebugger.cs
```

---

## 🚀 Como usar

Dentro de um Controller, Middleware ou qualquer classe que tenha acesso ao `HttpContext`, invoque:

```csharp
var routeInfo = RouteDebugger.ListAllRoutes(HttpContext);
Console.WriteLine(routeInfo); // Exibe no console do servidor
```

### 💡 Exemplo de saída no console:
```plaintext
MÉTODO  ROTA                              CONTROLLER  ACTION
------  ----                              ----------  ------
ANY     {controller=Home}/{action=Index}/{id?}  Home   Index
ANY     {controller=Home}/{action=Index}/{id?}  Home   Privacy
ANY     {controller=Home}/{action=Index}/{id?}  Home   Error
GET     login                                   Login  Login
ANY     {controller=Home}/{action=Index}/{id?}         
ANY     login                                     
```

---

## ⚙️ Configurações

### 🔕 Ignorar arquivos estáticos

Para não listar rotas que servem arquivos como CSS, JS, imagens, fontes, etc., o script possui:

```csharp
private static readonly string[] IgnoredExtensions = new[]
{
    ".css", ".js", ".map", ".ico", ".png", ".jpg", ".jpeg", ".svg", ".woff", ".woff2", ".ttf", ".gz", ".txt", ".md", ".81b7ukuj9c"
};
```

Você pode adicionar ou remover extensões conforme sua aplicação.

---

### 🔕 Ignorar padrões de rota

Rotas genéricas ou de fallback também são ignoradas com base nesses padrões:

```csharp
private static readonly string[] IgnoredRoutePatterns = new[]
{
    "{**path:file}", "lib/bootstrap/LICENSE"
};
```

Ideal para evitar ruído de rotas que servem arquivos estáticos via fallback ou bibliotecas externas.

---

## 💡 Ideias de uso

- ✅ Verificar rotas mapeadas automaticamente em áreas grandes de uma aplicação
- 🐞 Depurar problemas de roteamento em ambiente de desenvolvimento
- 🔐 Auditar quais actions estão expostas para HTTP GET ou POST
- 🧪 Automatizar testes de segurança (por exemplo, identificar actions públicas não protegidas)

---

## ❗ Observações

- Essa utilidade deve ser usada apenas em ambientes de **desenvolvimento** ou **homologação**.
- Caso use essa ferramenta em produção, certifique-se de proteger a chamada por autenticação.
