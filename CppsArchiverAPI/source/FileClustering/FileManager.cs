using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CppsArchiverAPI.FileClustering
{
	public static class FileManager
	{
		public static CDFileHeader[]? GetCDFileHeaders(Stream stream)
		{
			EOCDHeader? cDpos = FindCentralDirectory(stream);
			if (cDpos == null) { return null; }

			return DeserializeCD(stream, cDpos);
		}

		private static EOCDHeader? FindCentralDirectory(Stream stream)
		{
			long pos = stream.Length - 22;
			char[] buffer = new char[4];
			using StreamReader sr = new(stream, leaveOpen: true);

			while (true)
			{
				stream.Position = pos;
				sr.Read(buffer, 0, buffer.Length);
				string testString = new(buffer);

				if (testString == "PK\u0005\u0006")
				{
					stream.Position = pos + 4;
					//byte[] bytes = new byte[stream.Length - stream.Position];
					//stream.Read(bytes, 0, bytes.Length);

					return DeserializeEOCDHeader(stream);
				}

				//TODO: PK\u0006\u0006

				pos--;
				if (pos < 0)
				{
					return null;
				}

			}
		}

		private static CDFileHeader[] DeserializeCD(Stream stream, EOCDHeader CDStart)
		{
			return null!;
		}

		private static EOCDHeader DeserializeEOCDHeader(Stream stream)
		{
			short diskNo = DeserializeShort(stream);
			short diskStart = DeserializeShort(stream);
			short currentRecords = DeserializeShort(stream);
			short totalRecords = DeserializeShort(stream);

			int size = DeserializeInt(stream);
			int offset = DeserializeInt(stream);

			short commentSize = DeserializeShort(stream);

			char[] cbuffer = new char[commentSize];

			StreamReader sr = new(stream, leaveOpen: true);
			sr.Read(cbuffer, 0, cbuffer.Length);
			sr.Close();

			string comment = new(cbuffer);

			return new(diskNo, diskStart, currentRecords, totalRecords, size, offset, commentSize, comment);
		}

		private static short DeserializeShort(Stream stream)
		{
			byte[] shortBuffer = new byte[2];

			stream.Read(shortBuffer, 0, 2);
			//TODO: add little-endian check
			return BitConverter.ToInt16(shortBuffer, 0);
		}
		private static int DeserializeInt(Stream stream)
		{
			byte[] intBuffer = new byte[4];

			stream.Read(intBuffer, 0, 4);
			//TODO: add little-endian check
			return BitConverter.ToInt32(intBuffer, 0);
		}
	}
}
