#pragma once

#include "interfaces/IEncoder.h"
#include "interfaces/IDecoder.h"

namespace zip
{
	class Zip : 
		public encoding::IEncoder,
		public decoding::IDecoder
	{
	private:
		uint64_t compressionMethod;
		uint64_t compressedSize;


	public:
		Zip(uint64_t compressionMethod, uint64_t compressedSize);

		void* encode(void* data, uint64_t dataSize) override;

		void* decode(const char* encodedData, uint64_t encodedDataSize) override;

		~Zip() = default;
	};
}
