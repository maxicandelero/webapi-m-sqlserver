# DDD ASP.NET Core Web API Template (SQL Server version) 

For now, to install these templates you will need to do the following:

- `git clone https://github.com/maxicandelero/webapi-m-sqlserver.git {LocalTempPath}`
- `dotnet new -i {LocalTempPath}`

To confirm the templates are installed correctly, type `dotnet new -l`, you should see it in the list of available projects. 

## Create a new project
You can use the following command to create a new project with the name `My.API` in it's own directory.

```
dotnet new webapi-m-sqlserver --ProjectName My.API
```

The `--ProjectName` argument is required.

## Uninstall the template

Because you installed the template by using a file path, you must uninstall it with the absolute file path. You can see a list of templates installed by running the `dotnet new -u` command. Your template should be listed last. Use the path listed to uninstall your template with the `dotnet new -u <ABSOLUTE PATH TO TEMPLATE DIRECTORY>` command.




