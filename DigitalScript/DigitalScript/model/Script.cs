using System.Collections.Generic;

namespace DigitalScript.model
{
    class Script
    {        
        private int id;
        private string title;
        private List<Actor> actorLst;
        private List<Scene> sceneLst;
        private List<Emotion> emotionLst;

        public void SetID(int id)
        {
            this.id = id;
        }

        public void SetTitle(string title)
        {
            this.title = title;
        }

        public void SetActorList(List<Actor> actorLst)
        {
            this.actorLst = actorLst;
        }

        public void SetSceneList(List<Scene> sceneLst)
        {
            this.sceneLst = sceneLst;
        }

        public void SetEmotionList(List<Emotion> emotionLst)
        {
            this.emotionLst = emotionLst;
        }

        public int GetID()
        {
            return id;
        }

        public string GetTitle()
        {
            return title;
        }

        public List<Actor> GetActorList()
        {
            return actorLst;
        }

        public List<Scene> GetSceneList()
        {
            return sceneLst;
        }
        
        public List<Emotion> GetEmotionList()
        {
            return emotionLst;
        }        

    }
}
