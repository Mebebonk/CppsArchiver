using CppsArchiverAPI.LibraryImport;
using CppsArchiverAPI.LibraryImport.DerivedProcessHandlers;
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
			T uh = (T)Activator.CreateInstance(typeof(T), [inStream, outStream, header.Compression, (ulong)header.CompressedSize])!;
			//UnzipHandler uh = new(inStream, outStream, header.Compression, (ulong)header.CompressedSize);

			uh.ProcessFile();
			return;
		}

		public static CDFileHeader[] GetCDFileHeaders(Stream inStream)
		{
			//TODO: Add explicit exception
			return FileManager.GetCDFileHeaders(inStream) ?? throw new("Couldn't find CD");
		}
	}
}
