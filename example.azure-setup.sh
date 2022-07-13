#!/bin/bash

APP_NAME=ncn-bank-api-app
APP_RG=ncn-bank-api-rg
APP_DBMS=PostgreSQL
APP_CONN_STRING='User ID=postgres;Password=password;Host=db.host.com;Port=5432;Database=postgres'

az webapp config connection-string set -n $APP_NAME -g $APP_RG -t $APP_DBMS --settings Default=$APP_CONN_STRING;
az webapp config appsettings set -n $APP_NAME -g $APP_RG --settings ASPNETCORE_ENVIRONMENT=Production;
az webapp config appsettings set -n $APP_NAME -g $APP_RG --settings DISABLE_PHP_BUILD=True;
az webapp config appsettings set -n $APP_NAME -g $APP_RG --settings ENABLE_ORYX_BUILD=False;
az webapp config appsettings set -n $APP_NAME -g $APP_RG --settings SCM_DO_BUILD_DURING_DEPLOYMENT=False;