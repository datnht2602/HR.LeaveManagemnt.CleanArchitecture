using System.Net;
using NSwag;
using NSwag.CodeGeneration.CSharp;

WebClient wclient = new ();         

var document = await OpenApiDocument.FromJsonAsync(wclient.DownloadString("https://localhost:7268/swagger/v1/swagger.json"));

wclient.Dispose();

var settings = new CSharpClientGeneratorSettings
{
    ClassName = "ServiceClient", 
    CSharpGeneratorSettings = 
    {
        Namespace = "HR.LeaveManagement.BlazorUI.Services.Base"
    },
    UseBaseUrl = false,
    GenerateBaseUrlProperty = false,
};
settings.GenerateClientInterfaces = true;

var generator = new CSharpClientGenerator(document, settings);	
var fileGenerator = generator.GenerateFile();
using FileStream fs = new FileStream("../src/HR.LeaveManagement.BlazorUI/Services/Base/ServiceClient.cs", FileMode.Create);
await fs.WriteAsync(System.Text.Encoding.UTF8.GetBytes(fileGenerator));