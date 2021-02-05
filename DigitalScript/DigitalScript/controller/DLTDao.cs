using DigitalScript.model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DigitalScript
{
    /// <summary>
    /// All kinds of db access action
    /// </summary>
    class DLTDao
    {
        MySqlConnection conn;

        /// <summary>
        /// After done with DB access remember to close the connection
        /// </summary>
        DLTDao()
        {
            conn = DBTools.GetConnection();
            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Close the connecetion
        /// </summary>
        public void CloseConnection()
        {
            conn.Close();
        }

        /// <summary>
        /// Get films
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetFilms()
        {
            string sql = "SELECT * FROM films";
            return DBTools.Query(sql, conn);
        }

        /// <summary>
        /// Get films with groupId
        /// </summary>
        /// <param name="group_Id"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetOwnFilms(string group_Id)
        {
            string sql = "SELECT * FROM films WHERE group_id = '" + group_Id + "'";
            return DBTools.Query(sql, conn);
        }

        /// <summary>
        /// get Subject Films
        /// </summary>
        /// <param name="subject_Id"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetSubjectFilms(string subject_Id)
        {
            string sql = "SELECT * FROM films WHERE subjects_id = " + subject_Id;
            return DBTools.Query(sql, conn);
        }

        /// <summary>
        /// Get subject
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetSubject()
        {
            string sql = "SELECT * FROM subjects";
            return DBTools.Query(sql, conn);
        }

        /// <summary>
        /// Get scenes
        /// </summary>
        /// <param name="film_Id"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetScenes(string film_Id)
        {
            string sql = "SELECT scenes.*, backgrounds.name as BgImg, foregrounds.name as FgImg,props.name as PropImg,masks.name as MaskImg ,clothes.name as ClothImg,puppets.name as PuppetImg " +
                              "FROM(select * from `film_scenesorder`where `film_id`= '" + film_Id + "') temp " +
                              "JOIN scenes on temp.`scene_id`= scenes.id " +
                              "JOIN backgrounds ON scenes.background_id = backgrounds.id " +
                              "LEFT JOIN foregrounds ON scenes.`foreground_id` = foregrounds.id " +
                              "LEFT JOIN clothes ON scenes.`cloth_id` = clothes.id " +
                              "LEFT JOIN props ON scenes.`prop_id` = props.id " +
                              "LEFT JOIN masks ON scenes.`mask_id` = masks.id " +
                              "LEFT JOIN puppets ON scenes.`puppet_id` = puppets.id " +
                                "order by `scenesorder`";
            return DBTools.Query(sql, conn);
        }

        /// <summary>
        /// Get scenes' sounds
        /// </summary>
        /// <param name="scene_Id"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetScenesSounds(string scene_Id)
        {
            string sql = "SELECT sounds.* " +
                            "FROM scene_soundsorder " +
                            "JOIN sounds ON scene_soundsorder.sound_id = sounds.id " +
                                "WHERE scene_id = '" + scene_Id + "' " +
                                "ORDER BY scene_soundsorder.soundsorder ASC";
            return DBTools.Query(sql, conn);
        }

        /// <summary>
        /// Get materials with material type(<see cref="MaterialType"/>)
        /// </summary>
        /// <param name="material_type"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetMaterials(MaterialType material_type)
        {
            string sql = "SELECT * FROM " + CustomType.ToString(material_type);
            return DBTools.Query(sql, conn);
        }

        /// <summary>
        /// Get specific materials with material type(<see cref="MaterialType"/>) and groupId
        /// </summary>
        /// <param name="material_type"></param>
        /// <param name="group_Id"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetOwnMaterials(MaterialType material_type, string group_Id)
        {
            string sql = "SELECT * FROM " + CustomType.ToString(material_type) + " WHERE group_id =" + group_Id;
            return DBTools.Query(sql, conn);
        }


        /// <summary>
        /// Get dialogues with sceneId
        /// </summary>
        /// <param name="scene_Id"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetDialogues(string scene_Id)
        {
            string sql = "SELECT dialogues.* " +
                            "FROM dialogues " +
                            "JOIN characters_dialogues ON dialogues.id = characters_dialogues.dialogues_id " +
                            "JOIN characters ON characters.id = characters_dialogues.character_id " +
                                "WHERE characters.scene_id = '" + scene_Id + "'";
            return DBTools.Query(sql, conn);
        }

        /// <summary>
        /// Get all sounds
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetSounds()
        {
            string sql = "SELECT * FROM sounds ORDER BY id DESC";
            return DBTools.Query(sql, conn);
        }

        /// <summary>
        /// Get sounds with group_id
        /// </summary>
        /// <param name="group_Id"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetOwnSounds(string group_Id)
        {
            string sql = "SELECT * FROM sounds WHERE group_id = " + group_Id + " ORDER BY id DESC";
            return DBTools.Query(sql, conn);
        }

        /// <summary>
        /// Get script with script id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Script GetScript(string id)
        {
            Script script = new Script();
            string[] id_list; //id list of actors/scenes.

            string sql = "SELECT * FROM films WHERE id = " + id;
            List<Dictionary<string, object>> lst = DBTools.Query(sql, conn);            
            Dictionary<string, object> item = lst[0];

            script.SetId(Int16.Parse(id));
            script.SetTitle((string)item["name"]);
            string tmp = (string)item["actors_list"];  // Column 'actors_list' not existed.
            string tmp2 = (string)item["scenes_list"]; // Column 'scenes_list' not existed.
            id_list = tmp.Split(',');    
            script.SetActorList(GetActor(id_list));
            id_list = tmp2.Split(',');
            script.SetSceneList(GetScene(id_list));
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
            string[] id_list2; // id list of actors/foregrounds/lines.

            foreach (string id in id_list)
            {
                Scene scene = new Scene();

                string sql = "SELECT * FROM scenes WHERE id = " + id;   
                List<Dictionary<string, object>> lst = DBTools.Query(sql, conn);
                Dictionary<string, object> item = lst[0];

                scene.SetId(Int16.Parse(id));
                scene.SetbackgroundId((int)item["background_id"]);
                string tmp = (string)item["actors_list"];          // Column 'actors_list' not existed.
                string tmp2 = (string)item["foregrounds_list"];    // Column 'foregrounds_list' not existed.
                string tmp3 = (string)item["lines_list"];          // Column 'lines_list' not existed.                    
                id_list2 = tmp.Split(',');
                scene.SetActorList(GetActor(id_list2));
                id_list2 = tmp2.Split(',');
                scene.SetForegroundList(GetForeground(id_list2));
                id_list2 = tmp3.Split(',');
                scene.SetLineList(GetLine(id_list2));

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
            string[] id_list2; // id list of clothes.
                       
            foreach (string id in id_list)
            {
                Actor actor = new Actor();

                string sql = "SELECT * FROM actors WHERE id = " + id;  // Table 'actors' not existed. 
                List<Dictionary<string, object>> lst = DBTools.Query(sql, conn);
                Dictionary<string, object> item = lst[0];
                
                actor.SetId(id); 
                actor.SetName((string)item["name"]);
                string tmp = (string)item["clothes_list"];
                id_list2 = tmp.Split(',');
                actor.SetClothingList(GetClothing(id_list2));

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
        public model.Clothes GetClothes(string clothes_Id)
        {
            model.Clothes clothes = new model.Clothes();   
            string sql = "SELECT * FROM costume WHERE id = '" + clothes_Id + "'";
            List<Dictionary<string, object>> result = DBTools.Query(sql, conn);

            if (result != null)
            {
                foreach (Dictionary<string, object> res in result)
                {
                    clothes.id = Convert.ToInt32(res["id"]);
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
        public model.Background GetBackground(string bg_Id)
        {
            model.Background background = new model.Background();
            string sql = "SELECT * FROM backgrounds WHERE id = '" + bg_Id + "'";
            List<Dictionary<string, object>> result = DBTools.Query(sql, conn);

            if (result != null)
            {
                foreach (Dictionary<string, object> res in result)
                {
                    background.id = Convert.ToInt32(res["id"]);
                    background.imgPath = Convert.ToString(res["name"]);
                }
            }

            background.x = 0;
            background.y = 0;
            background.rotation = 0;

            return background;
        }

        /*
        static void Main(string[] args)
        {
            List<Dictionary<string, object>> lst = GetFilms();

            Console.WriteLine();
        }
        */
    }
}
