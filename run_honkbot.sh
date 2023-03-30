#!/bin/bash

for ((i = 1; i > 0; i=$?)); do
    git pull origin main;
    dotnet run --configuration Release --project ./Honk/Honk.csproj
done