#pragma once

#include <cstdint>
#include <vector>

namespace zip
{
	class Zip
	{
	private:
		uint64_t compressionMethod;
		uint64_t compressedSize;

	private:
		void decodeUncompressed(const uint8_t* encodedData, uint64_t encodedDataSize, std::vector<uint8_t>& buffer, uint64_t& decodedDataSize);

		void decodeCompressed(const uint8_t* encodedData, uint64_t encodedDataSize, std::vector<uint8_t>& buffer, uint64_t& decodedDataSize);

	public:
		Zip(uint64_t compressionMethod, uint64_t compressedSize);

		uint64_t request();

		void decode(const uint8_t* encodedData, uint64_t encodedDataSize, std::vector<uint8_t>& buffer, uint64_t& decodedDataSize);

		~Zip() = default;
	};
}
