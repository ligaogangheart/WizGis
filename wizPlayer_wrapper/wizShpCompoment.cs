using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

#pragma warning disable CS1591

namespace wiz
{

    /// <summary>
    /// Shape组件
    /// </summary>
    public struct WIZShpCompoment
    {
        public const string Name = "plugin.tileShape";
        public IntPtr ptr;

        public static implicit operator WIZShpCompoment(IntPtr p) { return new WIZShpCompoment() { ptr = p }; }


        /// <summary>
        /// 组件获取方法
        /// </summary>
        /// <param name="player">目标Player</param>
        /// <returns>需要判断是否成功</returns>
        /// <see cref="isValid"/>
        public static WIZShpCompoment From(WIZPlayer player) { return player.getCompoment(Name); }


        /// <summary>
        /// 判断该组件是否存在
        /// </summary>
        /// <returns></returns>
        public bool isValid() { return ptr != IntPtr.Zero; }


        /// <summary>
        /// 加载Shape对象
        /// </summary>
        /// <param name="file">.shp文件</param>
        /// <returns>需要判断是否成功</returns>
        /// <see cref="ShapeObject.isValid"/>
        public ShapeObject load(string file) { return wizShpNative.shp_load(ptr, file); }

    }



    /// <summary>
    /// Shape对象
    /// </summary>
    public struct ShapeObject
    {
        public IntPtr ptr;
        public static implicit operator ShapeObject(IntPtr p) { return new ShapeObject() { ptr = p }; }

        /// <summary>
        /// 判断该组件是否存在
        /// </summary>
        /// <returns></returns>
        public bool isValid() { return ptr != IntPtr.Zero; }


        /// <summary>
        /// 释放Shape对象
        /// </summary>
        public void release() { wizShpNative.shp_release(ptr); }


        /// <summary>
        /// 获取数据字段个数
        /// </summary>
        /// <returns></returns>
        public int getFieldCount() { return wizShpNative.shp_get_fieldCount(ptr); }


        /// <summary>
        /// 获取数据字段名称
        /// </summary>
        /// <param name="fieldIdx">字段索引</param>
        /// <returns></returns>
        public string getFieldName(int fieldIdx)
        {
            StringBuilder s = new StringBuilder(128);
            wizShpNative.shp_get_fieldName(ptr, fieldIdx, s);
            return s.ToString();
        }


    }



    #region C++层导出的接口原形

    /// <summary>
    /// C++层导出的接口原形
    /// </summary>
    class wizShpNative
    {
        public const string DLLFILE = "plugin/component/plugin.tileShape.dll";

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr shp_load(IntPtr plugin, string fileName);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void   shp_release(IntPtr shpObj);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern int    shp_get_fieldCount(IntPtr shpObj);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void   shp_get_fieldName(IntPtr shpObj, int index, [MarshalAs(UnmanagedType.LPStr)] StringBuilder outName);
        

    }

    #endregion
}
