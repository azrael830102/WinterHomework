using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalScript.model
{
    abstract class Picture
    {
        public int id; // from DB
        public int x;
        public int y;
        public float rotation;
        public string imgPath; // from DB        
    }
}
