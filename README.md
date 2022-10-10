# vl-task

To run this project, first configure MS SQL Database, to do this:
1. Run ./SetupQuery.sql, then ./PrefillQuery.sql against your SQL Server instance
2. In Data/Constants.cs change the localServerName variable to an appropriate value
3. Select as Startup either RestService or WebApp
