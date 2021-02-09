using DigitalScript.controller.Tools;
using DigitalScript.model;
using System;
using System.Collections.Generic;

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

            string sql = "SELECT * FROM films WHERE id = " + id;
            List<Dictionary<string, object>> lst = DBTools.Query(sql, base.GetConnection());
            Dictionary<string, object> item = lst[0];

            script.SetId(Convert.ToInt16(id));
            script.SetTitle(Convert.ToString(item["name"]));
            script.SetActorList(GetActor(Convert.ToString(item["actors_list"]).Split(','))); // Column 'actors_list' not existed.
            script.SetSceneList(GetScene(Convert.ToString(item["scenes_list"]).Split(','))); // Column 'scenes_list' not existed.
            script.SetEmotionList(GetEmotion()); // Get all emotions here.

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

            foreach (string id in id_list)
            {
                Scene scene = new Scene();

                string sql = "SELECT * FROM scenes WHERE id = " + id;
                List<Dictionary<string, object>> lst = DBTools.Query(sql, base.GetConnection());
                Dictionary<string, object> item = lst[0];

                scene.SetId(Convert.ToInt16(id));
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

            foreach (string id in id_list)
            {
                Actor actor = new Actor();

                string sql = "SELECT * FROM actors WHERE id = " + id;  // Table 'actors' not existed. 
                List<Dictionary<string, object>> lst = DBTools.Query(sql, base.GetConnection());
                Dictionary<string, object> item = lst[0];

                actor.SetId(id);
                actor.SetName((string)item["name"]);
                actor.SetClothingList(GetClothing(Convert.ToString(item["clothes_list"]).Split(',')));

                actors.Add(actor);
            }

            return actors;
        }

        public List<Clothing> GetClothing(string[] id_list)
        {
            List<Clothing> clothes = new List<Clothing>();
            //...
            return clothes;
        }

        public List<Foreground> GetForeground(string[] id_list)
        {
            List<Foreground> foregrounds = new List<Foreground>();
            //...
            return foregrounds;
        }

        public List<Line> GetLine(string[] id_list)
        {
            List<Line> lines = new List<Line>();
            //...
            return lines;
        }

        //Get all emotions.
        public List<Emotion> GetEmotion()
        {
            List<Emotion> emotions = new List<Emotion>();
            //...
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
                    clothes.id = Convert.ToInt16(res["id"]);
                    clothes.imgPath = Convert.ToString(res["file_path"]);
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
                    background.id = Convert.ToInt16(res["id"]);
                    background.imgPath = Convert.ToString(res["name"]);
                }
            }

            background.x = 0;
            background.y = 0;
            background.rotation = 0;

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
                    foreground.id = Convert.ToInt16(res["id"]);
                    foreground.imgPath = Convert.ToString(res["name"]);
                }
            }

            foreground.x = 0;
            foreground.y = 0;
            foreground.rotation = 0;

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
                    emotion.id = Convert.ToInt16(res["id"]);
                    emotion.type = Convert.ToString(res["emotion_type"]);
                }
            }

            emotion.x = 0;
            emotion.y = 0;
            emotion.rotation = 0;
            emotion.imgPath = null;

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
                    line.id = Convert.ToInt16(res["id"]);
                    line.type = Convert.ToInt16(res["type"]);
                    line.content = Convert.ToString(res["name"]);

                    /*line.has_skeleton = Convert.ToInt16(res["has_skeleton"]);
                    line.robot_face = Convert.ToString(res["robot_face"]);
                    line.robot_action = Convert.ToString(res["robot_action"]);
                    line.robot_sound = Convert.ToString(res["robot_sound"]);
                    line.robot_isON = Convert.ToInt16(res["robot_isON"]);
                    line.type4FileName = Convert.ToString(res["type4FileName"]);
                    line.sentiment = Convert.ToString(res["sentiment"]);*/

                }
            }
                        

            return line;
        }
    }
}
