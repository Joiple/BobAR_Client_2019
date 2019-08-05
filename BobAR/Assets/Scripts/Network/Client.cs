using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Network {

    public static class NetworkConstants {
        public const string Ip = "127.0.0.1";
        public const char Diff = '\\';
    }

    public class Client<T> where T : ILoadable,new() {
        
        public const int Port = 1354;
        private TcpClient internalClient;
        private NetworkStream stream;

        public T Target { get; private set; }

        public Client(string data) {
            internalClient=new TcpClient(NetworkConstants.Ip,Port);
            stream = internalClient.GetStream();
            byte[] realData = Encoding.UTF8.GetBytes(data);
            stream.Write(realData,0,realData.Length);
            new Thread(Load).Start();
            
        }

        void Load() {
            try {
                while (true) {
                    int bufferSize = internalClient.ReceiveBufferSize;

                    if (bufferSize > 0) {
                        byte[] buffer = new byte[bufferSize];
                        stream.Read(buffer, 0, bufferSize);
                        string output = Encoding.UTF8.GetString(buffer);
                        T temp = (T) new T().Load(output);
                        Target = temp;
                        prepared = true;

                        break;
                    }

                    Thread.Sleep(0);
                }
            }
            catch (Exception e) {
                Debug.LogError(e);
                Target = default;
                prepared = true;
            }
        }


        public bool prepared =false;
    }

}