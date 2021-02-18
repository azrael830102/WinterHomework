using System.Collections.Generic;

namespace DigitalScript.model
{
    class Actor
    {   
        private string id;      //UID
        private string name;
        private List<Clothes> clothingLst;

        public void SetID(string id)
        {
            this.id = id;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetClothingList(List<Clothes> clothingLst)
        {
            this.clothingLst = clothingLst;
        }
       
        public string GetID()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }

        public List<Clothes> GetClothingList()
        {
            return clothingLst;
        }

    }
}
