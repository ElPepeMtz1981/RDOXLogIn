#!/bin/bash
set -e

echo "🔧 Ejecutando after_install.sh..."

# Exporta las variables para usar el SDK correcto
export DOTNET_ROOT=$HOME/.dotnet
export PATH=$HOME/.dotnet:$PATH

sudo chown -R ubuntu:ubuntu /home/ubuntu/rdoxloginrssc

chmod -R u+rwX /home/ubuntu/rdoxloginrssc

# 👉 Navega al código fuente
cd /home/ubuntu/rdoxloginrssc

# 👉 Publica en la carpeta de artefactos
$DOTNET_ROOT/dotnet publish Login.csproj -c Release -o /home/ubuntu/rdoxlogin

echo "✅ Publicación completada en /home/ubuntu/login."