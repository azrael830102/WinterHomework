using System.Collections.Generic;

namespace DigitalScript.model
{
    class Actor
    {   
        private string id;      //UID
        private string name;
        private List<Clothing> clothingLst;

        public void SetId(string id)
        {
            this.id = id;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetClothingList(List<Clothing> clothingLst)
        {
            this.clothingLst = clothingLst;
        }
       
        public string GetId()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }

        public List<Clothing> GetClothingList()
        {
            return clothingLst;
        }

    }
}
