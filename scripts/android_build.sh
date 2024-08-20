#!/bin/bash

set -e

cd .. && rm -rf build && mkdir build && cd build

cmake -DANDROID_ABI=arm64-v8a -DANDROID_PLATFORM=android-28 -DCMAKE_TOOLCHAIN_FILE=${NDK_PATH}/build/cmake/android.toolchain.cmake .. && make install -j $(nproc)

cd ../scripts/
