using System;
using Common.Dummies;

namespace Network.Data {

    public class MyPagePacket:Loadable {
        public new const int Code = 0;
        public static string ParsePacket(string userId) {
            return Code + Diff + userId + Diff;
        }

        public string nickName,profileFileName;

        public int followerNum,
                   followingNum,
                   reviewNum;

        public  string[] reviewIds,
                         restaurantNames,
                         imageFileNames;

        public override Loadable Load(string input) {
            string[] data = input.Split(new []{Diff},StringSplitOptions.None);
            int counter = 0;
            nickName = data[counter++];
            profileFileName = data[counter++];
            followerNum = Convert.ToInt32(data[counter++]);
            followingNum = Convert.ToInt32(data[counter++]);
            reviewNum = Convert.ToInt32(data[counter++]);
            
            reviewIds=new string[reviewNum];
            restaurantNames=new string[reviewNum];
            imageFileNames=new string[reviewNum];

            for (int i = 0; i < reviewNum; i++) {
                reviewIds[i] = data[counter++];
                restaurantNames[i] = data[counter++];
                imageFileNames[i] =data[counter++];
            }

            return this;
        }

        public override Loadable LoadBinary(byte[] input) {
            throw new NotImplementedException();
        }
    }

}