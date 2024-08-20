#pragma once

#include <cstdint>
#include <concepts>

namespace decoding
{
	template<typename T>
	concept Decoder = requires(T decoder)
	{
		{ decoder.decode(static_cast<const uint8_t*>(nullptr), int64_t(), static_cast<uint64_t*>(nullptr)) } -> std::same_as<uint8_t*>;
		{ decoder.request() } -> std::same_as<uint64_t>;
	};
}
