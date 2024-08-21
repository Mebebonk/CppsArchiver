using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CppsArchiverAPI
{
	public static class FileManager
	{
		public static CDFileHeader[]? GetCDFileHeaders(Stream stream)
		{
			EOCDHeader? cDpos = FindCentralDirectory(stream);
			if (cDpos == null) { return null; }

			return DeserializeCD(stream, cDpos);
		}
		public static LFileHeader LocateFile(Stream stream, CDFileHeader cDFileHeader)
		{
			stream.Position = cDFileHeader.RelativeOffset;

			return DeserializeLFileHeader(stream);
		}
		private static EOCDHeader? FindCentralDirectory(Stream stream)
		{
			long pos = stream.Length - 22;
			char[] buffer = new char[4];
			using StreamReader sr = new(stream, leaveOpen: true);

			while (true)
			{
				stream.Position = pos;
				sr.Read(buffer);
				string testString = new(buffer);

				if (testString == "PK\u0005\u0006")
				{
					stream.Position = pos;

					return DeserializeEOCDHeader(stream);
				}

				//TODO: PK\u0006\u0006 (EOCD64)

				pos--;
				if (pos < 0)
				{
					return null;
				}

			}
		}

		#region deserializers
		private static CDFileHeader[] DeserializeCD(Stream stream, EOCDHeader eOCDHeader)
		{
			List<CDFileHeader> list = [];
			stream.Position = eOCDHeader.CDOOffset;

			for (int i = 0; i < eOCDHeader.CDOTotalRecordCount; i++)
			{
				list.Add(DeserializeCDFile(stream));
			}

			return [.. list];
		}
		private static CDFileHeader DeserializeCDFile(Stream stream)
		{
			DeserializeHeader(stream, "PK\u0001\u0002");

			short versionMadeBy = DeserializeShort(stream);
			short minVersion = DeserializeShort(stream);
			short GPF = DeserializeShort(stream);
			short compression = DeserializeShort(stream);
			short lastModifiedTime = DeserializeShort(stream);
			short lastModifiedDate = DeserializeShort(stream);

			int CRC32 = DeserializeInt(stream);
			int compressedSize = DeserializeInt(stream);
			int uncompressedSize = DeserializeInt(stream);

			short fileNameLength = DeserializeShort(stream);
			short extraFieldLength = DeserializeShort(stream);
			short fileCommentLength = DeserializeShort(stream);
			short startDiskNo = DeserializeShort(stream);
			short internalFileAttributes = DeserializeShort(stream);

			int exernalFileAttributes = DeserializeInt(stream);
			int relativeOffset = DeserializeInt(stream);

			string fileName = DeserializeString(stream, fileNameLength);
			string extraField = DeserializeString(stream, extraFieldLength);
			string fileComment = DeserializeString(stream, fileCommentLength);

			return new CDFileHeader(versionMadeBy,
									minVersion,
									GPF,
									compression,
									lastModifiedTime,
									lastModifiedDate,
									CRC32,
									compressedSize,
									uncompressedSize,
									fileNameLength,
									extraFieldLength,
									fileCommentLength,
									startDiskNo,
									internalFileAttributes,
									exernalFileAttributes,
									relativeOffset,
									fileName,
									extraField,
									fileComment
				);
		}
		private static EOCDHeader DeserializeEOCDHeader(Stream stream)
		{
			DeserializeHeader(stream, "PK\u0005\u0006");

			short diskNo = DeserializeShort(stream);
			short diskStart = DeserializeShort(stream);
			short currentRecords = DeserializeShort(stream);
			short totalRecords = DeserializeShort(stream);

			int size = DeserializeInt(stream);
			int offset = DeserializeInt(stream);

			short commentSize = DeserializeShort(stream);
			string comment = DeserializeString(stream, commentSize);

			return new(diskNo, diskStart, currentRecords, totalRecords, size, offset, commentSize, comment);
		}
		private static LFileHeader DeserializeLFileHeader(Stream stream)
		{
			//TODO: add field check

			DeserializeHeader(stream, "PK\u0003\u0004");

			short minVersion = DeserializeShort(stream);
			short GPF = DeserializeShort(stream);
			short compression = DeserializeShort(stream);
			short lastModifiedTime = DeserializeShort(stream);
			short lastModifiedDate = DeserializeShort(stream);

			int CRC32 = DeserializeInt(stream);
			int compressedSize = DeserializeInt(stream);
			int uncompressedSize = DeserializeInt(stream);

			short fileNameLength = DeserializeShort(stream);
			short extraFieldLength = DeserializeShort(stream);

			string fileName = DeserializeString(stream, fileNameLength);
			string extraField = DeserializeString(stream, extraFieldLength);

			return new(
				minVersion, 
				GPF, 
				compression, 
				lastModifiedTime, 
				lastModifiedDate, 
				CRC32, 
				compressedSize, 
				uncompressedSize, 
				fileNameLength, 
				extraFieldLength, 
				fileName, 
				extraField);
		}

		private static void DeserializeHeader(Stream stream, string header)
		{
			//TODO: change to explicit exception
			if (DeserializeString(stream, 4) != header) { throw new("invalid signature found"); }
		}
		private static short DeserializeShort(Stream stream)
		{
			byte[] shortBuffer = new byte[2];

			stream.Read(shortBuffer);
			//TODO: add little-endian check
			return BitConverter.ToInt16(shortBuffer, 0);
		}
		private static int DeserializeInt(Stream stream)
		{
			byte[] intBuffer = new byte[4];

			stream.Read(intBuffer);
			//TODO: add little-endian check
			return BitConverter.ToInt32(intBuffer, 0);
		}
		private static string DeserializeString(Stream stream, long size)
		{
			if (size == 0) { return ""; }

			byte[] buffer = new byte[size];
			stream.Read(buffer);

			return new(Encoding.UTF8.GetChars(buffer));
		}
		#endregion
	}
}
