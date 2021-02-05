using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalScript.model
{
    class Script
    {
        public int id;
        public string title;

        public List<Actor> Actors { get; set; }
        public List<Scene> Scenes { get; set; }
        public List<Emotion> Emotions { get; set; }
    }
}
