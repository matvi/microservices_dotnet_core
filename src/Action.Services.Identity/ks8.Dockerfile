#FROM microsoft/dotnet:3.0.0-runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /dotnetapp
COPY ./bin/Docker .
ENV ASPNETCORE_URLS http://*:5051
ENV ASPNETCORE_ENVIRONMENT k8s
ENTRYPOINT dotnet Action.Services.Identity.dll