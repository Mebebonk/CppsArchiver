#include "api/c_api.h"

#include "zip/ZipStruct.h"

void unzip(const void* metaInformation, const char* fileName, const char* extraField, GetDataCallback dataCallback, FinishCallback finishCallback)
{
	const zip::ZipHeader* header = static_cast<const zip::ZipHeader*>(metaInformation);


}
