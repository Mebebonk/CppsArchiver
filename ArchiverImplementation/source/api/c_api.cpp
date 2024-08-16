#include "api/c_api.h"

#include <stdexcept>

#include "zip/Zip.h"
#include "functionality/decode.h"

const char* getExceptionMessage(Exception exception)
{
	return static_cast<std::runtime_error*>(exception)->what();
}

void deleteException(Exception exception)
{
	delete static_cast<std::runtime_error*>(exception);
}

void setBufferSize(uint64_t size)
{
	bufferSize = size;
}

void unzip(uint64_t compressionMethod, uint64_t compressedSize, SendDataCallback sendDataCallback, ReceiveDataCallback receiveDataCallback, FinishCallback finishCallback, Exception* exception) try
{
	*exception = nullptr;

	decoding::decode<zip::Zip>(sendDataCallback, receiveDataCallback, finishCallback, compressionMethod, compressedSize);
}
catch (const std::exception& e)
{
	*exception = new std::runtime_error(e.what());
}
