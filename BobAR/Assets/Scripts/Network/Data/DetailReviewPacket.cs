using System;

namespace Network.Data {

    public class DetailReviewPacket : Loadable {
        public new const int Code = 3;
        public string restaurantName,
                      address,
                      phoneNumber;

        public float restaurantAvgPoint;
        public int reviewNum;

        public string[] reviewIds,
                        imageFileIds,
                        date;

        public int[] likes;

        public static string ParsePacket(string restaurantId) {
            return Code + Diff + restaurantId + Diff;
        }
        public override Loadable Load(string input) {
            string[] data = input.Split(new[] {
                Diff
            }, StringSplitOptions.None);

            int counter = 0;
            restaurantName = data[counter++];
            restaurantAvgPoint = Convert.ToSingle(data[counter++]);
            address = data[counter++];
            phoneNumber = data[counter++];
            reviewNum = Convert.ToInt32(data[counter++]);
            reviewIds = new string[reviewNum];
            imageFileIds = new string[reviewNum];
            date = new string[reviewNum];
            likes = new int[reviewNum];

            for (int i = 0; i < reviewNum; i++) {
                reviewIds[i] = data[counter++];
                imageFileIds[i] = data[counter++];
                likes[i] = Convert.ToInt32(data[counter++]);
                date[i] = data[counter++];
            }

            return this;
        }
    }

}