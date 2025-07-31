# url-shortene

Url shortener course

## Infrastructure as Code

### Log in into Azure

```bash
az login
```

### Create Resource Group

```bash
az group create --name urlshortener-dev --location westeurope
```

### Deploymento group

```bash
az deployment group what-if --resource-group urlshortener-dev --template-file Infrastructure/main.bicep
```

### Create User for GH Actions

```bash

az ad sp create-for-rbac --name "GitHub-Actions-SP" \
                         --role contributor \
                         --scope /subscriptions/931d4293-3db8-4485-9a66-44588c475fc4 \
                         --sdk-auth

```

### Configure a federated identity credential on an app

```bash



```
