
namespace DigitalScript.model
{
    class Line
    {
        private int id; // from DB
        private int linesType;
        private string content; // from DB 

        /*
        private int has_skeleton;
        private string robot_face;
        private string robot_action;
        private string robot_sound;
        private int robot_isON;
        private string type4FileName;
        private string sentiment;
        */

        public void SetId(int id)
        {
            this.id = id;
        }

        public void SetLinesType(int linesType)
        {
            this.linesType = linesType;
        }

        public void SetContent(string content)
        {
            this.content = content;
        }

        /*public void SetHas_skeleton(int has_skeleton)
        {
            this.has_skeleton = has_skeleton;
        }

        public void SetRobot_face(string robot_face)
        {
            this.robot_face = robot_face;
        }

        public void SetRobot_action(string robot_action)
        {
            this.robot_action = robot_action;
        }

        public void SetRobot_sound(string robot_sound)
        {
            this.robot_sound = robot_sound;
        }

        public void SetRobot_isON(int robot_isON)
        {
            this.robot_isON = robot_isON;
        }

        public void SetType4FileName(string type4FileName)
        {
            this.type4FileName = type4FileName;
        }

        public void SetSentiment(string sentiment)
        {
            this.sentiment = sentiment;
        }*/

        public int GetId()
        {
            return id;
        }

        public int GetLinesType()
        {
            return linesType;
        }

        public string GetContent()
        {
            return content;
        }

        /*public void GetHas_skeleton()
        {
            return has_skeleton;
        }

        public void GetRobot_face()
        {
            return robot_face;
        }

        public void GetRobot_action( )
        {
            return robot_action;
        }

        public void GetRobot_sound( )
        {
            return robot_sound;
        }

        public void GetRobot_isON( )
        {
            return robot_isON;
        }

        public void GetType4FileName( )
        {
            return type4FileName;
        }

        public void GetSentiment( )
        {
            return sentiment;
        }*/


    }
}
