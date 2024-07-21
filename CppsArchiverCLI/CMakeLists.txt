cmake_minimum_required(VERSION 3.27.0)

project(CppsArchiverCLI)

add_custom_target(
	${PROJECT_NAME} ALL
	WORKING_DIRECTORY ${PROJECT_SOURCE_DIR}
	COMMAND dotnet publish -o ${PROJECT_BINARY_DIR}/bin /p:DebugType=None /p:DebugSymbols=false
)

install(DIRECTORY ${PROJECT_BINARY_DIR}/bin/ DESTINATION bin)
