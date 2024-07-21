cmake_minimum_required(VERSION 3.27.0)

project(ArchiverImplementation)

add_library(
	${PROJECT_NAME} SHARED
	source/c_api.cpp
)

install(TARGETS ${PROJECT_NAME} DESTINATION bin)
