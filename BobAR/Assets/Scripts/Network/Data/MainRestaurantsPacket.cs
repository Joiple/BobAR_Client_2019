namespace Network.Data {

    public class MainRestaurantsPacket : SearchRestaurantPacket {
        public new const int Code=1;
        public static string ParsePacket() {
            return Code+Diff;
        }
    }

}