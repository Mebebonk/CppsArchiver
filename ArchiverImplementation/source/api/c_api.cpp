#include "api/c_api.h"

#include <stdexcept>

#include "zip/Zip.h"

const char* getExceptionMessage(Exception exception)
{
	return static_cast<std::runtime_error*>(exception)->what();
}

void deleteException(Exception exception)
{
	delete static_cast<std::runtime_error*>(exception);
}

void unzip(uint64_t compressionMethod, uint64_t compressedSize, SendDataCallback sendDataCallback, ReceiveDataCallback receiveDataCallback, FinishCallback finishCallback, Exception* exception) try
{
	*exception = nullptr;

	switch (compressionMethod)
	{
	case 0x0:
		break;

	case 0x8:
		break;
	}
}
catch (const std::exception& e)
{
	*exception = new std::runtime_error(e.what());
}
