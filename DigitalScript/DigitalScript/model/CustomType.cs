using System;
using System.Collections.Generic;

namespace DigitalScript
{
    public enum MaterialType
    {
        Bgimg,
        Fgimg,
        Char,
        Mask,
        Clothes,
        Puppet
    }
    /// <summary>
    /// Add your own enum type in here, and you can use this class(<see cref="CustomType"/>)
    /// to deal with the enum type you just defined
    /// </summary>
    /// 
    class CustomType
    {
        public static string ToString(MaterialType type)
        {
            switch (type)
            {
                case MaterialType.Bgimg:
                    return "backgrounds";
                case MaterialType.Fgimg:
                    return "foregrounds";
                case MaterialType.Char:
                    return "props";
                case MaterialType.Mask:
                    return "masks";
                case MaterialType.Clothes:
                    return "clothes";
                case MaterialType.Puppet:
                    return "puppets";
                default:
                    return "";
            }
        }
    }

    class Emotion
    {

    }

    class Foreground
    {

    }
    
    class Line
    {

    }

    class Clothing
    {

    }

}
