using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Network {

    public static class NetworkConstants {
        public const string Ip = "127.0.0.1";
    }

    public class Client<T> where T : Loadable,new() {
        
        public const int Port = 1354;
        private TcpClient internalClient;
        private NetworkStream stream;

        public T Target { get; private set; }

        public Client(string data,bool binary) {
            internalClient=new TcpClient(NetworkConstants.Ip,Port);
            stream = internalClient.GetStream();
            byte[] realData = Encoding.UTF8.GetBytes(data);
            stream.Write(realData,0,realData.Length);
            new Thread(() => {
                    if(binary)
                        LoadBinary();
                    else
                        Load();
            }).Start();
        }

        void LoadBinary() {
            try {
                while (true) {
                    int bufferSize = internalClient.ReceiveBufferSize;

                    if (bufferSize > 0) {
                        byte[] buffer = new byte[bufferSize];
                        stream.Read(buffer, 0, bufferSize);
                        T temp = (T) new T().LoadBinary(buffer);
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