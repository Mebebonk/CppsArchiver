using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CppsArchiverAPI.LibraryImport
{
	public class ZipHandler(Stream sourceStream, Stream destStream, short compression, ulong size) : BaseProcessHandler(sourceStream, destStream, compression, size)
	{
		#region private_unsafe

		//[LibraryImport("ArchiverImplementation", EntryPoint = "zip")]
		//private static unsafe partial void Zip(
		//	ulong compressionMethod,
		//	ulong compressedSize,
		//	IntPtr sendCallback,
		//	IntPtr receiveCallback,
		//	IntPtr finishCallback,
		//	ref void* exception);

		#endregion

		protected override unsafe Process GetProcess()
		{
			throw new NotImplementedException();
			//return Zip;
		}
	}
}



