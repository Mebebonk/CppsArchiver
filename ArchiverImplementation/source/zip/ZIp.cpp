#include "zip/Zip.h"

namespace zip
{
	Zip::Zip(uint64_t compressionMethod, uint64_t compressedSize) :
		compressionMethod(compressionMethod),
		compressedSize(compressedSize)
	{

	}

	void* Zip::encode(void* data, uint64_t dataSize)
	{
		return nullptr;
	}

	void* Zip::decode(const char* encodedData, uint64_t encodedDataSize)
	{
		return nullptr;
	}
}
