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
    public struct WIZTreeCompoment
    {
        private const string Name = "model.tree";
        public IntPtr ptr;

        public static implicit operator WIZTreeCompoment(IntPtr p) { return new WIZTreeCompoment() { ptr = p }; }


        /// <summary>
        /// 组件获取方法
        /// </summary>
        /// <param name="player">目标Player</param>
        /// <returns>需要判断是否成功</returns>
        /// <seealso cref="isValid"/>
        public static WIZTreeCompoment From(WIZPlayer player) { return player.getCompoment(Name); }
        

        /// <summary>
        /// 判断该组件是否存在
        /// </summary>
        /// <returns></returns>
        public bool isValid() { return ptr != IntPtr.Zero; }


        /// <summary>
        /// 设置父窗口
        /// </summary>
        /// <param name="parent">父窗口句柄</param>
        public void setParent(IntPtr parent) { wizTreeNative.plugin_tree_set_parent(ptr, parent); }


        /// <summary>
        /// 设置显示区域
        /// </summary>
        /// <param name="x">left</param>
        /// <param name="y">top</param>
        /// <param name="w">width</param>
        /// <param name="h">height</param>
        public void setRect(int x, int y, int w, int h) { wizTreeNative.plugin_tree_set_rect(ptr, x, y, w, h); }

    }


    #region C++层导出的接口原形

    /// <summary>
    /// C++层导出的接口原形
    /// </summary>
    public class wizTreeNative
    {
        public const string DLLFILE = "wizPlayerEngine";


        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void plugin_tree_set_parent(IntPtr tree, IntPtr parent);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void plugin_tree_set_rect(IntPtr tree, int x, int y, int w, int h);
    }

    #endregion
}
