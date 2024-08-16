#pragma once

#include <cstdint>

namespace encoding
{
	class IEncoder
	{
	public:
		virtual void* encode(void* data, uint64_t dataSize) = 0;

		virtual ~IEncoder() = default;
	};
}
