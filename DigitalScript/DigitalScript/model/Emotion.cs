namespace DigitalScript.model
{
    class Emotion: Picture
    {
        private string type;

        public void SetId(int id)
        {
            this.id = id;
        }

        public void SetType(string type)
        {
            this.type = type;
        }

        public void GetId()
        {
            return id;
        }

        public int GetType()
        {
            return type;
        }
    }
}
