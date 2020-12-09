#!/usr/bin/env bash

dotnet build

cp ./bin/Debug/netcoreapp3.1/* scripts/
