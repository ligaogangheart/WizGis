using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

#pragma warning disable CS1591

namespace wiz
{
    /// <summary>
    /// 模型树组件
    /// </summary>
    public struct WIZObliquePhotograph
    {
        private const string Name = "plugin.obliquePhotograph";
        public IntPtr ptr;

        public static implicit operator WIZObliquePhotograph(IntPtr p) { return new WIZObliquePhotograph() { ptr = p }; }

        /// <summary>
        /// 组件获取方法
        /// </summary>
        /// <param name="player">目标Player</param>
        /// <returns>需要判断是否成功</returns>
        /// <seealso cref="isValid"/>
        public static WIZObliquePhotograph From(WIZPlayer player) { return player.getCompoment(Name); }
        
        /// <summary>
        /// 判断该组件是否存在
        /// </summary>
        /// <returns></returns>
        public bool isValid() { return ptr != IntPtr.Zero; }


        /// <summary>
        /// 加载倾斜摄影数据
        /// </summary>
        /// <param name="fileName">文件名称</param>
        public bool load(string fileName)
        {
            return nativeObliquePhotograph.load(ptr, fileName);
        }


        /// <summary>
        /// 卸载倾斜摄影数据
        /// </summary>
        public void unload()
        {
            nativeObliquePhotograph.unload(ptr);
        }

        /// <summary>
        /// 卸载倾斜摄影数据
        /// </summary>
        public aabb3df getAabb()
        {
            aabb3df aabb    =   new aabb3df();
            nativeObliquePhotograph.getAabb(ptr,ref aabb);
            return aabb;
        }

        /// <summary>
        /// 卸载倾斜摄影数据
        /// </summary>
        public aabb3dr getAbsAabb()
        {
            aabb3dr aabb = new aabb3dr();
            nativeObliquePhotograph.getAbsAabb(ptr, ref aabb);
            return aabb;
        }
        /// <summary>
        /// calcIntersect
        /// </summary>
        public bool calcIntersect(RayR ray, ref real3 result)
        {
            return nativeObliquePhotograph.calcIntersect(ptr, ray,ref result);
        }
        /// <summary>
        /// calcIntersect
        /// </summary>
        public void moveCameraTo()
        {
            nativeObliquePhotograph.moveCameraTo(ptr);
        }
    }


    #region C++层导出的接口原形

    /// <summary>
    /// C++层导出的接口原形
    /// </summary>
    public class nativeObliquePhotograph
    {
        public const string DLLFILE = "plugin/component/plugin.obliquePhotograph.dll";


        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool   load(IntPtr obj, string fileName);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void   unload(IntPtr obj);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void getAbsAabb(IntPtr obj,ref aabb3dr aabb);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void getAabb(IntPtr obj,ref aabb3df aabb);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool calcIntersect(IntPtr obj, RayR ray, ref real3 result);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool moveCameraTo(IntPtr obj);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void setVisible(IntPtr obj,bool vis);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool isVisible(IntPtr obj);

    }

    #endregion
}
