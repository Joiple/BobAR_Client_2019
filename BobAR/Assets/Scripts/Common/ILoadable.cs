using UnityEditor;

namespace Common {

    public class ILoadable {
        internal const char diff = '\\';
        public virtual ILoadable Load(string input){
            return this;
        }
    }

}