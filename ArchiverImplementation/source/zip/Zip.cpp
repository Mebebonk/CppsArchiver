#include "zip/Zip.h"

#include <algorithm>
#include <stdexcept>
#include <format>

#include "api/defines.h"

namespace zip
{
	void* Zip::decodeUncompressed(const char* encodedData, uint64_t encodedDataSize, uint64_t* decodedDataSize)
	{
		*decodedDataSize = encodedDataSize;

		void* result = new char[encodedDataSize];

		memcpy(result, encodedData, encodedDataSize);

		return result;
	}

	void* Zip::decodeCompressed(const char* encodedData, uint64_t encodedDataSize, uint64_t* decodedDataSize)
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

	void* Zip::decode(const char* encodedData, uint64_t encodedDataSize, uint64_t* decodedDataSize)
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
