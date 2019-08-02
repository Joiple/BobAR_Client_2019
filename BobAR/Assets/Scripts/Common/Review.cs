using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Review:ILoadable
    {
        public List<Sprite> pictures=new List<Sprite>();
        public int author;
        public string content;

    }
}