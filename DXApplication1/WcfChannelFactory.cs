﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Reflection;
namespace DXApplication1
{
    //==============================================================
    //  作者：李高钢  (735450681@qq.com)
    //  时间：2018/4/23/周一 13:52:11
    //  文件名：WcfChannelFactory
    //  版本：V1.0.1  
    //  说明： 
    //  修改者：李高钢
    //  修改说明： 
    //==============================================================
    public class WcfChannelFactory
    {
        public WcfChannelFactory() { 
        }
         /// <summary>
         /// 执行方法   WSHttpBinding
         /// </summary>
         /// <typeparam name="T">服务接口</typeparam>
         /// <param name="uri">wcf地址</param>
         /// <param name="methodName">方法名</param>
         /// <param name="args">参数列表</param>
         public static object ExecuteMetod<T>(string uri, string methodName, params object[] args)
         {
             //BasicHttpBinding binding = new BasicHttpBinding();   //出现异常:远程服务器返回错误: (415) Cannot process the message because the content type 'text/xml; charset=utf-8' was not the expected type 'application/soap+xml; charset=utf-8'.。
             WSHttpBinding binding = new WSHttpBinding();
             EndpointAddress endpoint = new EndpointAddress(uri);
 
             using (ChannelFactory<T> channelFactory = new ChannelFactory<T>(binding, endpoint))
             {
                 T instance = channelFactory.CreateChannel();
                 using (instance as IDisposable)
                 {
                     try
                     {
                         Type type = typeof(T);
                         MethodInfo mi = type.GetMethod(methodName);
                         return mi.Invoke(instance, args);
                     }
                     catch (TimeoutException)
                     {
                         (instance as ICommunicationObject).Abort();
                         throw;
                     }
                     catch (CommunicationException)
                     {
                         (instance as ICommunicationObject).Abort();
                         throw;
                     }
                     catch (Exception vErr)
                     {
                         (instance as ICommunicationObject).Abort();
                         throw;
                     }
                 }
             }
 
 
         }
 
 
         //nettcpbinding 绑定方式
         public static object ExecuteMethod<T>(string pUrl, string pMethodName,params object[] pParams)
         {
                 EndpointAddress address = new EndpointAddress(pUrl);
                 Binding bindinginstance = null;
                 BasicHttpBinding ws = new BasicHttpBinding();
                 ws.MaxReceivedMessageSize = 20971520; 
                 bindinginstance = ws;
                 using (ChannelFactory<T> channel = new ChannelFactory<T>(bindinginstance, address))
                 {
                     T instance = channel.CreateChannel();
                     using (instance as IDisposable)
                     {
                         try
                         {
                             Type type = typeof(T);
                             MethodInfo mi = type.GetMethod(pMethodName);
                             return mi.Invoke(instance, pParams);
                         }
                         catch (TimeoutException)
                         {
                             (instance as ICommunicationObject).Abort();
                             throw;
                         }
                         catch (CommunicationException)
                         {
                             (instance as ICommunicationObject).Abort();
                             throw;
                         }
                         catch (Exception vErr)
                         {
                             (instance as ICommunicationObject).Abort();
                             throw;
                         }
                     }
                 }
         }
     
    }
}
