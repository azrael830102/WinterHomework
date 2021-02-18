
namespace DigitalScript.model
{
    class Clothes: Picture
    {
        BonesType index; 
        float ratio;

        public void SetIndex(BonesType index)
        {
            this.index = index;
        }


        public BonesType GetIndex()
        {
            return index;
        }

        public void SetRatio(float ratio)
        {
            this.ratio = ratio;
        }

        public float GetRatio()
        {
            return ratio;
        }
    }
}
