namespace Network.Data {

    public class ReviewWritePacket : Loadable {
        public new const int Code = 7;

        public static string ParsePacket(string userId, string restaurantId, string text, int tastePoint, int cleanPoint, int kindnessPoint, int moodPoint, int costPoint) {
            return Code + Diff + userId + Diff+restaurantId+Diff+text+Diff+cleanPoint+Diff+kindnessPoint+Diff+moodPoint+Diff+costPoint+Diff;
        }
        public override Loadable Load(string input) {
            return this;
        }
        
    }

}