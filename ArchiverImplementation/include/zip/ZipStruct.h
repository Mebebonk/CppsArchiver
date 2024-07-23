#pragma once

#include <cstdint>

namespace zip
{
#pragma pack(push, 1)
	struct ZipHeader
	{
		uint16_t version;
		uint16_t generalPurpose;
		uint16_t compressionMethod;
		uint16_t lastModificationTime;
		uint16_t lastModificationDate;
		uint32_t crc32;
		uint32_t compressedSize;
		uint32_t uncompressedSize;
		uint16_t fileNameLength;
		uint16_t extraFieldLength;
	};
#pragma pack(pop)
}
