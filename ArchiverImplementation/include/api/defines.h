#pragma once

#include <cstdint>

using SendDataCallback = void* (*)(uint64_t dataSize);
using ReceiveDataCallback = void* (*)(uint64_t size);
using FinishCallback = void (*)();
using Exception = void*;
