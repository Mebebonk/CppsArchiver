using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppsArchiverAPI.FileClustering
{
	public class LFileHeader(
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
	public class CDFileHeader(
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

	public class EOCDHeader(
		short currentDiskNumber, 
		short cDODiskStart, 
		short currentDiskCDORecordCount, 
		short cDOTotalRecordCount, 
		int cDOSize,
		int cDOOffset, 
		short commentLength, 
		string comment)
	{
		public short CurrentDiskNumber { get; } = currentDiskNumber;
		public short CDODiskStart { get; } = cDODiskStart;
		public short CurrentDiskCDORecordCount { get; } = currentDiskCDORecordCount;
		public short CDOTotalRecordCount { get; } = cDOTotalRecordCount;
		public int CDOSize { get; } = cDOSize;
		public int CDOOffset { get; } = cDOOffset;
		public short CommentLength { get; } = commentLength;
		public string Comment { get; } = comment;
	}

}
