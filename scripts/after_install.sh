#!/bin/bash
set -e

echo "Start after_install.sh" >> /home/ubuntu/rdoxlogin.log

# Exporta las variables para usar el SDK correcto
export DOTNET_ROOT=$HOME/.dotnet
export PATH=$HOME/.dotnet:$PATH

sudo chown -R ubuntu:ubuntu /home/ubuntu/rdoxloginsc

chmod -R u+rwX /home/ubuntu/rdoxloginsc

echo "change directory publish" >> /home/ubuntu/rdoxlogin.log

# 👉 Navega al código fuente
cd /home/ubuntu/rdoxloginsc

echo "start publish" >> /home/ubuntu/rdoxlogin.log
# 👉 Publica en la carpeta de artefactos
$DOTNET_ROOT/dotnet publish Login.csproj -c Release -o /home/ubuntu/rdoxlogin

echo "End publish" >> /home/ubuntu/rdoxlogin.log

echo "End after_install.sh." >> /home/ubuntu/rdoxlogin.log