using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace CppsArchiverAPI.LibraryImport
{
	internal abstract partial class BaseProcessHandler(Stream sourceStream, Stream destStream, short compression, ulong size)
	{

		#region delegates

		private delegate void SendDataCallback(IntPtr data, ulong dataSize);

		private delegate IntPtr ReceiveDataCallback(ulong size);

		private delegate void FinishCallback();

		protected unsafe delegate void Process(ulong compressionMethod,
			ulong size,
			IntPtr sendCallback,
			IntPtr receiveCallback,
			IntPtr finishCallback,
			ref void* exception);

		#endregion

		#region private_unsafe 

		[LibraryImport("ArchiverImplementation", EntryPoint = "getExceptionMessage", StringMarshalling = StringMarshalling.Utf8)]
		private static unsafe partial string GetExceptionMessage(void* exception);

		[LibraryImport("ArchiverImplementation", EntryPoint = "deleteException")]
		private static unsafe partial void DeleteException(void* exception);

		[LibraryImport("ArchiverImplementation", EntryPoint = "setBufferSize")]
		private static partial void SetBufferSize(ulong size);

		#endregion

		private readonly Stream sourceStream = sourceStream;
		private readonly Stream destStream = destStream;

		private readonly short compression = compression;
		private readonly ulong size = size;

		private readonly List<GCHandle> gCHandles = [];

		private void Receive(IntPtr data, ulong dataSize)
		{
			byte[] buffer = new byte[dataSize];
			Marshal.Copy(data, buffer, 0, buffer.Length);

			destStream.Write(buffer);
		}

		private IntPtr Throw(ulong size)
		{
			byte[] buffer = new byte[size];
			sourceStream.Read(buffer);

			GCHandle pinnedArray = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			IntPtr pointer = pinnedArray.AddrOfPinnedObject();

			gCHandles.Add(pinnedArray);

			return pointer;
		}

		private void Finish()
		{
			foreach (GCHandle h in gCHandles)
			{
				h.Free();
			}
		}

		protected abstract Process GetProcess();		

		internal void ProcessFile()
		{
			unsafe
			{
				void* exception = null;

				GetProcess()((ulong)compression,
					size,
					Marshal.GetFunctionPointerForDelegate((SendDataCallback)Receive),
					Marshal.GetFunctionPointerForDelegate((ReceiveDataCallback)Throw),
					Marshal.GetFunctionPointerForDelegate((FinishCallback)Finish),
					ref exception);

				if (exception != null)
				{
					string message = GetExceptionMessage(exception);
					DeleteException(exception);

					// TODO: change to explicit exception

					throw new(message);
				}
			}
		}
	}
}
