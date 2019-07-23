using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace RocketMQ.Client.Internal
{
    internal class FunctionLoader : IFunctionLoader
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport("Kernel32.dll")]
        internal static extern IntPtr LoadLibrary(string path);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr hModule);

        private static readonly IDictionary<string, Delegate> _delegateCache = new ConcurrentDictionary<string, Delegate>();

        private HandleRef _moduleHandle;

        public FunctionLoader() : this(null)
        {

        }

        public FunctionLoader(string nativeFilePath)
        {
            if (this._moduleHandle.Handle != IntPtr.Zero) return;

            if (string.IsNullOrWhiteSpace(nativeFilePath))
            {
                nativeFilePath = GetNativeFilePath();
            }

            if (!File.Exists(nativeFilePath)) throw new FileNotFoundException($"rocketmq cpp sdk not found in path: {nativeFilePath} .");

            var handle = LoadLibrary(nativeFilePath);
            if (handle == IntPtr.Zero)
            {
                throw new ArgumentException($"failed to load {nativeFilePath}");
            }
            this._moduleHandle = new HandleRef(this, handle);
        }

        public TDelegate LoadFunction<TDelegate>(string functionName) where TDelegate : Delegate
        {
            if(_delegateCache.TryGetValue(functionName, out var @delegate))
            {
                return @delegate as TDelegate;
            }

            var functionAddress = GetProcAddress(this._moduleHandle.Handle, functionName);
            @delegate = Marshal.GetDelegateForFunctionPointer(functionAddress, typeof(TDelegate));

            _delegateCache.Add(functionName, @delegate);

            return @delegate as TDelegate;
        }

        public void Free()
        {
            if (this._moduleHandle.Handle == IntPtr.Zero) return;

            FreeLibrary(this._moduleHandle.Handle);

            this._moduleHandle = new HandleRef(null, IntPtr.Zero);
        }

        private static string GetNativeFilePath()
        {
            var arch = IntPtr.Size == 4 ? "x86" : "x64";

            Console.WriteLine($"---------------------arch:{arch}");

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "native", arch, "rocketmq-client-cpp.dll");
        }

        //public static Delegate LoadFunction<TDelegate>(string dllPath, string functionName)
        //{
        //    var hModule = LoadLibrary(dllPath);
        //    var functionAddress = GetProcAddress(hModule, functionName);
        //    return Marshal.GetDelegateForFunctionPointer(functionAddress, typeof(TDelegate));
        //}
    }
}
