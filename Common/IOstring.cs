using System;
using System.Text;
using System.Net.Sockets;

namespace Common
{
    public static class IOstring
    {

        /// <summary>
        /// Writes a UTF-8 encoded string to the network stream, prefixed with its length.
        /// </summary>
        /// <param name="str">The string to write.</param>
        public static void WriteString(NetworkStream stream, string str)
        {
            var buf = Encoding.UTF8.GetBytes(str);
            var len = BitConverter.GetBytes(buf.Length);
            stream.Write(len, 0, 4);
            stream.Write(buf, 0, buf.Length);
        }

        /// <summary>
        /// Reads a UTF-8 encoded string from the network stream, using the prefixed length.
        /// </summary>
        /// <returns>The string read from the stream.</returns>
        public static string ReadString(NetworkStream stream)
        {
            var lenBuf = new byte[4];
            stream.Read(lenBuf, 0, 4);
            int len = BitConverter.ToInt32(lenBuf, 0);

            var buf = new byte[len];
            int read = 0;
            while (read < len) read += stream.Read(buf, read, len - read);
            return Encoding.UTF8.GetString(buf);
        }
    }
}
