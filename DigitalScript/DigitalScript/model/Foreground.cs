namespace DigitalScript.model
{
    class Foreground : Picture
    {
        public void SetId(int id)
        {
            this.id = id;
        }

        public void SetImgPath(int imgPath)
        {
            this.imgPath = imgPath;
        }

        public void GetId()
        {
            return id;
        }

        public void GetImgPath()
        {
            return imgPath;
        }
    }
}
