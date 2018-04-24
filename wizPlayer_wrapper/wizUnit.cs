using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
#pragma warning disable CS1591
#pragma warning disable IDE1006

namespace wiz
{
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct int2
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct int3
    {
        public int x;
        public int y;
        public int z;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct int4
    {
        public int x;
        public int y;
        public int z;
        public int w;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]

    public struct real2
    {
        public double x;
        public double y;
    }



    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct float2
    {
        public float x;
        public float y;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct real3
    {
        public double x;
        public double y;
        public double z;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct float3
    {
        public float x;
        public float y;
        public float z;

        public float3(float x,float y,float z )
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct real4
    {
        public double x;
        public double y;
        public double z;
        public double w;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct float4
    {
        public float x;
        public float y;
        public float z;
        public float w;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct quatr
    {
        public double x;
        public double y;
        public double z;
        public double w;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct quatf
    {
        public float x;
        public float y;
        public float z;
        public float w;
    }
    

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct Rgba
    {
        public byte _r;
        public byte _g;
        public byte _b;
        public byte _a;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct matrix4f
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public float[] _data;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct matrix4r
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public double[] _data;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct aabb2dr
    {
        public real2    _minimum;
        public real2    _maximum;
        public UInt32   _extent;
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct aabb2df
    {
        public float2 _minimum;
        public float2 _maximum;
        public UInt32 _extent;
    };
    /// <summary>
    /// aabb3dr
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct aabb3dr
    {
        public real3  _minimum;
        public real3  _maximum;
        public UInt32  _extent;
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct aabb3df
    {
        public float3 _minimum;
        public float3 _maximum;
        public UInt32 _extent;
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct RayF
    {
        public float3 _origin;
        public float3 _direction;
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct RayR
    {
        public real3 _origin;
        public real3 _direction;
    };

}
