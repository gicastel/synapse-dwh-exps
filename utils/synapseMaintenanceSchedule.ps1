# 1 - parameters
$clientId= ""
$clientSecret = ""
$tenantId = ""
$grant_type = "client_credentials"
$resource = "https://management.azure.com/"

$subscriptionId = ""
$rgName = ""
$serverName = ""
$dbName = ""

# 2 - request auth token

$authUrl = "https://login.microsoftonline.com/$tenantId/oauth2/token"

$authBody = @{resource="$resource";grant_type="$grant_type";client_Id="$clientId";client_secret="$clientSecret"}

$authRequest = @{
    Method      = "POST"
    Uri         = $authUrl
    ContentType = "application/x-www-form-urlencoded"
    Body        = $authBody
}    

$authData = Invoke-RestMethod @authRequest

$token = $authData.access_token

# 3 - POST maintenance schedule

$maintenanceApiEndpoint = "https://management.azure.com/subscriptions/$subscriptionId/resourceGroups/$rgName/providers/Microsoft.Sql/servers/$serverName/databases/$dbName/maintenanceWindows/current?maintenanceWindowName=current&api-version=2017-10-01-preview"


$windowsList = New-Object System.Collections.ArrayList
$windowsList.Add(@{"dayOfWeek"="Sunday";"startTime"="05:00:00";"duration"="PT180M";})
$windowsList.Add(@{"dayOfWeek"="Wednesday";"startTime"="02:00:00";"duration"="PT180M";})

$timeRanges = @{"timeRanges"=$windowsList;}
$properties = @{}
$properties.Add("properties", $timeRanges)

$scheduleJson = ($properties | ConvertTo-Json -Depth 3)
 

$Headers = @{
    "Authorization" = "Bearer $token"
}

$scheduleRequestParams = @{
    Method      = "PUT"
    Uri         = $maintenanceApiEndpoint
    Headers     = $Headers
    ContentType = "application/json"
    Body        = $scheduleJson
}

Invoke-RestMethod @scheduleRequestParams