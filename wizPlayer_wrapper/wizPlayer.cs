using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

#pragma warning disable CS1591
#pragma warning disable IDE1006

namespace wiz
{

    /// <summary>
    /// WIZModel，包含了Model的句柄，和其方法包装
    /// </summary>
    public struct WIZModel
    {
        public IntPtr ptr;

        public static implicit operator WIZModel(IntPtr ptr) { WIZModel o = new WIZModel(); o.ptr = ptr; return o; }

        /// <summary>
        /// 获取模型唯一标识
        /// </summary>
        /// <returns></returns>
        public uint getKey() { return wizPlayerNative.player_get_modelKey(ptr); }
    }


    /// <summary>
    /// WIZPlayer，包含了Player的句柄，和其方法包装
    /// </summary>
    public struct WIZPlayer
    {

        /// <summary>
        /// Player句柄
        /// </summary>
        public IntPtr ptr;


        /// <summary>
        /// 创建方法
        /// </summary>
        public static WIZPlayer CreateInstance() { return new WIZPlayer() { ptr = wizPlayerNative.createPlayerEngine() }; }


        /// <summary>
        /// 销毁Player
        /// </summary>
        public void destory() { if (ptr != IntPtr.Zero) { wizPlayerNative.player_desotry(ptr); ptr = IntPtr.Zero; } }


        /// <summary>
        /// 添加从Player句柄到Player类的自动装箱方法
        /// </summary>
        /// <param name="p">Player句柄</param>
        public static implicit operator WIZPlayer(IntPtr p) { return new WIZPlayer() { ptr = p }; }


        /// <summary>
        /// 启动Player
        /// </summary>
        /// <remarks>该方法为异步方法，需要监听"engine.init"消息</remarks>
        /// <param name="hParent">要附加的窗口句柄</param>
        /// <param name="resourcePath">资源目录，默认是../data</param>
        /// <param name="logFile">日志文件，默认是./log.xml</param>
        public void start(IntPtr hParent, string resourcePath = "", string logFile = "") { wizPlayerNative.player_start(ptr, hParent, resourcePath, logFile); }


        /// <summary>
        /// 判断Player是否正在运行
        /// </summary>
        /// <seealso cref="WIZPlayer.start(IntPtr, string, string)"/>
        public bool isRunning() { return ptr == null ? false : wizPlayerNative.player_is_running(ptr); }


        /// <summary>
        /// 设置Player的显示区域
        /// </summary>
        /// <param name="x">left</param>
        /// <param name="y">top</param>
        /// <param name="w">width</param>
        /// <param name="h">heigh</param>
        public void setRect(int x, int y, int w, int h) { wizPlayerNative.player_set_rect(ptr, x, y, w, h); }


        /// <summary>
        /// 添加Player消息监听
        /// </summary>
        /// <param name="message">要监听的消息名称</param>
        /// <param name="handler">监听回调函数</param>
        public void addMessageHandler(string message, MessageHandler handler) { wizPlayerNative.player_subscribe(ptr, message, handler); }


        /// <summary>
        /// 移出Player消息监听
        /// </summary>
        /// <param name="message">要移出的消息名称</param>
        /// <param name="handler">要移出的回调函数</param>
        public void removeMessageHandler(string message, MessageHandler handler) { wizPlayerNative.player_unsubscribe(ptr, message, handler); }


        /// <summary>
        /// 向Player发送消息
        /// </summary>
        /// <param name="message">消息名称</param>
        /// <param name="param">参数</param>
        /// <param name="async">是否为异步消息</param>
        public void sendMessage(string message, IntPtr param, bool async = false) { wizPlayerNative.player_publish(ptr, message, param, async ? wizPlayerNative.MSG_ASYNC : 0); }


        /// <summary>
        /// 设置Player窗口为Focus窗口
        /// </summary>
        /// <remarks>一些Window7/XP系统，当窗口没有Focus时无法得到鼠标滚轮消息</remarks>
        /// <returns></returns>
        public IntPtr setFocus() { return wizPlayerNative.player_set_focus(ptr); }
        

        /// <summary>
        /// 加载指定目录下的所有插件
        /// </summary>
        /// <param name="dir">插件目录</param>
        /// <param name="ext">插件文件扩展名称</param>
        public void loadPlugins(string dir, string ext = ".dll") { wizPlayerNative.player_load_plugins(ptr, dir, ext); }


        /// <summary>
        /// 加载一个模型
        /// </summary>
        /// <param name="file">模型文件路径</param>
        public void loadModel(string file) { wizPlayerNative.player_load_model(ptr, file); }


        /// <summary>
        /// 关闭模型
        /// </summary>
        /// <param name="model">要关闭的模型句柄</param>
        public void closeModel(WIZModel model) { wizPlayerNative.player_close_model(ptr, model.ptr); }


        /// <summary>
        /// 选则当前工具
        /// </summary>
        /// <param name="tool">工具</param>
        public void setCurTool(WIZTool tool) { wizPlayerNative.player_set_curTool(ptr, tool.ptr); }

        /// <summary>
        /// 获取组件
        /// </summary>
        /// <param name="name">组件名称</param>
        /// <returns>组件对象，通过其ptr判断是否获取成功</returns>
        public IntPtr getCompoment(string name) { return wizPlayerNative.player_get_compoment(ptr, name); }


        /// <summary>
        /// 常量及结构定义
        /// </summary>
        public class Const
        {
            
            /// <summary>
            /// Player初始化消息名称，该消息没有参数
            /// </summary>
            public const string PMSG_INIT = "engine.init";


            /// <summary>
            /// Player加载模型消息名称，该消息附带参数结构为<see cref="ParamLoadModel"/>
            /// </summary>
            public const string PMSG_LOADMODEL = "engine.loadModel";
            public struct ParamLoadModel
            {
                public WIZModel    model;
                public string      fileName;
                public IntPtr      user;
            };


            /// <summary>
            /// Player关闭模型消息名称，该消息附带参数结构为<see cref="WIZModel"/>
            /// </summary>
            public const string PMSG_CLOSEMODEL = "engine.closeModel";


            /// <summary>
            /// Player切换工具消息名称，该消息附带参数结构为<see cref="ParamSelectTool"/>
            /// </summary>
            public const string PMSG_SELECTTOOL = "engine.selectTool";
            public struct ParamSelectTool
            {
                public WIZTool tool;
                public WIZTool oldTool;
            };

            
            /// <summary>
            /// Player注册工具消息名称，该消息附带参数结构为<see cref="WIZTool"/>
            /// </summary>
            public const string PMSG_REGISTERTOOL = "engine.registerTool";
            
        }
    }
    

    /// <summary>
    /// 接受Player消息的委托原型
    /// </summary>
    /// <param name="player">发送消息的Player</param>
    /// <param name="param">消息参数</param>
    public delegate void MessageHandler(WIZPlayer player, IntPtr param);
    


    #region C++层导出的接口原形

    /// <summary>
    /// C++层导出的接口原形
    /// </summary>
    class wizPlayerNative
    {
        public const string DLLFILE = "wizPlayerEngine";
        public const int MSG_ASYNC = 1 << 0;

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr createPlayerEngine();

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_set_rect(IntPtr player, int x, int y, int w, int h);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_desotry(IntPtr player);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_subscribe(IntPtr player, string topic, MessageHandler handler);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_unsubscribe(IntPtr player, string topic, MessageHandler handler);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_publish(IntPtr player, string topic, IntPtr param, int flag);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_start(IntPtr player, IntPtr hParent, string resourcePath, string logFile);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool player_is_running(IntPtr player);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_load_plugins(IntPtr player, string dir, string ext);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_load_model(IntPtr player, string file);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_close_model(IntPtr player, IntPtr model);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint player_get_modelKey(IntPtr model);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void player_set_curTool(IntPtr player, IntPtr tool);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr player_set_focus(IntPtr player);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr player_get_compoment(IntPtr player, string name);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern int player_get_compomentName(IntPtr compoment, [MarshalAs(UnmanagedType.LPStr)] StringBuilder outName);

    }

    #endregion
}
