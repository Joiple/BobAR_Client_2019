using System;
using ARComponents;
using Common;
using UnityEditor;

namespace MainScene {

    public class Restaurant:ILoadable
    {
        public string name;
        public int startTime, endTime;
        public Poi locator;

    }

}
