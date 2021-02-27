using DigitalScript.controller.Tools;
using DigitalScript.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalScript.controller
{
    class ScriptDao : DaoObject
    {

        /// <summary>
        /// Get script with script id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Script GetScript(string id)
        {
            Script script = new Script();

            string sql = "SELECT * FROM films WHERE id = '" + id + "'";
            List<Dictionary<string, object>> lst = DBTools.Query(sql, base.GetConnection());
            Dictionary<string, object> item = lst[0];

            script.SetID(Convert.ToInt16(id));
            script.SetTitle(Convert.ToString(item["name"]));
            script.SetActorList(GetActor(Convert.ToString(item["actors_list"]).Split(','))); // Column 'actors_list' not existed.
            script.SetSceneList(GetScene(Convert.ToString(item["scenes_list"]).Split(','))); // Column 'scenes_list' not existed.
            script.SetEmotionList(GetEmotion());

            return script;
        }

        /// <summary>
        /// Get scenes with scene id list
        /// </summary>
        /// <param name="id_list"></param>
        /// <returns></returns>
        public List<Scene> GetScene(string[] id_list)
        {
            List<Scene> scenes = new List<Scene>();
            
            HashSet<String> tmpSet = new HashSet<string>();
            foreach (string id in id_list)
            {
                tmpSet.Add("'" + id + "'");
            }
            string idlst = string.Join(",", tmpSet);
                                 
            string sql = "SELECT * FROM scenes WHERE id IN (" + idlst + ")";
            List<Dictionary<string, object>> lst = DBTools.Query(sql, base.GetConnection());
            
            foreach (Dictionary<string, object> item in lst)
            {
                Scene scene = new Scene();
                
                scene.SetID(Convert.ToInt16(item["id"]));
                scene.SetbackgroundId(Convert.ToInt16(item["background_id"]));
                scene.SetActorList(GetActor(Convert.ToString(item["actors_list"]).Split(','))); // Column 'actors_list' not existed.
                scene.SetForegroundList(GetForeground(Convert.ToString(item["foregrounds_list"]).Split(','))); // Column 'foregrounds_list' not existed.
                scene.SetLineList(GetLine(Convert.ToString(item["lines_list"]).Split(','))); // Column 'lines_list' not existed.

                scenes.Add(scene);
            }

            return scenes;
        }

        /// <summary>
        /// Get actors with actor id list
        /// </summary>
        /// <param name="id_list"></param>
        /// <returns></returns>
        public List<Actor> GetActor(string[] id_list)
        {
            List<Actor> actors = new List<Actor>();

            string sql = "SELECT * FROM actors WHERE id IN ('" + string.Join("','", id_list) + "')";  // Table 'actors' not existed. 

            List<Dictionary<string, object>> lst = DBTools.Query(sql, base.GetConnection());    
            
            foreach (Dictionary<string, object> item in lst)
            {
                Actor actor = new Actor();

                actor.SetID(Convert.ToString(item["id"]));
                actor.SetName((string)item["name"]);
                actor.SetClothingList(GetClothing(Convert.ToString(item["clothes_list"]).Split(',')));

                actors.Add(actor);
            }

            return actors;
        }

        /// <summary>
        /// Get clothes with clothing id list
        /// </summary>
        /// <param name="id_list"></param>
        /// <returns></returns>
        public List<Clothes> GetClothing(string[] id_list)
        {
            List<Clothes> clothes = new List<Clothes>();

            string sql = "SELECT * FROM costume WHERE id IN ('" + string.Join("','", id_list) + "')";  
            List<Dictionary<string, object>> lst = DBTools.Query(sql, base.GetConnection());

            foreach (Dictionary<string, object> item in lst)
            {
                Clothes clothing = new Clothes();

                clothing.SetID(Convert.ToString(item["id"]));
                clothing.SetImgPath(Convert.ToString(item["file_path"]));
                // clothing.SetIndex(Convert.ToInt32(item["index"])); // the column is not exist
                // clothing.SetRatio(Convert.ToSingle(item["ratio"])); // the column is not exist

                clothes.Add(clothing);
            }

            return clothes;
        }

        /// <summary>
        /// Get foregounds with foreground id list
        /// </summary>
        /// <param name="id_list"></param>
        /// <returns></returns>
        public List<Foreground> GetForeground(string[] id_list)
        {
            List<Foreground> foregrounds = new List<Foreground>();

            string sql = "SELECT * FROM foregrounds WHERE id IN ('" + string.Join("','", id_list) + "')";
            List<Dictionary<string, object>> lst = DBTools.Query(sql, base.GetConnection());

            foreach (Dictionary<string, object> item in lst)
            {
                Foreground foreground = new Foreground();

                foreground.SetID(Convert.ToString(item["id"]));
                foreground.SetImgPath(Convert.ToString(item["name"]));            
                /*
                foreground.x = 0;
                foreground.y = 0;
                foreground.rotation = 0;
                */

                foregrounds.Add(foreground);
            }

            return foregrounds;
        }

        /// <summary>
        /// Get lines with line id list
        /// </summary>
        /// <param name="id_list"></param>
        /// <returns></returns>
        public List<Line> GetLine(string[] id_list)
        {
            List<Line> lines = new List<Line>();

         
            string sql = "SELECT * FROM sounds WHERE id IN ('" + string.Join("','", id_list) + "')";
            List<Dictionary<string, object>> lst = DBTools.Query(sql, base.GetConnection());

            foreach (Dictionary<string, object> item in lst)
            {
                Line line = new Line();

                line.SetId(Convert.ToInt16(item["id"]));
                line.SetLinesType(Convert.ToInt16(item["type"]));
                line.SetContent(Convert.ToString(item["name"]));
                /*
                line.SetHas_skeleton(Convert.ToInt16(item["has_skeleton"]));
                line.SetRobot_face(Convert.ToString(item["robot_face"]));
                line.SetRobot_action(Convert.ToString(item["robot_action"]));
                line.SetRobot_sound(Convert.ToString(item["robot_sound"]));
                line.SetRobot_isON(Convert.ToInt16(item["robot_isON"]));
                line.SetType4FileName(Convert.ToString(item["type4FileName"]));
                line.SetSentiment(Convert.ToString(item["sentiment"]));
                */

                lines.Add(line);
            }

            return lines;
        }

        /// <summary>
        /// Get all emotions
        /// </summary>
        /// <returns></returns>
        public List<Emotion> GetEmotion()
        {
            List<Emotion> emotions = new List<Emotion>();

            string sql = "SELECT * FROM emotion";
            List<Dictionary<string, object>> lst = DBTools.Query(sql, base.GetConnection());

            if (lst != null)
            {
                foreach (Dictionary<string, object> item in lst)
                {
                    Emotion emotion = new Emotion();

                    emotion.SetID(Convert.ToString(item["id"]));
                    emotion.SetIndex((BonesType)Convert.ToInt16(item["emotion_type"]));


                    /*emotion.SetX(0);
                    emotion.y = 0;
                    emotion.rotation = 0;
                    emotion.imgPath = null;
                     emotion.SetImage();
                    */

                    emotions.Add(emotion);
                }
            }

            return emotions;
        }


        /// <summary>
        /// Get clothes with clothes_Id
        /// </summary>
        /// <param name="clothes_Id"></param>
        /// <returns></returns>
        public Clothes GetClothes(string clothes_Id)
        {
            Clothes clothes = new Clothes();
            string sql = "SELECT * FROM costume WHERE id = '" + clothes_Id + "'";
            List<Dictionary<string, object>> result = DBTools.Query(sql, base.GetConnection());

            if (result != null)
            {
                foreach (Dictionary<string, object> res in result)
                {
                    clothes.SetID(Convert.ToString(res["id"]));
                    clothes.SetImgPath(Convert.ToString(res["file_path"]));
                    clothes.SetImage();
                    // clothes.index = Convert.ToInt32(res["index"]); // the column is not exist
                    // clothes.ratio = Convert.ToSingle(res["ratio"]); // the column is not exist
                }
            }

            return clothes;
        }

        /// <summary>
        /// Get background with bg_Id
        /// </summary>
        /// <param name="bg_Id"></param>
        /// <returns></returns>
        public Background GetBackground(string bg_Id)
        {
            Background background = new Background();
            string sql = "SELECT * FROM backgrounds WHERE id = '" + bg_Id + "'";
            List<Dictionary<string, object>> result = DBTools.Query(sql, base.GetConnection());

            if (result != null)
            {
                foreach (Dictionary<string, object> res in result)
                {
                    background.SetID(Convert.ToString(res["id"]));
                    background.SetImgPath(Convert.ToString(res["name"]));
                    background.SetImage();
                }
            }

            background.SetX(0);
            background.SetY(0);
            background.SetRotation(0);

            return background;
        }

        /// <summary>
        /// Get foreground with fg_Id
        /// </summary>
        /// <param name="fg_Id"></param>
        /// <returns></returns>
        public Foreground GetForeground(string fg_Id)
        {
            Foreground foreground = new Foreground();
            string sql = "SELECT * FROM foregrounds WHERE id = '" + fg_Id + "'";
            List<Dictionary<string, object>> result = DBTools.Query(sql, base.GetConnection());

            if (result != null)
            {
                foreach (Dictionary<string, object> res in result)
                {
                    foreground.SetID(Convert.ToString(res["id"]));
                    foreground.SetImgPath(Convert.ToString(res["name"]));
                    foreground.SetImage();
                }
            }

            /*foreground.x = 0;
            foreground.y = 0;
            foreground.rotation = 0;*/

            return foreground;
        }

        /// <summary>
        /// Get emotion with em_Id
        /// </summary>
        /// <param name="em_Id"></param>
        /// <returns></returns>
        public Emotion GetEmotion(string em_Id)
        {
            Emotion emotion = new Emotion();
            string sql = "SELECT * FROM emotion WHERE id = '" + em_Id + "'";
            List<Dictionary<string, object>> result = DBTools.Query(sql, base.GetConnection());

            if (result != null)
            {
                foreach (Dictionary<string, object> res in result)
                {
                    emotion.SetID(Convert.ToString(res["id"]));
                    emotion.SetIndex((BonesType)Convert.ToInt16(res["emotion_type"]));
                    emotion.SetImage();
                }
            }

            /*emotion.x = 0;
            emotion.y = 0;
            emotion.rotation = 0;
            emotion.imgPath = null;*/

            return emotion;
        }

        /// <summary>
        /// Get line with line_Id
        /// </summary>
        /// <param name="line_Id"></param>
        /// <returns></returns>
        public Line GetLine(string line_Id)
        {
            Line line = new Line();
            string sql = "SELECT * FROM sounds WHERE id = '" + line_Id + "'";
            List<Dictionary<string, object>> result = DBTools.Query(sql, base.GetConnection());

            if (result != null)
            {
                foreach (Dictionary<string, object> res in result)
                {
                    line.SetId(Convert.ToInt16(res["id"]));
                    line.SetLinesType(Convert.ToInt16(res["type"]));
                    line.SetContent(Convert.ToString(res["name"]));

                    /*
                    line.SetHas_skeleton(Convert.ToInt16(res["has_skeleton"]));
                    line.SetRobot_face(Convert.ToString(res["robot_face"]));
                    line.SetRobot_action(Convert.ToString(res["robot_action"]));
                    line.SetRobot_sound(Convert.ToString(res["robot_sound"]));
                    line.SetRobot_isON(Convert.ToInt16(res["robot_isON"]));
                    line.SetType4FileName(Convert.ToString(res["type4FileName"]));
                    line.SetSentiment(Convert.ToString(res["sentiment"]));
                    */

                }
            }
                        

            return line;
        }
    }
}
