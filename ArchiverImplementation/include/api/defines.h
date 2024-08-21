#pragma once

#include <cstdint>

using SendDataCallback = void (*)(const uint8_t* data, uint64_t dataSize);
using ReceiveDataCallback = const uint8_t* (*)(uint64_t size);
using FinishCallback = void (*)();
using Exception = void*;

inline uint64_t bufferSize = 1 * 1024 * 1024;
