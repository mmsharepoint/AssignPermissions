$ClientId = '' # Administrative App registration to grant/revoke permissions
$TenantID = '' # Your Tenant ID
$SiteId = '' # Your Site Id
$ListID = '' # Your List ID (or Name)
$ClientId2 = '' # App registration with .Selected permissions to be granted/revoked on resources

Install-Module -Name MSAL.PS

$authParams = @{ ClientId = $ClientId; TenantId = $TenantID; Interactive = $true }
$auth = Get-MsalToken @authParams
$accessToken = $auth.AccessToken

$header = @{ "Content-Type" = "application/json"; Authorization = "Bearer $accessToken" }

@{ "requests" = @( @{ "entityTypes" = @( "listItem" ); "query" = @{ "queryString" = "*" } } )}
$body = @{
    "roles" = @(
        "read"
    );
    "grantedTo" = @{
        "application" = @{
            "id" = $ClientId2
        }
    }
}
$requestBody = ConvertTo-Json -InputObject $body -Depth 4
$assingnRequestUrl = "https://graph.microsoft.com/beta/sites/$($SiteId)/lists/$($ListID)/permissions"
$assignResponse = Invoke-RestMethod -Uri $assingnRequestUrl -Method Post -Body $requestBody -Headers $header
$assignResponse.id

# Test 

$authParams = @{ ClientId = $ClientId2; TenantId = $TenantID; Interactive = $true }
$auth = Get-MsalToken @authParams
$accessToken = $auth.AccessToken

$header = @{ "Content-Type" = "application/json"; Authorization = "Bearer $accessToken" }

# Test List

$requestUrl = "https://graph.microsoft.com/beta/sites/$($SiteId)/lists/$($ListID)"
$listResponse = Invoke-RestMethod -Uri $requestUrl -Method Get -Headers $header
$listResponse.displayName

# Test List Items

$requestUrl = "https://graph.microsoft.com/beta/sites/$($SiteId)/lists/$($ListID)/items" 
$listItemResponse = Invoke-RestMethod -Uri $requestUrl -Method Get -Headers $header
foreach($i in $listItemResponse.value)
{
    $i.id
}