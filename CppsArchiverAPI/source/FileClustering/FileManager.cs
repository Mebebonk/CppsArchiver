using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppsArchiverAPI.FileClustering
{
	internal static class FileManager
	{
		public static LFileHeader[]? ZipLookup(Stream stream)
		{

			return null;
		}

		private static long FindCentralDirectory(Stream stream)
		{
			long pos = stream.Length - 4;
			byte[] buffer = new byte[4];

			while (true)
			{
				stream.Position = pos;
				stream.Read(buffer, 0, buffer.Length);

				if (buffer.SequenceEqual((byte[])[80, 75, 1, 2]))
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

		private static LFileHeader[] DeserializeZip(Stream stream, long CDStart)
		{
			return null!;
		}

	}
}
