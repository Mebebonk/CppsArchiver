using CppsArchiverAPI;
using CppsArchiverAPI.LibraryImport;
using System;
using System.Collections;
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

			// TODO: Parallel.ForEach
			foreach (var file in files)
			{
				List<string> list = [.. file.FileName.Split(Path.DirectorySeparatorChar)];
				list.RemoveAt(list.Count - 1);

				string path = String.Join(Path.DirectorySeparatorChar, list);

				if (output != null)
				{
					path = $"{output}{Path.DirectorySeparatorChar}{path}";
				}
				else
				{
					path = $"{$@".{Path.DirectorySeparatorChar}{input.Split('.')[^2]}\"}{Path.DirectorySeparatorChar}{path}";
				}

				if (!String.IsNullOrWhiteSpace(path))
				{
					Directory.CreateDirectory(path);
				}

				using FileStream fs = File.Open(path + file.FileName, FileMode.Create);

				CppsArchiverAPI.CppsArchiverAPI.ProcessFile<UnzipHandler>(stream, fs, file);
			}
		}
	}
}
