#!/bin/bash

set -e

cd .. && rm -rf build && mkdir build && cd build

cmake .. && make install -j $(nproc)

cd ../scripts/
