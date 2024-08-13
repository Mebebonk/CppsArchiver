using System;
using System.Collections.Generic;
using System.Linq;
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
			char[] cbuffer = new char[4];
			using StreamReader sr = new(stream, leaveOpen: true);

			while (true)
			{
				stream.Position = pos;
				sr.Read(cbuffer, 0, cbuffer.Length);

				if (new string(cbuffer) == "PK\u0005\u0006")
				{
					return DeserializeEOCDHeader(stream);
				}

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
			return null!;
		}
	}
}
