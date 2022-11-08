using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/", () => new {Name = "Propolis gay", Age = 24});
app.MapGet("/AddHeader", (HttpResponse response) => {
    response.Headers.Add("Teste", "propolis gay");
    return new {Name = "propolis gay", Age = 24};
});

/*app.MapPost("/saveproduct", (Product product) => {
    return product.Code + " - " + product.Nome;
});*/

app.MapPost("/saveproduct", (Product product) => {
    ProductRepository.Add(product);
});

//api.app.com/users?datastart={date}&dateend={date} //no postman coloca isso no endereço /getproduct?datestart=x&dateend=y
app.MapGet("/getproduct", ([FromQuery] string datestart, [FromQuery] string dateEnd) => {
    return datestart + " - " + dateEnd;
});
//api.app.com/user/{code} //no postman coloca assimm getproduct/asdf
app.MapGet("/getproduct/{code}", ([FromRoute] string code) => {
    return code;
});

app.MapGet("/getproduct2", (HttpRequest request) => {
    return request.Headers["product-code"].ToString();
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
        return Products.First(p => p.Code == code);
    }
}

public class Product {
    public string Code { get; set; }
    public string Nome { get; set; }
}
