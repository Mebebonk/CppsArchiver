#pragma once

#include "Decoder.h"
#include "api/defines.h"

namespace decoding
{
	template<Decoder T, typename... Args>
	void decode(SendDataCallback sendDataCallback, ReceiveDataCallback receiveDataCallback, FinishCallback finishCallback, Args&&... args)
	{
		T decoder(std::forward<Args>(args)...);

		uint64_t requestSize = decoder.request();
		std::vector<uint8_t> buffer(bufferSize);

		while (requestSize)
		{
			const uint8_t* data = receiveDataCallback(requestSize);
			uint64_t resultSize = 0;
			
			decoder.decode(data, requestSize, buffer, resultSize);

			sendDataCallback(buffer.data(), resultSize);

			requestSize = decoder.request();
		}

		finishCallback();
	}
}
