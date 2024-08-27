using CppsArchiverAPI.LibraryImport;
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
	public static partial class CppArchiverAPI
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

		public delegate void SendDataCallback(IntPtr data, ulong dataSize);

		public delegate IntPtr ReceiveDataCallback(ulong size);

		public delegate void FinishCallback();

		#endregion

		public static void ProcessFileUnzip(
			ulong compressionMethod,
			ulong compressedSize,
			ProcessHandler processHandler)
		{			
			unsafe
			{
				void* exception = null;
				Unzip(
					compressionMethod,
					compressedSize,
					Marshal.GetFunctionPointerForDelegate((SendDataCallback)processHandler.Receive),
					Marshal.GetFunctionPointerForDelegate((ReceiveDataCallback)processHandler.Throw),
					Marshal.GetFunctionPointerForDelegate((FinishCallback)processHandler.Finish),
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
