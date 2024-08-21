using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CppsArchiverAPI
{
	internal static partial class CppArchiverAPI
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
		[LibraryImport("ArchiverImplementation", EntryPoint = "getExceptionMessage", StringMarshalling = StringMarshalling.Utf8)]
		private static unsafe partial string GetExceptionMessage(void* exception);

		[LibraryImport("ArchiverImplementation", EntryPoint = "deleteException")]
		private static unsafe partial void DeleteException(void* exception);

		[LibraryImport("ArchiverImplementation", EntryPoint = "setBufferSize")]
		private static partial void SetBufferSize(ulong size);
		#endregion

		#region delegates
		internal delegate void SendDataCallback(byte[] data, ulong dataSize);

		internal delegate byte[] ReceiveDataCallback(ulong size);

		internal delegate void FinishCallback();
		#endregion

		internal static void ProcessFileUnzip(
			ulong compressionMethod,
			ulong compressedSize,
			SendDataCallback sendCallback,
			ReceiveDataCallback receiveCallback,
			FinishCallback finishCallback)
		{
			unsafe
			{
				void* exception = null;
				Unzip(
					compressionMethod,
					compressedSize,
					Marshal.GetFunctionPointerForDelegate(sendCallback),
					Marshal.GetFunctionPointerForDelegate(receiveCallback),
					Marshal.GetFunctionPointerForDelegate(finishCallback),
					ref exception);
				if (exception != null)
				{
					string message = GetExceptionMessage(exception);
					DeleteException(exception);

					throw new(message);
				}
			}
		}
	}
}
