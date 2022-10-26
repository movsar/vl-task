FROM mcr.microsoft.com/mssql/server:2022-latest

COPY Data/SqlScripts .
             
ENV SA_PASSWORD <YourStrong@Passw0rd>
ENV ACCEPT_EULA Y

RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 10 \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P ${SA_PASSWORD} -i create-database.sql