
namespace DigitalScript.model
{
    abstract class Picture
    {
        protected string id; // from DB
        protected int x;
        protected int y;
        protected float rotation;
        protected string imgPath; // from DB        

        public void SetID(string id)
        {
            this.id = id;
        }

        public string GetID()
        {
            return id;
        }

        public void SetX(int x)
        {
            this.x = x;
        }

        public int GetX()
        {
            return x;
        }

        public void SetY(int y)
        {
            this.y = y;
        }

        public int GetY()
        {
            return y;
        }

        public void SetRotation(float rotation)
        {
            this.rotation = rotation;
        }

        public float GetRotation()
        {
            return rotation;
        }

        public void SetImgPath(string imgPath)
        {
            this.imgPath = imgPath;
        }

        public string GetImgPath()
        {
            return imgPath;
        }
    }
}
