cmake_minimum_required(VERSION 3.27.0)

set(CMAKE_INSTALL_PREFIX ${CMAKE_BINARY_DIR} CACHE STRING "")
set(CMAKE_BUILD_TYPE Release CACHE STRING "")

if (UNIX)
	add_definitions(-D__LINUX__)

	add_compile_options(-fvisibility=hidden)
endif()

project(CppsArchiver)

add_subdirectory(ArchiverImplementation)
add_subdirectory(CppsArchiverAPI)
add_subdirectory(CppsArchiverCLI)
