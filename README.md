# url-shortene

Url shortener course

## Infrastructure as Code

### Log in into Azure

```bash
az login
```

```bash
az group create --name urlshortener-dev --location westeurope
```

```bash
az deployment group what-if --resource-group urlshortener-dev --template-file Infrastructure/main.bicep
```
