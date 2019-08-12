using System;

namespace Network.Data {

    public class SearchRestaurantPacket :Loadable {
        public new const int Code = 5;

        public static string ParsePacket(string query) {
            return Code + Diff + query + Diff;
        }

        public int restaurantNum;

        public string[] restaurantIds,
                        imageFileIds;

        public override Loadable Load(string input) {
            string[] data = input.Split(new[] {
                Diff
            }, StringSplitOptions.None);

            int counter = 0;
            restaurantNum = Convert.ToInt32(data[counter++]);
            restaurantIds=new string[restaurantNum];
            imageFileIds=new string[restaurantNum];

            for (int i = 0; i < restaurantNum; i++) {
                restaurantIds[i] = data[counter++];
                imageFileIds[i] = data[counter++];
            }
            return this;
        }
    }

}