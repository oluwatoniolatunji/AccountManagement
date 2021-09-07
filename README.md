# AccountManagement
Account Management Microservice

## Database Set Up
- Create an MSSQL Database with a choice name
- Open SqlScript
- Run database-set-up-script.sql to create the tables required by the microservice

## Creating Migration if Necessary
- Clone the repo
- Open command prompt and cd to AccountManagement/EnergyAccountManagement.DataAccess
- Run the script
  dotnet ef --startup-project ../EnergyAccountManagement.Api migrations add CreateMeterReadingTable -c MeterReadingDbContext
  dotnet ef --startup-project ../EnergyAccountManagement.Api database update
  
## API Configuration
- Update appsettings.json and appsettings.Development.json EnergyAccountManagement.Api
