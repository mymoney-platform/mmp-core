#!/bin/bash

docker compose -f ./docker-compose.yaml --profile seed up --build -d
#docker compose -f ./docker-compose.yaml --profile infra up --build -d