using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/", () => new {Name = "Propolis gay", Age = 24});
app.MapGet("/AddHeader", (HttpResponse response) => {
    response.Headers.Add("Teste", "propolis gay");
    return new {Name = "propolis gay", Age = 24};
});

app.MapPost("/products", (Product product) => {
    ProductRepository.Add(product);
    return Results.Created($"/products/{product.Code}", product.Code);// colocando o $ vc pode concaternar a string
});

//api.app.com/user/{code} //no postman coloca assimm getproduct/asdf
app.MapGet("/products/{code}", ([FromRoute] string code) => {
    var product = ProductRepository.GetBy(code);
    if(product != null){
        Console.WriteLine("Product found"); //essa linha serve para rodar um debug via terminal, ver aula 29
        return Results.Ok(product);
    }
        
    return Results.NotFound();    
});

//aqui editaremos um produto
app.MapPut("/products", (Product product) => { 
    var productSaved = ProductRepository.GetBy(product.Code);
    productSaved.Name = product.Name;
    return Results.Ok();
});

//para deletar dados
app.MapDelete("/products/{code}", ([FromRoute] string code) => {
    var productSaved = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);
    return Results.Ok();
});

app.MapGet("/configuration/database", (IConfiguration configuration) => {
    return Results.Ok($"{configuration["database:connection"]}/{configuration["database:port"]}");
});

app.Run();

public static class ProductRepository {  //para que a requisiçao sirva para varias vezes devemos deixala estatca
    public static List<Product> Products { get; set; }
    public static void Add(Product product) {
        if(Products == null)
            Products = new List<Product>();

        Products.Add(product);    
    }

    public static Product GetBy(string code) {
        return Products.FirstOrDefault(p => p.Code == code); // com o comando FirstOrDefault caso eu busque um cadastro inexistente ele n da erro, apenas mostra como null
    }

    public static void Remove(Product product) {
        Products.Remove(product);
    }
}

public class Product {
    public string Code { get; set; }
    public string Name { get; set; }
}
