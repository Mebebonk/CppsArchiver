@echo off

cd ..
mkdir build
cd build

cmake -G "NMake Makefiles" .. && nmake install
