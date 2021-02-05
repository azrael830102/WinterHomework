using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalScript.model
{
    class Scene
    {
        private int id;
        private int backgroundId;
        private List<Actor> actorLst;
        private List<Foreground> foregroundLst;
        private List<Line> lineLst;

        public void SetId(int id)
        {
            this.id = id;
        }

        public void SetbackgroundId(int backgroundId)
        {
            this.backgroundId = backgroundId;
        }

        public void SetActorList(List<Actor> actorLst)
        {
            this.actorLst = actorLst;
        }

        public void SetForegroundList(List<Foreground> foregroundLst)
        {
            this.foregroundLst = foregroundLst;
        }

        public void SetLineList(List<Line> lineLst)
        {
            this.lineLst = lineLst;
        }

        public int GetId()
        {
            return id;
        }

        public int GetBackgroundId()
        {
            return backgroundId;
        }

        public List<Actor> GetActorList()
        {
            return actorLst;
        }

        public List<Foreground> GetForegroundList()
        {
            return foregroundLst;
        }

        public List<Line> GetLineList()
        {
            return lineLst;
        }
    }
}
