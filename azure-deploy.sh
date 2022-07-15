#!/bin/bash

APP_NAME=ncn-bank-api-app
APP_RG=ncn-bank-api-rg
APP_PLAN=ncn-bank-api-plan
APP_REGION=eastus2
APP_OS=linux

az webapp up -n $APP_NAME -l $APP_REGION --os-type $APP_OS -p $APP_PLAN -g $APP_RG --sku F1 --verbose;
