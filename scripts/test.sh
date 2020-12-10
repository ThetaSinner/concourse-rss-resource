#!/usr/bin/env bash

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"
pushd "$DIR" > /dev/null || exit

./check [] < "$DIR"/test-initial-input.json

printf "\n\n"

./check < "$DIR"/test-input.json

printf "\n\n"

./in "output" < "$DIR"/test-input.json

printf "\n\n"

./out < "$DIR"/test-input.json

printf "\n\n"

popd > /dev/null || exit
