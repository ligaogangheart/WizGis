using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
namespace DXApplication1
{
    //==============================================================
    //  作者：李高钢  (735450681@qq.com)
    //  时间：2018/4/23/周一 13:55:11
    //  文件名：Itestdemo
    //  版本：V1.0.1  
    //  说明： 
    //  修改者：李高钢
    //  修改说明： 
    //==============================================================
       [ServiceContract]
    public interface Itestdemo
    {
            [OperationContract]
            string GetTestString(Guid objId);
        }
    
}
