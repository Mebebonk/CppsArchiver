#pragma once

#include "defines.h"

#ifdef __LINUX__
#define EXPORT extern "C" __attribute__((visibility("default")))
#else
#define EXPORT extern "C" __declspec(dllexport)
#endif

EXPORT const char* getExceptionMessage(Exception exception);

EXPORT void deleteException(Exception exception);

EXPORT void setBufferSize(uint64_t size);

EXPORT void unzip(uint64_t compressionMethod, uint64_t compressedSize, SendDataCallback sendDataCallback, ReceiveDataCallback receiveDataCallback, FinishCallback finishCallback, Exception* exception);
