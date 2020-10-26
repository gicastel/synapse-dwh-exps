# current path
$wd = pwd
# file name
$appcfg = $args[0]
# complete connection string
$newConnString = $args[1]
# open file as XML
$xml =  New-Object System.XML.XMLDocument
$xml.Load($appcfg)
# replace attribute in execution context/privileged context
$xml.SelectNodes("configuration/SqlUnitTesting/ExecutionContext").SetAttribute("ConnectionString","$newConnString")
$xml.SelectNodes("configuration/SqlUnitTesting/PrivilegedContext").SetAttribute("ConnectionString","$newConnString")
# replace file
$xml.Save($appcfg)