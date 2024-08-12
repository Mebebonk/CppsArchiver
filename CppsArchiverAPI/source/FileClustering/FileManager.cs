using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppsArchiverAPI.FileClustering
{
	internal static class FileManager
	{
		public static CDFileHeader[]? GetCDFileHeaders(Stream stream)
		{
			long cDpos = FindCentralDirectory(stream);
			if(cDpos == -1) { return null; }

			return DeserializeCD(stream, cDpos);
		}

		private static long FindCentralDirectory(Stream stream)
		{
			long pos = stream.Length - 22;
			byte[] buffer = new byte[4];

			while (true)
			{
				stream.Position = pos;
				stream.Read(buffer, 0, buffer.Length);

				if (buffer.SequenceEqual((byte[])[80, 75, 5, 6]))
				{
					return pos;
				}

				pos--;
				if (pos < 0)
				{
					return -1;
				}

			}
		}

		private static CDFileHeader[] DeserializeCD(Stream stream, long CDStart)
		{
			return null!;
		}

		private static CDFileHeader DeserializeCDHeader(Stream stream)
		{

		}
	}
}
