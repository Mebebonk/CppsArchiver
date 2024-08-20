@echo off

cd ..
mkdir build
cd build
del /s /q *

cmake -G "NMake Makefiles" .. && nmake install

cd ..\scripts\
