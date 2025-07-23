#!/bin/bash
set -e

echo "Ejecutando start_server.sh..." >> /home/ubuntu/rdoxlogin.log

# Restart the api service
sudo systemctl restart LogInService

echo "Servicio reiniciado correctamente." >> /home/ubuntu/rdoxlogin.log