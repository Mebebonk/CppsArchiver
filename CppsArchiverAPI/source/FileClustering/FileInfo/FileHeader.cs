using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppsArchiverAPI.FileClustering
{
	internal class CDFileHeader(
				int signature,
				short madeBy,
				short minVersion,
				short general,
				short compression,
				short lastModTime,
				short lastModDate,
				int crcUncompressedData,
				int compressedSize,
				int uncompressedSize,
				short fileNameLength,
				short extraFieldLength,
				short commentLength,
				short diskStartNumber,
				short internalAttributes,
				int externalAttributes,
				int relativeOffset,
				string fileName,
				string extraField) : LFileHeader(
					signature,
					minVersion,
					general,
					compression,
					lastModTime,
					lastModDate,
					crcUncompressedData,
					compressedSize,
					uncompressedSize,
					fileNameLength,
					extraFieldLength,
					fileName,
					extraField)
	{
		public short MadeBy { get; } = madeBy;
		public short CommentLength { get; } = commentLength;
		public short DiskStartNumber { get; } = diskStartNumber;
		public short InternalAttributes { get; } = internalAttributes;
		public int ExternalAttributes { get; } = externalAttributes;
		public int RelativeOffset { get; } = relativeOffset;
	}

	internal class LFileHeader(
					int signature,
					short minVersion,
					short general,
					short compression,
					short lastModTime,
					short lastModDate,
					int crcUncompressedData,
					int compressedSize,
					int uncompressedSize,
					short fileNameLength,
					short extraFieldLength,
					string fileName,
					string extraField)
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
