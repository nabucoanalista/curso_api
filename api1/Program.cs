var builder = WebApplication.CreateBuilder(args); /*as 2 primeiras linhas criam o host*/
var app = builder.Build();/*caso tenha coflito de portas vá nab pasta properties para alterar*/

app.MapGet("/", () => "Hello povo 2!"); /*end point faz o retorno da aplicaçao quando o comando é dado*/
app.MapGet("/user", () => "Hello povo 4!");
app.MapPost("/", () => new {Name = "Ibercson", Age = 35});
app.MapGet("/AddHeader", (HttpResponse response) => { 
    response.Headers.Add("Teste", "Ibercson");
    return new {Name = "Ibercson", Age = 35};
});

app.MapPost("/produtos", (Produtos product) => {
    return product.Code + " - " + product.Name;
});

app.Run();

public class Produtos {
    public string Code { get; set;}
    public string Name { get; set; }
}