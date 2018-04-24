using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

#pragma warning disable CS1591
#pragma warning disable IDE1006

namespace wiz
{

    /// <summary>
    /// WIZTool，包含了Tool的句柄，和其方法的包装
    /// </summary>
    public struct WIZTool
    {
        public IntPtr ptr;

        public static implicit operator WIZTool(IntPtr p) { return new WIZTool() { ptr = p }; }

        public bool isValid() { return ptr != IntPtr.Zero; }

        /// <summary>
        /// 获取工具的分组名称
        /// </summary>
        /// <param name="lev">名称级别，可能有多级分组</param>
        /// <returns>名称</returns>
        public string getGroupName(int lev = 0)
        {
            StringBuilder s = new StringBuilder(128);
            wizToolNative.player_get_toolGroupName(ptr, s, lev);
            return s.ToString();
        }

        /// <summary>
        /// 获取工具的显示名称
        /// </summary>
        /// <param name="lev">名称级别，可能有多级分组</param>
        /// <returns>名称</returns>
        public string getDisplayName(int lev = 0)
        {
            StringBuilder s = new StringBuilder(128);
            wizToolNative.player_get_toolDisplayName(ptr, s, lev);
            return s.ToString();
        }

        /// <summary>
        /// 获取工具的名称
        /// </summary>
        /// <returns></returns>
        public string getName()
        {
            StringBuilder s = new StringBuilder(128);
            wizToolNative.player_get_toolName(ptr, s);
            return s.ToString();
        }
    }

    #region C++层导出的接口原形

    /// <summary>
    /// C++层导出的接口原形
    /// </summary>
    class wizToolNative
    {
        public const string DLLFILE = "wizPlayerEngine";

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern int player_get_toolGroupName(IntPtr tool, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder outGroupName, int lev);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern int player_get_toolDisplayName(IntPtr tool, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder outDisplayName, int lev);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern int player_get_toolName(IntPtr tool, [MarshalAs(UnmanagedType.LPStr)] StringBuilder outName);
    }

    #endregion
}
