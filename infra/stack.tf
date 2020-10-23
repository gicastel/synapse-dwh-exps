resource "azurerm_resource_group" "rg" {
    name = "${var.prefix}-rg"
    location = var.location
}

resource "azurerm_storage_account" "storage_account" {
  name                     = "${replace(var.prefix, "/-/", "")}strg"
  resource_group_name      = azurerm_resource_group.rg.name
  location                 = azurerm_resource_group.rg.location
  account_tier             = "Standard"
  account_replication_type  = "LRS"
  enable_https_traffic_only = true
  min_tls_version           = "TLS1_2"
  allow_blob_public_access  = false
}

resource "azurerm_sql_server" "sql_server" {
    resource_group_name          = azurerm_resource_group.rg.name
    location                     = azurerm_resource_group.rg.location
    name                         = "${var.prefix}-srv"
    administrator_login          = var.sqladmin
    administrator_login_password = var.sqlpassword
    version                      = "12.0"
}

resource "azurerm_mssql_server_extended_auditing_policy" "sql_server_auditing_policy" {
    server_id                  = azurerm_sql_server.sql_server.id
    storage_endpoint           = azurerm_storage_account.storage_account.primary_blob_endpoint
    storage_account_access_key = azurerm_storage_account.storage_account.primary_access_key
    retention_in_days          = 6
}
resource "azurerm_sql_database" "sql_database" {
    resource_group_name              = azurerm_resource_group.rg.name
    location                         = azurerm_resource_group.rg.location
    name                             = "${var.prefix}-dw"
    server_name                      = azurerm_sql_server.sql_server.name
    edition                          = "DataWarehouse"
    requested_service_objective_name = "DW100c"
}

resource "azurerm_sql_database" "sql_database_sql" {
    resource_group_name              = azurerm_resource_group.rg.name
    location                         = azurerm_resource_group.rg.location
    name                             = "${var.prefix}-db"
    server_name                      = azurerm_sql_server.sql_server.name
    edition                          = "Standard"
    requested_service_objective_name = "S1"
}

resource "azurerm_sql_firewall_rule" "localip" {
    resource_group_name = azurerm_resource_group.rg.name
    server_name         = azurerm_sql_server.sql_server.name
    name                = "openIp"
    start_ip_address    = "0.0.0.0"
    end_ip_address      = "0.0.0.0"
}