using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Common.BaseLibrary
{
    public static class Filehelp
    {
        /// <summary>
        /// 异步读取文件内容到字符串（windows磁盘队列支持，大文件优先使用）
        /// </summary>
        /// <param name="path">文件链接</param>
        /// <returns>返回字符串（Utf8）</returns>
        public static async Task<string> ReadFile2StrAsync(string path)
        {
            StringBuilder sb = new StringBuilder(100);
            const int FILE_READ_SIZE = 4096;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, FILE_READ_SIZE, true))
            {
                byte[] vs = new byte[0x1000];
                int numRead = 0;
                while ((numRead = await fs.ReadAsync(vs, 0, vs.Length)) != 0)
                {
                    await Task.Delay(100);
                    sb.Append(Encoding.UTF8.GetString(vs, 0, numRead));
                }
            }
            return sb.ToString();
        }


        public static async Task<int> WriteFileAsync(System.IO.Stream stream, string path)
        {
            const int FILE_WRITE_SIZE = 4096;
            int writeCount = 0;
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write, FILE_WRITE_SIZE, true))
            {
                byte[] byteArr = new byte[500];
                int readCount = 0;
                while ((readCount = await stream.ReadAsync(byteArr, 0, byteArr.Length)) > 0)
                {
                    await fileStream.WriteAsync(byteArr, 0, readCount);
                    writeCount += readCount;
                }
            }
            return writeCount;
        }




        /// <summary>
        /// 创建GUID文件路径，并创建对应路径
        /// /basePath+/20190204/+guid
        /// </summary>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static string GetFilePath(string basePath)
        {
            string filePath = string.Empty;
            string fileName = Guid.NewGuid().ToString("N");
            filePath = basePath;

            string dirName = DateTime.Now.ToString("yyyyMMdd");
            filePath = System.IO.Path.Combine(filePath, dirName);
            if (!System.IO.Directory.Exists(filePath))
                System.IO.Directory.CreateDirectory(filePath);

            filePath = System.IO.Path.Combine(filePath, fileName);
            return filePath;
        }
    }
}
