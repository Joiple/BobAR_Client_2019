using NUnit.Framework.Constraints;
using UnityEngine;

namespace Network.Data{

    public class ImageDownloadPacket : Loadable {
        public new const int Code=4;
        public static string ParsePacket(string imageId) {
            return Code + Diff+imageId + Diff;
        }
        public Texture2D tex;
        

        public override Loadable LoadBinary(byte[] input) {
            tex.LoadImage(input);

            return this;
        }
    }

}