using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Beta;
using Microsoft.Graph.Beta.Models.ODataErrors;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();
string clientId = config["clientID"];
string tenantId = config["tentantID"];

var scopes = new[] { "https://graph.microsoft.com/.default" };

var options = new InteractiveBrowserCredentialOptions
{
  TenantId = tenantId,
  ClientId = clientId,
  AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
  RedirectUri = new Uri("http://localhost"),
};

var interactiveCredential = new InteractiveBrowserCredential(options);

var graphClient = new GraphServiceClient(interactiveCredential, scopes);

try
{
  // Will not work
  // var site = await graphClient.Sites[config["siteId"]].GetAsync();
  // Will only work if Lists.SelectedOperations.Selected assigned otherwise: itemNotFound
  var list = await graphClient.Sites[config["siteId"]].Lists["Test"].GetAsync();
  var listÍtems = await graphClient.Sites[config["siteId"]].Lists["Test"].Items.GetAsync((requestConfiguration) =>
  {
    requestConfiguration.QueryParameters.Expand = new string[] { "fields" };
  });

  // Should be able to read all Lists.SelectedOperations.Selected or selected list items
  listÍtems.Value.ForEach(i =>
  {
    if (i.Fields.AdditionalData.ContainsKey("Title"))
    {
      Console.WriteLine(i.Fields.AdditionalData["Title"]);
    }
    
  });
}
catch (ODataError oDataError)
{
  // itemNotFound
  Console.WriteLine(oDataError.Error.Code);
}

Console.ReadLine();