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

    class Script
    {
        public int id;
        public string title;

        public List<Actor> Actors { get; set; }
        public List<Scene> Scenes { get; set; }
        public List<Emotion> Emotions { get; set; }
    }

    class Scene
    {
        public int id;
        public int background_id;

        public List<Actor> Actors { get; set; }
        public List<Emotion> Emotions { get; set; }
        public List<Line> Lines { get; set; }
    }

    class Actor
    {
        public Guid id;  //UID
        public string name;

        public List<Clothing> Clothes { get; set; }
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
