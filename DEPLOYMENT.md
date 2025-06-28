# Deployment Guide for ITventory

This guide explains how to deploy the ITventory application to Azure Container Apps using GitHub Actions.

## Overview

The deployment process includes:

1. Running unit tests
2. Building and pushing Docker images to GitHub Container Registry (GHCR)
3. Deploying to Azure Container Apps (Staging environment)

## Prerequisites

- Azure subscription
- Azure Container Apps environment
- GitHub repository with Actions enabled

## Required GitHub Secrets

Set up the following secrets in your GitHub repository (Settings → Secrets and variables → Actions):

### Azure Authentication

- `AZURE_CREDENTIALS`: Azure service principal credentials in JSON format

### Azure Resources

- `AZURE_RESOURCE_GROUP`: Name of your Azure resource group
- `AZURE_CONTAINER_APP_NAME`: Name of your Azure Container App

### Database

- `DATABASE_CONNECTION_STRING`: Connection string for your database

## Setting up Azure Credentials

1. Create a service principal:

```bash
az ad sp create-for-rbac --name "github-actions-itventory" --role contributor --scopes /subscriptions/{subscription-id}/resourceGroups/{resource-group-name} --sdk-auth
```

2. Copy the JSON output and add it as the `AZURE_CREDENTIALS` secret in GitHub.

## GitHub Environment Setup

Create a `staging` environment in your GitHub repository (Settings → Environments) for additional security and deployment protection.

## Azure Container Apps Setup

Create your Azure Container App:

```bash
# Create Container Apps environment
az containerapp env create \
  --name itventory-env \
  --resource-group myapp-rg \
  --location eastus

# Create Container App
az containerapp create \
  --name itventory-staging \
  --resource-group myapp-rg \
  --environment itventory-env \
  --image ghcr.io/yourusername/itventory:latest \
  --target-port 8080 \
  --ingress external \
  --secrets connection-string="<your-db-connection>" \
  --env-vars ASPNETCORE_ENVIRONMENT=Staging \
  --env-vars ConnectionStrings__DefaultConnection=secretref:connection-string
```

## Deployment Workflow

The GitHub Actions workflow (`build-and-deploy.yml`) will:

1. **Test**: Run unit tests on every push and pull request
2. **Build**: Build and push Docker image to GHCR when tests pass
3. **Deploy**: Deploy to Azure Container Apps on main branch pushes

## Environment Configuration

The application uses:

- `ASPNETCORE_ENVIRONMENT=Staging` for the staging environment
- Connection strings are stored as Azure Container Apps secrets
- Environment variables are configured through the deployment process

## Manual Deployment

If you need to deploy manually:

```bash
# Build and push image
docker build -t ghcr.io/yourusername/itventory:latest .
docker push ghcr.io/yourusername/itventory:latest

# Deploy to Azure Container Apps
az containerapp update \
  --name your-container-app-name \
  --resource-group your-resource-group \
  --image ghcr.io/yourusername/itventory:latest
```

## Security Best Practices

1. **Connection Strings**: Never commit connection strings to your repository
2. **Secrets**: Use GitHub secrets for all sensitive data
3. **Environments**: Use GitHub environments with protection rules
4. **Service Principal**: Use least privilege access for the Azure service principal

## Monitoring

- Check deployment status in GitHub Actions
- Monitor application health through Azure Container Apps logs
- Application exposes health check endpoints at `/health`
- Use Azure Application Insights for application monitoring
