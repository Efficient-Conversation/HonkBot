#!/bin/bash

for ((i = 1; i > 0; i=$?)); do
    git pull origin main;
done