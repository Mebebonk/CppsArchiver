using CppsArchiverAPI;
using CppsArchiverAPI.LibraryImport;
using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace CppsArchiverCLI
{
	internal class Program
	{
		static void Main(string? input, string? output)
		{
			if (input == null)
			{
				Console.WriteLine("No input file was found!");

				return;
			}

			using Stream stream = File.OpenRead(input);

			CDFileHeader[] files = CppsArchiverAPI.CppsArchiverAPI.GetCDFileHeaders(stream);

			Parallel.ForEach(files, (x) => UnzipSingle(x, stream, input, output));			
		}

		private static void UnzipSingle(CDFileHeader file, Stream stream, string input, string? output)
		{
			string path = Path.GetDirectoryName(file.FileName) ?? "";

			if (output != null)
			{
				path = Path.Combine(output, Path.GetFileNameWithoutExtension(input), path);
			}
			else
			{
				path = Path.Combine(Path.GetDirectoryName(input) ?? "", Path.GetFileNameWithoutExtension(input), path);
			}

			if (!String.IsNullOrWhiteSpace(path))
			{
				Directory.CreateDirectory(path);
			}

			using FileStream fs = File.Open(Path.Combine(path, file.FileName), FileMode.Create);

			CppsArchiverAPI.CppsArchiverAPI.ProcessFile<UnzipHandler>(stream, fs, file);
		}
	}
}
