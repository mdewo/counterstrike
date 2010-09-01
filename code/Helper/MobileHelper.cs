﻿using System;

using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Collections;
using System.Net.Sockets;
using System.Threading;
using System.Drawing;
//using FarseerGames.FarseerPhysics.Mathematics;

namespace doru
{
#if(PocketPC)
        public delegate void Action();
#endif
    public partial class H
    {
        public static void Trace(object o)
        {
            Debug.WriteLine(o);
        }
        
        //public static Point ToPoint(Vector2 v)
        //{
        //    return new Point((int)v.X, (int)v.Y);
        //}
        public static IEnumerable<T> Add<T>(IEnumerable<T> tt,T t)
        {
            foreach (var a in tt)
                yield return a;
            yield return t;
        }
        
        public static T First<T>(IEnumerable<T> tt)
        {
             var a=tt.GetEnumerator();
             a.MoveNext();
            return a.Current;
        }
        public static Random r= new Random();
        public static T[] ToArray<T>(IEnumerable<T> t)
        {
            List<T> list = new List<T>();
            foreach (T i in t)
                list.Add(i);
            return list.ToArray();
        }
        public static IEnumerable<T> Concat<T>(IEnumerable<T> t1,IEnumerable<T> t2)
        {
            foreach(T t in t1)
                yield return t;
            foreach (T t in t2)
                yield return t;
        }

        [DebuggerStepThrough]
        public static void Assert(bool a)
        {
            if (!a) Debugger.Break();
        }
        public static string ReadString(Stream s)
        {
            if(s.Position==s.Length) s.Position = 0;
            StringBuilder sb = new StringBuilder();
            while (s.Position != s.Length)
            {
                sb.Append((char)s.ReadByte());
            }
            return sb.ToString();
        }
        public static void WriteString(Stream s, string ss)
        {
            foreach (char c in ss)
                s.WriteByte((byte)c);
        }
        public static string WriteLine(Stream _Stream, string s)
        {
            WriteString(_Stream,s + "\r\n");
            return s;
        }

        public static string ToStr(byte[] _Bytes)
        {
            return Encoding.Default.GetString(_Bytes,0,_Bytes.Length);
        }

        public static MemoryStream Serialize(XmlSerializer xs,object o)
        {
            MemoryStream ms = new MemoryStream();            
            xs.Serialize(ms, o);
            ms.Position = 0;
            return ms;
        }
        public class ENetworkStream : NetworkStream, IEnumerable<byte>
        {
            public bool blocking { set { _StreamEnumerator.blocking = value; } }
            public class StreamEnumerator : IEnumerator<byte>
            {
                public bool blocking;
                public Stream ms;
                public byte Current
                {
                    get { return current; }
                }
                byte current;
                public void Dispose()
                {

                }
                object IEnumerator.Current
                {
                    get { return current; }
                }

                public bool MoveNext()
                {
                    int _byte = ms.ReadByte();
                    if (blocking)
                    {
                        while (_byte == -1)
                        {
                            _byte = ms.ReadByte();
                            Thread.Sleep(2);
                        }
                        return true;
                    } else
                    {
                        if (_byte != -1)
                        {
                            current = (byte)_byte;
                            return true;
                        } else
                            return false;
                    }
                }

                public void Reset()
                {
                    ms.Position = 0;
                }

            }
            StreamEnumerator _StreamEnumerator = new StreamEnumerator();
            public ENetworkStream(Socket _Socket)
                : base(_Socket)
            {
                _StreamEnumerator.ms = this;
            }
            public IEnumerator<byte> GetEnumerator()
            {
                return _StreamEnumerator;

            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _StreamEnumerator;
            }
        }

        public static byte[] Cut(NetworkStream source, byte[] pattern)
        {
            MemoryStream _MemoryStream = new MemoryStream();
            while (true)
            {
                for (int i = 0; i < pattern.Length; i++)
                {
                    
                    int b = source.ReadByte();
                    if (b == -1) throw new IOException("Cut: unable to cut");
                    _MemoryStream.WriteByte((byte)b);
                    if (pattern[i] != b) break;
                    if (i == pattern.Length - 1) return _MemoryStream.ToArray();
                }
            }
        }

        //public static void Trace(object p)
        //{
        //    Debug.WriteLine(p);
        //}
    }
}