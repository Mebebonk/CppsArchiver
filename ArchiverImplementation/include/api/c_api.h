#pragma once

#include <cstdint>

#ifdef __LINUX__
#define EXPORT extern "C" __attribute__((visibility("default")))
#else
#define EXPORT extern "C" __declspec(dllexport)
#endif

typedef const char* (*GetDataCallback)(uint64_t size);
typedef void (*FinishCallback)();

EXPORT void unzip(const void* metaInformation, const char* fileName, const char* extraField, GetDataCallback dataCallback, FinishCallback finishCallback);
