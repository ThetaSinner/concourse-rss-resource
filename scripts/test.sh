#!/usr/bin/env sh

./check < test-input.json

printf "\n\n"

./in "output" < test-input.json

printf "\n\n"

./out < test-input.json

printf "\n\n"
