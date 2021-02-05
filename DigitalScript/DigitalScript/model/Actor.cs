using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalScript.model
{
    class Actor
    {
        public Guid id;  //UID
        public string name;

        public List<Clothing> Clothes { get; set; }
    }
}
