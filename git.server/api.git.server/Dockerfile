FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 22

RUN apt update && apt install -y git && apt install -y nano
RUN git config --global init.defaultBranch master

RUN useradd -ms /bin/sh git

USER git
WORKDIR /home/git
RUN mkdir .ssh && chmod 700 .ssh
RUN touch .ssh/authorized_keys && chmod 600 .ssh/authorized_keys

USER root
RUN apt update && apt install -y openssh-server
RUN service ssh start

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["api.git.server/api.git.server.csproj", "api.git.server/"]
RUN dotnet restore "api.git.server/api.git.server.csproj"
COPY . .
WORKDIR "/src/api.git.server"
RUN dotnet build "api.git.server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "api.git.server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api.git.server.dll"]