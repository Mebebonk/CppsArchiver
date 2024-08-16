#pragma once

#include <cstdint>

using SendDataCallback = void (*)(const void* data, uint64_t dataSize);
using ReceiveDataCallback = void* (*)(uint64_t size);
using FinishCallback = void (*)();
using Exception = void*;

inline uint64_t bufferSize = 1 * 1024 * 1024;
