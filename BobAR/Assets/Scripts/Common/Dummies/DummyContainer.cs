#define DUMMY
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Network;
using Network.Data;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common.Dummies {

    public class DummyContainer : MonoBehaviour {
        public bool running=true;
        public string userProfile,
                      RestaurantProfile;
#if DUMMY
        public byte[] userProfileBytes,
                      restaurantProfileBytes;

        TcpListener tcpListener = null;

        readonly IPAddress addr=IPAddress.Parse("127.0.0.1");
        public void Awake() {
            userProfileBytes=File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "Dummy", userProfile));
            restaurantProfileBytes=File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "Dummy", RestaurantProfile));

            tcpListener=new TcpListener(addr,Client<Review>.Port);
            new Thread(ControlListener).Start();
        }

        public void ControlListener() {
            while (running) {
                new DummyClient(tcpListener.AcceptTcpClient());
            }
        }

#endif
    }
    #if DUMMY
    class DummyClient {
        private readonly TcpClient client;

        public DummyClient(TcpClient input) {
            client = input;
            new Thread(RunDummy).Start();
        }

        public void RunDummy() {
            NetworkStream stream=client.GetStream();

            try {
                while (true) {
                    int bufferSize = client.ReceiveBufferSize;

                    if (bufferSize > 0) {
                        byte[] buffer = new byte[bufferSize];
                        stream.Read(buffer, 0, bufferSize);
                        string input= Encoding.UTF8.GetString(buffer);


                        //TODO 더미데이터 처리
                        string output = input;

                        buffer=Encoding.UTF8.GetBytes(output);
                        stream.Write(buffer,0,buffer.Length);
                        stream.Flush();

                        break;
                    }

                    Thread.Sleep(0);
                }
            }
            catch (Exception e) {
                Debug.LogError(e);
            }
            

        }
    }
    #endif

}