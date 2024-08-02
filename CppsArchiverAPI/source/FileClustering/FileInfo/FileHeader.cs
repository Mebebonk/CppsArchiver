using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppsArchiverAPI.source.FileClustering
{
	internal class FileHeader(int signature, short minVersion, short general, short compression, short lastModTime, short lastModDate, int crcUncompressedData, int compressedSize, int uncompressedSize, short fileNameLength, short extraFieldLength, string fileName, string extraField)
	{
		public int Signature { get; } = signature;
		public short MinVersion { get; } = minVersion;
		public short General { get; } = general;
		public short Compression { get; } = compression;
		public short LastModTime { get; } = lastModTime;
		public short LastModDate { get; } = lastModDate;
		public int CrcUncompressedData { get; } = crcUncompressedData;
		public int CompressedSize { get; } = compressedSize;
		public int UncompressedSize { get; } = uncompressedSize;
		public short FileNameLength { get; } = fileNameLength;
		public short ExtraFieldLength { get; } = extraFieldLength;
		public string FileName { get; } = fileName;
		public string ExtraField { get; } = extraField;
	}
}
