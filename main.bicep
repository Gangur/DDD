targetScope='subscription'

param localtion string = 'northcentralus'

var rgName = 'ddd-rg'
var storageName = 'ddd-storage'
var functionAppName = 'ddd-func'
var applicationInsightsName = 'ddd-application-insights'

resource newRG 'Microsoft.Resources/resourceGroups@2022-09-01' = {
  name: rgName
  location: localtion
}

module storageAcct 'storage.bicep' = {
  name: 'storageModule'
  scope: newRG
  params: {
    storageLocation: localtion
    storageName: storageName
  }
}

module fuctions 'functions.bicep' = {
  name: 'fuctionsModule'
  scope: newRG
  params: {
    location: localtion
    storageName: storageName
    storageId: storageAcct.outputs.storageAcctId
    functionAppName: functionAppName
    applicationInsightsName: applicationInsightsName
  }
}