#include "zip/Zip.h"

#include <algorithm>
#include <stdexcept>
#include <format>

#include "api/defines.h"

namespace zip
{
	uint8_t* Zip::decodeUncompressed(const uint8_t* encodedData, uint64_t encodedDataSize, uint64_t* decodedDataSize)
	{
		uint8_t* result = new uint8_t[encodedDataSize];

		*decodedDataSize = encodedDataSize;

		std::copy(encodedData, encodedData + encodedDataSize, result);

		return result;
	}

	uint8_t* Zip::decodeCompressed(const uint8_t* encodedData, uint64_t encodedDataSize, uint64_t* decodedDataSize)
	{
		return nullptr;
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

	uint8_t* Zip::decode(const uint8_t* encodedData, uint64_t encodedDataSize, uint64_t* decodedDataSize)
	{
		switch (compressionMethod)
		{
		case 0x0:
			return this->decodeUncompressed(encodedData, encodedDataSize, decodedDataSize);

		case 0x8:
			return this->decodeCompressed(encodedData, encodedDataSize, decodedDataSize);

		default:
			throw std::runtime_error(std::format("Wrong compressionMethod: {}", compressionMethod));
		}

		return nullptr;
	}
}
