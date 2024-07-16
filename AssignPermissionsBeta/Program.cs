using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Beta;
using Microsoft.Graph.Beta.Models;
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

// https://learn.microsoft.com/dotnet/api/azure.identity.interactivebrowsercredential
var interactiveCredential = new InteractiveBrowserCredential(options);

var graphClient = new GraphServiceClient(interactiveCredential, scopes);

var site = await graphClient.Sites[config["siteId"]].GetAsync();
var listPermissións = await graphClient.Sites[config["siteId"]].Lists["Test"].Permissions.GetAsync();

listPermissións.Value.ForEach(p =>
{
  if (p.GrantedTo.Application != null)
  {
    Console.WriteLine(p.GrantedTo.Application.DisplayName);
  }
  if (p.GrantedTo.User != null)
  {
    Console.WriteLine(p.GrantedTo.User.DisplayName);
  }
    
  Console.WriteLine(p.Roles[0]);
});

Permission newAppPerm = new Permission
{
  Roles = ["read"],
  GrantedTo = new IdentitySet
  {
    Application = new Identity
    {
      Id = "6f090c4a-e687-4ab9-9bcf-ae40a949a039",
      DisplayName = "SPOGranularPermissions"
    },
  },
};

try
{
  var response = await graphClient.Sites[config["siteId"]].Lists["Test"].Permissions.PostAsync(newAppPerm);
}
catch ( ODataError oDataError )
{
  Console.WriteLine(oDataError.Error);
}
Console.ReadLine();