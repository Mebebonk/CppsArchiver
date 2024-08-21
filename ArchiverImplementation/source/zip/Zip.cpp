#include "zip/Zip.h"

#include <algorithm>
#include <stdexcept>
#include <format>

#include "api/defines.h"

namespace zip
{
	void Zip::decodeUncompressed(const uint8_t* encodedData, uint64_t encodedDataSize, std::vector<uint8_t>& buffer, uint64_t& decodedDataSize)
	{
		decodedDataSize = encodedDataSize;

		std::copy(encodedData, encodedData + encodedDataSize, buffer.data());
	}

	void Zip::decodeCompressed(const uint8_t* encodedData, uint64_t encodedDataSize, std::vector<uint8_t>& buffer, uint64_t& decodedDataSize)
	{
		
	}

	Zip::Zip(uint64_t compressionMethod, uint64_t compressedSize) :
		compressionMethod(compressionMethod),
		compressedSize(compressedSize)
	{

	}

	uint64_t Zip::request()
	{
		if (compressionMethod)
		{
			return 0;
		}

		uint64_t size = std::min(compressedSize, bufferSize);

		compressedSize -= size;

		return size;
	}

	void Zip::decode(const uint8_t* encodedData, uint64_t encodedDataSize, std::vector<uint8_t>& buffer, uint64_t& decodedDataSize)
	{
		switch (compressionMethod)
		{
		case 0x0:
			this->decodeUncompressed(encodedData, encodedDataSize, buffer, decodedDataSize);

			break;

		case 0x8:
			this->decodeCompressed(encodedData, encodedDataSize, buffer, decodedDataSize);

			break;

		default:
			throw std::runtime_error(std::format("Wrong compressionMethod: {}", compressionMethod));
		}
	}
}
