using System;

namespace Network.Data {

    public class RestaurantMetaPacket :Loadable {
        public new const int Code = 2;

        public static string ParsePacket(string restaurantId) {
            return Code + Diff + restaurantId + Diff;
        }

        private string name,
                       address,
                       phoneNum,
                       reviewText;
        public override Loadable Load(string input) {
            string[] data = input.Split(new[] {
                Diff
            }, StringSplitOptions.None);

            int count = 0;
            name = data[count++];
            address= data[count++];
            phoneNum = data[count++];
            reviewText = data[count++];

            return this;
        }
    }

}