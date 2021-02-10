namespace DigitalScript.model
{
    class Foreground : Picture
    {
        public void SetId(int id)
        {
            this.id = id;
        }

        public void SetImgPath(string imgPath)
        {
            this.imgPath = imgPath;
        }

        public int GetId()
        {
            return id;
        }

        public string GetImgPath()
        {
            return imgPath;
        }
    }
}
