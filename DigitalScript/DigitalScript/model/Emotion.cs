
namespace DigitalScript.model
{
    class Emotion: Picture
    {
        BonesType index;

        public void SetIndex(BonesType index)
        {
            this.index = index;
        }

       
        public BonesType GetIndex()
        {
            return index;
        }
    }
}
