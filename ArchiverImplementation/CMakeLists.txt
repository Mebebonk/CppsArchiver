cmake_minimum_required(VERSION 3.27.0)

set(CMAKE_CXX_STANDARD 20)

project(ArchiverImplementation)

add_library(
	${PROJECT_NAME} SHARED
	source/api/c_api.cpp
	source/zip/ZipStruct.cpp
)

target_include_directories(
	${PROJECT_NAME} PRIVATE
	${PROJECT_SOURCE}/include/
)

install(TARGETS ${PROJECT_NAME} DESTINATION bin)
