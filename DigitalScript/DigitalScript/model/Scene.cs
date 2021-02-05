using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalScript.model
{
    class Scene
    {
        public int id;
        public int background_id;

        public List<Actor> Actors { get; set; }
        public List<Emotion> Emotions { get; set; }
        public List<Line> Lines { get; set; }
    }
}
