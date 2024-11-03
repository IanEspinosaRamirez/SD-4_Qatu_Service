FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev-env

WORKDIR /app

RUN apt-get update && apt-get install -y \
    curl \
    apt-transport-https \
    ca-certificates \
    gnupg \
    lsb-release \
    && rm -rf /var/lib/apt/lists/*

# Descarga e instala el cliente de Google Cloud SDK
RUN curl -O https://dl.google.com/dl/cloudsdk/channels/rapid/downloads/google-cloud-cli-linux-x86_64.tar.gz \
    && tar -xf google-cloud-cli-linux-x86_64.tar.gz \
    && ./google-cloud-sdk/install.sh \
    && rm google-cloud-cli-linux-x86_64.tar.gz

ENV PATH $PATH:/google-cloud-sdk/bin

RUN mkdir -p /home/finnisimo/Keys

COPY ./Keys/chromatic-being-438520-e5-04b6d53ca3e9.json /home/finnisimo/Keys/chromatic-being-438520-e5-04b6d53ca3e9.json

ENV GOOGLE_APPLICATION_CREDENTIALS="/home/finnisimo/Keys/chromatic-being-438520-e5-04b6d53ca3e9.json"

COPY ./certs/localhost.pfx /app/localhost.pfx

ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/localhost.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=finndavid1

COPY . .

EXPOSE 5136 7016

CMD ["dotnet", "run", "--project", "Web.Api", "--urls", "https://0.0.0.0:7016;http://0.0.0.0:5136"]
