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

		while (requestSize)
		{
			void* data = receiveDataCallback(requestSize);
			uint64_t resultSize = 0;
			void* result = decoder.decode(static_cast<const char*>(data), requestSize, &resultSize);

			sendDataCallback(result, resultSize);

			delete[] result;

			requestSize = decoder.request();
		}

		finishCallback();
	}
}
