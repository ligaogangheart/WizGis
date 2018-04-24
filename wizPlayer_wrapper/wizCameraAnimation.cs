using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

#pragma warning disable CS1591

namespace wiz
{
    /// <summary>
    /// 相机动画组件
    /// </summary>
    public class WIZCameraAnimation
    {
        public const string Name = "plugin.cameraAnimation";
        public const string ToolName = "system.ToolCamreaAnimation";
        public IntPtr ptr;

        public static implicit operator WIZCameraAnimation(IntPtr p) { return new WIZCameraAnimation() { ptr = p }; }


        /// <summary>
        /// 组件获取方法
        /// </summary>
        /// <param name="player">目标Player</param>
        /// <returns>需要判断是否成功</returns>
        /// <seealso cref="isValid"/>
        public static WIZCameraAnimation From(WIZPlayer player) { return player.getCompoment(Name); }


        /// <summary>
        /// 判断该组件是否存在
        /// </summary>
        /// <returns></returns>
        public bool isValid() { return ptr != IntPtr.Zero; }


        /// <summary>
        /// 设置编辑状态
        /// </summary>
        /// <param name="state">编辑状态</param>
        public void setEdit(bool state) { wizCameraAnimationNative.cam_anim_setEdit(ptr, state); }


        /// <summary>
        /// 创建虚拟相机
        /// </summary>
        /// <param name="time"></param>
        public void createCamera(int time) { wizCameraAnimationNative.cam_anim_createCamera(ptr, time); }


        /// <summary>
        /// 删除相机
        /// </summary>
        public void deleteCamera() { wizCameraAnimationNative.cam_anim_deleteCamera(ptr); }


        /// <summary>
        /// 前往虚拟相机
        /// </summary>
        public void goToVirtualCamera() { wizCameraAnimationNative.cam_anim_goToVirtualCamera(ptr); }


        /// <summary>
        /// 回到真实相机
        /// </summary>
        public void goBackRealCamera() { wizCameraAnimationNative.cam_anim_goToRealCamera(ptr); }


        /// <summary>
        /// 播放或停止当前相机动画
        /// </summary>
        /// <param name="run">播放或停止</param>
        public void playCameraAnimation(bool run) { wizCameraAnimationNative.cam_anim_playCameraAnimation(ptr, run); }


        /// <summary>
        /// 清楚所有虚拟相机
        /// </summary>
        public void clear() { wizCameraAnimationNative.cam_anim_clear(ptr); }


        /// <summary>
        /// 设置当前时间点
        /// </summary>
        /// <param name="t"></param>
        public void setCurTime(int t) { wizCameraAnimationNative.cam_anim_setCurTime(ptr, t); }


        /// <summary>
        /// 保存当前摄像机动画
        /// </summary>
        public void save(string fileName, string aniName) { wizCameraAnimationNative.cam_anim_save(ptr, fileName, aniName); }

        /// <summary>
        /// 常量及结构定义
        /// </summary>
        public class Const
        {
            /// <summary>
            /// 删除相机动作的消息名称，该消息附带参数结构为int，标识相机的索引
            /// </summary>
            public static string PMSG_CAM_DELETE = "camAnim.delete";

            /// <summary>
            /// 当相机预览时的当前时间消息称，该消息附带参数机构为int，标识当前时间
            /// </summary>
            public static string PMSG_CAM_CURTIME = "camAnim.playtime";
        }
    }


    /// <summary>
    /// 动画播放组件
    /// </summary>
    public class WIZCameraAnimationPlay
    {
        public const string Name = "plugin.cameraAnimationPlay";
        public const string ToolName = "system.ToolCamreaAnimation";
        public IntPtr ptr;

        public static implicit operator WIZCameraAnimationPlay(IntPtr p) { return new WIZCameraAnimationPlay() { ptr = p }; }


        /// <summary>
        /// 组件获取方法
        /// </summary>
        /// <param name="player">目标Player</param>
        /// <returns>需要判断是否成功</returns>
        /// <seealso cref="isValid"/>
        public static WIZCameraAnimationPlay From(WIZPlayer player) { return player.getCompoment(Name); }


        /// <summary>
        /// 判断该组件是否存在
        /// </summary>
        /// <returns></returns>
        public bool isValid() { return ptr != IntPtr.Zero; }


        /// <summary>
        /// 加载模型动画文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool load(string file) { return wizCameraAnimationNative.cam_animplay_load(ptr, file); }


        /// <summary>
        /// 获取动画个数
        /// </summary>
        /// <returns></returns>
        public int getAnimCount() { return wizCameraAnimationNative.cam_animplay_getAnimCount(ptr); }


        /// <summary>
        /// 获取动画名称
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public string getAnimName(int idx)
        {
            StringBuilder s = new StringBuilder(128);
            wizCameraAnimationNative.cam_animplay_getAnimName(ptr, idx, s);
            return s.ToString();
        }

        /// <summary>
        /// 播放或停止动画
        /// </summary>
        /// <param name="run"></param>
        /// <param name="idx"></param>
        public void playAnim(bool run, int idx) { wizCameraAnimationNative.cam_animplay_playAnim(ptr, run, idx); }
    }

    #region C++层导出的接口原形

    /// <summary>
    /// C++层导出的接口原形
    /// </summary>
    class wizCameraAnimationNative
    {
        public const string DLLFILE = "plugin/component/plugin.cameraAnimation.dll";


        [DllImport(DLLFILE, CallingConvention=CallingConvention.Cdecl)]
        public static extern void cam_anim_setEdit(IntPtr camPlugin, bool state);


        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void cam_anim_createCamera(IntPtr camPlugin, int time);


        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void cam_anim_deleteCamera(IntPtr camPlugin);


        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void cam_anim_playCameraAnimation(IntPtr camPlugin, bool run);


        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void cam_anim_goToVirtualCamera(IntPtr camPlugin);


        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void cam_anim_goToRealCamera(IntPtr camPlugin);


        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void cam_anim_clear(IntPtr camPlugin);


        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void cam_anim_setCurTime(IntPtr camPlugin, int value);


        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void cam_anim_save(IntPtr camPlugin, string fileName, string aniName);


        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool cam_animplay_load(IntPtr camPlayPlugin, string file);


        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern int cam_animplay_getAnimCount(IntPtr camPlayPlugin);


        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void cam_animplay_getAnimName(IntPtr camPlayPlugin, int id, [MarshalAs(UnmanagedType.LPStr)] StringBuilder outName);


        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void cam_animplay_clearAnim(IntPtr camPlayPlugin);


        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void cam_animplay_deleteAnim(IntPtr camPlayPlugin, int id);


        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void cam_animplay_playAnim(IntPtr camPlayPlugin, bool run, int id);
    }

    #endregion
}
