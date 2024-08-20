#pragma once

#include <cstdint>

namespace zip
{
	class Zip
	{
	private:
		uint64_t compressionMethod;
		uint64_t compressedSize;

	private:
		uint8_t* decodeUncompressed(const uint8_t* encodedData, uint64_t encodedDataSize, uint64_t* decodedDataSize);

		uint8_t* decodeCompressed(const uint8_t* encodedData, uint64_t encodedDataSize, uint64_t* decodedDataSize);

	public:
		Zip(uint64_t compressionMethod, uint64_t compressedSize);

		uint64_t request();

		uint8_t* decode(const uint8_t* encodedData, uint64_t encodedDataSize, uint64_t* decodedDataSize);

		~Zip() = default;
	};
}
