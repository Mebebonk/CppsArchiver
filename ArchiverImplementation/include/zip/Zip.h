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
		void* decodeUncompressed(const char* encodedData, uint64_t encodedDataSize, uint64_t* decodedDataSize);

		void* decodeCompressed(const char* encodedData, uint64_t encodedDataSize, uint64_t* decodedDataSize);

	public:
		Zip(uint64_t compressionMethod, uint64_t compressedSize);

		uint64_t request();

		void* decode(const char* encodedData, uint64_t encodedDataSize, uint64_t* decodedDataSize);

		~Zip() = default;
	};
}
