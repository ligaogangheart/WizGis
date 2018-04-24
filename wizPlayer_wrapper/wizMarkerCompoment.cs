using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

#pragma warning disable CS1591

namespace wiz
{

    /// <summary>
    /// 标记组件
    /// </summary>
    public struct WIZMarkerCompoment
    {
        public const string Name = "plugin.marker";
        public const string ToolName = "system.ToolMark";
        public IntPtr ptr;

        public static implicit operator WIZMarkerCompoment(IntPtr p) { return new WIZMarkerCompoment() { ptr = p }; }
        

        /// <summary>
        /// 组件获取方法
        /// </summary>
        /// <param name="player">目标Player</param>
        /// <returns>需要判断是否成功</returns>
        /// <seealso cref="isValid"/>
        public static WIZMarkerCompoment From(WIZPlayer player) { return player.getCompoment(Name); }


        /// <summary>
        /// 判断该组件是否存在
        /// </summary>
        /// <returns></returns>
        public bool isValid() { return ptr != IntPtr.Zero; }


        /// <summary>
        /// 通知组件进行新建标记
        /// </summary>
        public void newMark() { wizMarkerNative.player_pluginmark_newMark(ptr); }


        /// <summary>
        /// 通知组件选择一个形状
        /// </summary>
        /// <param name="shp">目标形状</param>
        public void selectShape(MarkerShape shp) { wizMarkerNative.player_pluginmark_selectShape(ptr, shp); }


        /// <summary>
        /// 设置线段宽度
        /// </summary>
        /// <param name="lineWidth">像素</param>
        public void setLineWidth(int lineWidth) { wizMarkerNative.player_pluginmark_lineWidth(ptr, lineWidth); }

        
        /// <summary>
        /// 设置线段的颜色
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        /// <param name="a">Alpha</param>
        public void setLineColor(byte r, byte g, byte b, byte a) { wizMarkerNative.player_pluginmark_lineColor(ptr, r, g, b, a); }


        /// <summary>
        /// 设置填充颜色
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        /// <param name="a">Alpha</param>
        public void setFillColor(byte r, byte g, byte b, byte a) { wizMarkerNative.player_pluginmark_fillColor(ptr, r, g, b, a); }


        /// <summary>
        /// 通知组件保存标记
        /// </summary>
        public void saveMark() { wizMarkerNative.player_pluginmark_saveMark(ptr); }


        /// <summary>
        /// 通知标记进入或退出编辑状态
        /// </summary>
        /// <param name="bEdit">true为进入，false为退出</param>
        public void tryEdit(bool bEdit) { wizMarkerNative.player_pluginmark_tryEdit(ptr, bEdit); }


        /// <summary>
        /// 判断组件是否在编辑状态
        /// </summary>
        /// <returns></returns>
        public bool isEdit() { return wizMarkerNative.player_pluginmark_isEdit(ptr); }


        /// <summary>
        /// 判断组件自上次保存或打开后是否对标记进行了修改
        /// </summary>
        /// <returns></returns>
        public bool isEdited() { return wizMarkerNative.player_pluginmark_isEdited(ptr); }


        /// <summary>
        /// 删除当前选中的形状
        /// </summary>
        public void removeCurSharp() { wizMarkerNative.player_pluginmark_deleteSelect(ptr); }


        /// <summary>
        /// 删除当前选中标记
        /// </summary>
        public void deleteCurMark() { wizMarkerNative.player_pluginmark_deleteMark(ptr); }


        /// <summary>
        /// 设置当前目标模型
        /// </summary>
        /// <param name="model"></param>
        public void setCurModel(WIZModel model) { wizMarkerNative.player_pluginmark_setCurModelKey(ptr, model.getKey()); }



        /// <summary>
        /// 常量及结构定义
        /// </summary>
        public class Const
        {

            /// <summary>
            /// 标记执行了动作的消息名称，该消息附带参数结构为<see cref="MarkerNotice"/>
            /// </summary>
            public const string PMSG_EDIT = "marker.edit";


            /// <summary>
            /// 选择了标记或形状的消息名称，该消息附带参数结构为<see cref="ParamMarkerSelect"/>
            /// </summary>
            public const string PMSG_SELECT = "marker.select";
            public struct ParamMarkerSelect
            {
                /// <summary>
                /// 对象标识
                /// </summary>
                public string id;

                /// <summary>
                /// 0为选中标记，1为形状
                /// </summary>
                public int bMark;
            };


        }
    }


    /// <summary>
    /// 标记类型,用于选择标记形状
    /// </summary>
    public enum MarkerShape
    {
        ///圆型标记
        Ellipse = 0,
        ///矩形标记
        Rect = 1,
        ///箭头标记
        Array = 2,
        ///多边形(折线)标记
        Polygon = 3,
        ///曲线标记
        Curvel = 4,
        ///画笔
        Lines = 5,
        ///文本标记
        Text = 6,
    };


    /// <summary>
    /// 操作类型,用于标记编辑通知
    /// </summary>
    public enum MarkerNotice
    {
        ///新建标记
        NewMark = 0,
        ///添加标记
        AddMark = 1,
        ///更新标记
        UpdateMark = 2,
        ///移除当前选择
        RemoveSelect = 3,
        ///删除标记
        DeleteMark = 4,
        ///选择标记
        SelectMark = 5,
        ///加载标记
        LoadMark = 6,
        ///浏览标记
        BrowseMark = 7,
    };



    #region C++层导出的接口原形

    /// <summary>
    /// C++层导出的接口原形
    /// </summary>
    class wizMarkerNative
    {
        public const string DLLFILE = "wizPlayerEngine";
        

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_pluginmark_newMark(IntPtr marker);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_pluginmark_selectShape(IntPtr marker, MarkerShape shape);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_pluginmark_lineWidth(IntPtr marker, int lineWidth);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_pluginmark_lineColor(IntPtr marker, byte r, byte g, byte b, byte a);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_pluginmark_fillColor(IntPtr marker, byte r, byte g, byte b, byte a);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_pluginmark_saveMark(IntPtr marker);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_pluginmark_tryEdit(IntPtr marker, bool bEdit);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool player_pluginmark_isEdit(IntPtr marker);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool player_pluginmark_isEdited(IntPtr marker);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_pluginmark_deleteSelect(IntPtr marker);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_pluginmark_deleteMark(IntPtr marker);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_pluginmark_setSelectMark(IntPtr marker, string id);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_pluginmark_setSelectShape(IntPtr marker, string id);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_pluginmark_setCurModelKey(IntPtr marker, UInt32 modelKey);
    }

    #endregion
}
