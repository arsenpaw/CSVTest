dockerUp:
	DATABASE_PASSWORD=MSSQL_root docker-compose up -d --build
dockerDown:
	docker-compose down
migrationAdd:
	cd ./CSVTest.DataAccess && dotnet ef migrations add --startup-project ../Hrizer.API $(filter-out $@,$(MAKECMDGOALS)) && cd ../
# Remove last migration
migrationRemove:
	cd ./CSVTest.DataAccess && dotnet ef migrations remove --startup-project ../HRizer.API
updateDatabase:
	cd ./CSVTest.DataAccess && dotnet ef database update $(filter-out $@,$(MAKECMDGOALS)) --startup-project ../CSVTest.API
%:
	@: # ignore params after the action