#pragma once

#include <cstdint>

namespace decoding
{
	class IDecoder
	{
	public:
		virtual void* decode(const char* encodedData, uint64_t encodedDataSize) = 0;

		virtual ~IDecoder() = default;
	};
}
