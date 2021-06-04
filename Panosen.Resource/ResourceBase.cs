using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Panosen.Resource
{
    /// <summary>
    /// 资源文件基础类。推荐使用单例。
    /// </summary>
    public abstract class ResourceBase<T> where T : class
    {
        /// <summary>
        /// 数据资源
        /// </summary>
        private Dictionary<string, byte[]> BytesMap { get; set; }

        /// <summary>
        /// 获取单个数据资源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public byte[] GetResource(string key)
        {
            this.WaitForFirstLoad();

            if (BytesMap.ContainsKey(key))
            {
                return BytesMap[key];
            }

            return null;
        }

        /// <summary>
        /// 第一次加载
        /// </summary>
        private void WaitForFirstLoad()
        {
            if (this.BytesMap != null)
            {
                return;
            }

            this.BytesMap = LoadResources();
        }

        private Dictionary<string, byte[]> LoadResources()
        {
            Dictionary<string, byte[]> resources = new Dictionary<string, byte[]>();

            var assembly = typeof(T).Assembly;
            if (assembly.GetManifestResourceNames().Length == 0)
            {
                return resources;
            }

            foreach (var item in assembly.GetManifestResourceNames())
            {
                var resourceStream = assembly.GetManifestResourceStream(item);

                var bytes = new byte[resourceStream.Length];
                resourceStream.Read(bytes, 0, bytes.Length);

                resources.Add(item, bytes);
            }

            return resources;
        }
    }
}
