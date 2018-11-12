#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");
    string RunBookWebHook = "https://s8events.azure-automation.net/webhooks?token=%2b65nEPt3eT0TX5wymvt4hZqRBZ83rgi46FB3FYXrjbc%3d";
    bool test = false;
    if(req.Query.ContainsKey("validationToken")){       

        return  new OkObjectResult(req.Query["validationToken"].ToString()); 
    }else{
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var requestData = new StringContent(requestBody, Encoding.UTF8, "application/json");
        using (var client = new HttpClient())
        {
             var response = await client.PostAsync(String.Format(RunBookWebHook), requestData);
             var result = await response.Content.ReadAsStringAsync();
        }
        return new OkObjectResult("No-validationToken"); 
    }

}