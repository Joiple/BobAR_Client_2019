#define DUMMY
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Network;
using Network.Data;
using UnityEngine;

namespace Common.Dummies {

    public class DummyContainer : MonoBehaviour {
        public bool running = true;
        public string userProfile,
                      restaurantProfile;
#if DUMMY
        public byte[] userProfileBytes,
                      restaurantProfileBytes;

        TcpListener tcpListener = null;

        readonly IPAddress addr = IPAddress.Parse("127.0.0.1");

        public void Awake() {
            userProfileBytes = File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "Dummy", userProfile));
            restaurantProfileBytes = File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "Dummy", restaurantProfile));

            tcpListener = new TcpListener(addr, Client<Loadable>.Port);
            new Thread(ControlListener).Start();
        }

        public void ControlListener() {
            while (running) {
                new DummyClient(tcpListener.AcceptTcpClient(), this);
            }
        }

#endif
    }
#if DUMMY
    class DummyClient {
        private readonly TcpClient client;
        public DummyContainer container;

        public DummyClient(TcpClient input, DummyContainer context) {
            client = input;
            new Thread(RunDummy).Start();
            container = context;
        }

        public void RunDummy() {
            NetworkStream stream = client.GetStream();

            try {
                while (true) {
                    int bufferSize = client.ReceiveBufferSize;

                    if (bufferSize > 0) {
                        byte[] buffer = new byte[bufferSize];
                        stream.Read(buffer, 0, bufferSize);
                        string input = Encoding.UTF8.GetString(buffer);

                        string[] data = input.Split(new[] {
                            Loadable.Diff
                        }, StringSplitOptions.None);

                        string output = "";
                        byte[] outputBytes = new byte[0];

                        switch (Convert.ToInt32(data[0])) {
                            case MyPagePacket.Code: //내정보
                                switch (data[1]) {
                                    case "MyId":
                                        output += "내 닉네임" + Loadable.Diff;
                                        output += "0" + Loadable.Diff;
                                        output += 10 + Loadable.Diff;
                                        output += 11 + Loadable.Diff;
                                        output += 8 + Loadable.Diff;

                                        for (int i = 0; i < 8; i++) {
                                            output += i + Loadable.Diff + "식당" + i + Loadable.Diff + 1 + Loadable.Diff;
                                        }

                                        break;
                                }

                                break;

                            case MainRestaurantsPacket.Code: //음식점 고유번호/리뷰사진
                                switch (Convert.ToInt32(data[1])) {
                                    case 0:
                                        output += 8 + Loadable.Diff;

                                        for (int i = 0; i < 8; i++) {
                                            output += 0 + Loadable.Diff + 0 + Loadable.Diff;
                                        }

                                        break;
                                }

                                break;

                            case 2: //간단리뷰 : 안함
                                Debug.LogError("Invalid Data");

                                break;

                            case DetailReviewPacket.Code:
                                switch (Convert.ToInt32(data[1])) {
                                    case 0:
                                        output += "식당1" + Loadable.Diff;
                                        output += 4.5 + Loadable.Diff;
                                        output += "너네집앞은 아님" + Loadable.Diff;
                                        output += "010-1111-2222" + Loadable.Diff;
                                        output += 8 + Loadable.Diff;

                                        for (int i = 0; i < 8; i++) {
                                            output += 0 + Loadable.Diff + 0 + Loadable.Diff;
                                            output += i + Loadable.Diff + System.DateTime.Now.ToString("yyyy-mm-dd") + Loadable.Diff;
                                        }

                                        break;
                                }

                                break;

                            case ImageDownloadPacket.Code: //이미지
                                switch (Convert.ToInt32(data[1])) {
                                    case 0:
                                        outputBytes = container.userProfileBytes;

                                        break;
                                }

                                break;

                            case SearchRestaurantPacket.Code: //검색결과
                                output += 8 + Loadable.Diff;

                                for (int i = 0; i < 8; i++) {
                                    output += 0 + Loadable.Diff + 0 + Loadable.Diff;
                                }
                                break;
                            case ReviewWritePacket.Code://글쓰기
                                output += true + Loadable.Diff;

                                break;

                            default:
                                Debug.LogError("Parsing Failed");

                                break;
                        }


                        buffer = Encoding.UTF8.GetBytes(output);
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Write(outputBytes, buffer.Length, outputBytes.Length);
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