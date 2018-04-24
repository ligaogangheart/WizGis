using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

#pragma warning disable CS1591

namespace wiz
{

    /// <summary>
    /// GIS
    /// </summary>
    public struct WIZGISPlatform
    {
        private const string Name = "plugin.gisPlatform";
        public IntPtr ptr;

        public static implicit operator WIZGISPlatform(IntPtr p) { return new WIZGISPlatform() { ptr = p }; }

        /// <summary>
        /// 组件获取方法
        /// </summary>
        /// <param name="player">目标Player</param>
        /// <returns>需要判断是否成功</returns>
        /// <seealso cref="isValid"/>
        public static WIZGISPlatform From(WIZPlayer player) { return player.getCompoment(Name); }
        
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
            return nativeWIZGISPlatform.GISObject_load(ptr, fileName);
        }


        /// <summary>
        /// 卸载倾斜摄影数据
        /// </summary>
        public void unload()
        {
            nativeWIZGISPlatform.GISObject_unload(ptr);
        }

        /// <summary>
        /// 卸载倾斜摄影数据
        /// </summary>
        public aabb3df getAabb()
        {
            aabb3df aabb    =   new aabb3df();
            nativeWIZGISPlatform.GISObject_getAabb(ptr,ref aabb);
            return aabb;
        }

        /// <summary>
        /// 卸载倾斜摄影数据
        /// </summary>
        public aabb3df getNodeAabb()
        {
            aabb3df aabb = new aabb3df();
            nativeWIZGISPlatform.GISObject_getNodeAabb(ptr, ref aabb);
            return aabb;
        }
        /// <summary>
        /// calcIntersect
        /// </summary>
        public bool calcIntersect(RayR ray, ref real3 result)
        {
            return nativeWIZGISPlatform.IGeometry_calcRay(ptr, ray,ref result);
        }

        /// <summary>
        /// calcIntersect
        /// </summary>
        public IntPtr loadGeometry(string fileName)
        {
            return nativeWIZGISPlatform.GISObject_loadGeometry(ptr, fileName);
        }
        /// <summary>
        /// getGeomCount
        /// </summary>
        public uint getGeomCount()
        {
            return nativeWIZGISPlatform.GISObject_getGeomCount(ptr);
        }

        /// <summary>
        /// getGeometry
        /// </summary>
        public IntPtr getGeometry(uint idx)
        {
            return nativeWIZGISPlatform.GISObject_getGeometry(ptr, idx);
        }
    }


    /// <summary>
    /// Geometry
    /// </summary>
    public struct WIZIGeometry
    {
        public IntPtr ptr;

        public static implicit operator WIZIGeometry(IntPtr p) { return new WIZIGeometry() { ptr = p }; }


        /// <summary>
        /// 判断该组件是否存在
        /// </summary>
        /// <returns></returns>
        public bool isValid() { return ptr != IntPtr.Zero; }

        /// <summary>
        /// 创建节点
        /// </summary>
        public IntPtr createNode()
        {
            return  nativeWIZGISPlatform.IGeometry_createNode(ptr);
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        public void destroyNode(IntPtr nodePtr)
        {
            nativeWIZGISPlatform.IGeometry_destroyNode(ptr, nodePtr);
        }

        /// <summary>
        /// 射线测试
        /// </summary>
        public bool calcRay(RayR ray, ref real3 result)
        {
            return  nativeWIZGISPlatform.IGeometry_calcRay(ptr, ray, ref result);
        }

        /// <summary>
        /// 测试修改节点
        /// </summary>
        public void modifyNodeFlag(uint addFlg,uint remFlg)
        {
            nativeWIZGISPlatform.IGeometry_modifyNodeFlag(ptr, addFlg, remFlg);
        }

    }


    /// <summary>
    /// Node
    /// </summary>
    public struct WIZNode
    {
        public IntPtr ptr;

        public static implicit operator WIZNode(IntPtr p) { return new WIZNode() { ptr = p }; }


        /// <summary>
        /// 判断该组件是否存在
        /// </summary>
        /// <returns></returns>
        public bool isValid() { return ptr != IntPtr.Zero; }

        /// <summary>
        /// 获取包围盒
        /// </summary>
        public aabb3df getAabb()
        {
            aabb3df aabb = new aabb3df();
            nativeWIZGISPlatform.Node_getAabb(ptr,ref aabb);
            return aabb;
        }

        /// <summary>
        /// 设置包围盒
        /// </summary>
        public void setAabb(aabb3df aabb)
        {
            nativeWIZGISPlatform.Node_setAabb(ptr, aabb);
        }

        /// <summary>
        /// 获取包围盒
        /// </summary>
        public void setScale(float3 sc)
        {
            nativeWIZGISPlatform.Node_setScale(ptr, sc);
        }

        /// <summary>
        /// 获取缩放比例
        /// </summary>
        public float3 getScale()
        {
            float3 data = new float3();
            nativeWIZGISPlatform.Node_getScale(ptr, ref data);
            return data;
        }

        /// <summary>
        /// 获取缩放比例
        /// </summary>
        public void setPosition(float3 pos)
        {
            nativeWIZGISPlatform.Node_setPosition(ptr, pos);
        }

        /// <summary>
        /// 获取缩放比例
        /// </summary>
        public float3 getPosition()
        {
            float3 data = new float3();
            nativeWIZGISPlatform.Node_getPosition(ptr, ref data);
            return data;
        }

        /// <summary>
        /// 设置旋转角度
        /// </summary>
        public void setAngle(float angle,float3 axis)
        {
            nativeWIZGISPlatform.Node_setAngle(ptr, angle, axis);
        }

        /// <summary>
        /// 获取旋转数据
        /// </summary>
        public quatf getQuat()
        {
            quatf data = new quatf();
            nativeWIZGISPlatform.Node_getQuat(ptr, ref data);
            return data;
        }

        /// <summary>
        /// 设置
        /// </summary>
        public void setQuat(quatf quad)
        {
            nativeWIZGISPlatform.Node_setQuat(ptr, quad);
        }

        /// <summary>
        /// 设置
        /// </summary>
        public matrix4f getMatrix( )
        {
            matrix4f mat = new matrix4f();
            nativeWIZGISPlatform.Node_getMatrix(ptr, ref mat);
            return mat;
        }

        /// <summary>
        /// 设置
        /// </summary>
        public void updateAabb()
        {
            nativeWIZGISPlatform.Node_updateAabb(ptr);
        }
    }


    #region C++层导出的接口原形

    /// <summary>
    /// C++层导出的接口原形
    /// </summary>
    public class nativeWIZGISPlatform
    {
        public const string DLLFILE = "plugin/component/plugin.gisPlatform.dll";


        
        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool longlatToWorld(ref real2 pos);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GISObject_load(IntPtr obj, string fileName);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GISObject_unload(IntPtr obj);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GISObject_getAabb(IntPtr obj,ref aabb3df aabb);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GISObject_getNodeAabb(IntPtr obj,ref aabb3df aabb);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GISObject_loadGeometry(IntPtr obj, string fileName);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GISObject_getGeomCount(IntPtr obj);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GISObject_getGeometry(IntPtr obj,uint index);

        /////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr IGeometry_createNode(IntPtr obj);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void IGeometry_destroyNode(IntPtr obj,IntPtr nodePtr);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IGeometry_calcRay(IntPtr obj, RayR ray, ref real3 result);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void IGeometry_modifyNodeFlag(IntPtr obj, uint addFlg, uint remFlg);

        ///////////////////////////////////////////////////////////////////////////////////////////////
        /// 
        ///////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Node_getAabb(IntPtr obj, ref aabb3df aabb);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Node_setAabb(IntPtr obj,  aabb3df aabb);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Node_setScale(IntPtr obj, float3 aabb);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Node_getScale(IntPtr obj, ref float3 sc);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Node_setPosition(IntPtr obj,  float3 sc);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Node_getPosition(IntPtr obj, ref float3 sc);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Node_setAngle(IntPtr obj, float angle, float3 axis);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Node_setQuat(IntPtr obj,quatf quad);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Node_getQuat(IntPtr obj, ref quatf quad);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Node_getMatrix(IntPtr obj, ref matrix4f mat);

        [DllImport(DLLFILE, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Node_updateAabb(IntPtr objt);
        

    }

    #endregion
}
