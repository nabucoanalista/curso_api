using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/", () => new {Name = "Propolis gay", Age = 24});
app.MapGet("/AddHeader", (HttpResponse response) => {
    response.Headers.Add("Teste", "propolis gay");
    return new {Name = "propolis gay", Age = 24};
});

app.MapPost("/saveproduct", (Product product) => {
    ProductRepository.Add(product);
});

//api.app.com/user/{code} //no postman coloca assimm getproduct/asdf
app.MapGet("/getproduct/{code}", ([FromRoute] string code) => {
    var product = ProductRepository.GetBy(code);
    return product;
});

//aqui editaremos um produto
app.MapPut("/eiditprod", (Product product) => { 
    var productSaved = ProductRepository.GetBy(product.Code);
    productSaved.Name = product.Name;
});

//para deletar dados
app.MapDelete("/delprod/{code}", ([FromRoute] string code) => {
    var productSaved = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);
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
