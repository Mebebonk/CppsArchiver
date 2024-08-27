using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CppsArchiverAPI.LibraryImport
{
	public class ProcessHandler(Stream sourceStream, Stream destStream)
	{
		public Stream SourceStream { get; } = sourceStream;
		public Stream DestStream { get; } = destStream;

		private readonly List<GCHandle> gCHandles = [];

		public void Receive(IntPtr data, ulong dataSize)
		{			
			unsafe
			{
				DestStream.Write(new(data.ToPointer(), (int)dataSize));
			}
		}

		public IntPtr Throw(ulong size)
		{
			byte[] buffer = new byte[size];
			SourceStream.Read(buffer);

			GCHandle pinnedArray = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			IntPtr pointer = pinnedArray.AddrOfPinnedObject();

			gCHandles.Add(pinnedArray);

			return pointer;
		}

		public void Finish()
		{
			foreach (GCHandle h in gCHandles)
			{
				h.Free();
			}
		}
	}
}
