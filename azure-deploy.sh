#!/bin/bash

az webapp up -n ncn-bank-api-app -l eastus2 --os-type linux -p ncn-bank-api-plan -g ncn-bank-api-rg --sku F1 --verbose
