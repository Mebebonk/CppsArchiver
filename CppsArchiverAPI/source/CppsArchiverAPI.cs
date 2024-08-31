using CppsArchiverAPI.LibraryImport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppsArchiverAPI
{
	static public class CppsArchiverAPI
	{

		public static void ProcessFile<T>(Stream inStream, Stream outStream, CDFileHeader header) where T : BaseProcessHandler
		{
			FileManager.LocateFile(inStream, header);
			//TODO: Add explicit exception
			T uh = (T)Activator.CreateInstance(typeof(T), [inStream, outStream, header.Compression, (ulong)header.CompressedSize])!;

			uh.ProcessFile();
		}

		public static CDFileHeader[] GetCDFileHeaders(Stream inStream)
		{
			//TODO: Add explicit exception
			return FileManager.GetCDFileHeaders(inStream) ?? throw new("Couldn't find CD");
		}
	}
}
