#!/bin/bash

set -e

cd .. && mkdir -p build && cd build

cmake .. && make install -j $(nproc)
