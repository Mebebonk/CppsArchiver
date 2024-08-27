using CppsArchiverAPI.LibraryImport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CppsArchiverAPI.source.LibraryImport.DerivedProcessHandlers
{
	internal partial class UnzipHandler(Stream sourceStream, Stream destStream, short compression, ulong size) : BaseProcessHandler(sourceStream, destStream, compression, size)
	{
		#region private_unsafe

		[LibraryImport("ArchiverImplementation", EntryPoint = "unzip")]
		private static unsafe partial void Unzip(
			ulong compressionMethod,
			ulong compressedSize,
			IntPtr sendCallback,
			IntPtr receiveCallback,
			IntPtr finishCallback,
			ref void* exception);
		#endregion

		protected override unsafe void Process(ulong compressionMethod, ulong size, nint sendCallback, nint receiveCallback, nint finishCallback, ref void* exception)
		{
			Unzip(compressionMethod, size, sendCallback, receiveCallback, finishCallback, ref exception);
		}

	}
}
