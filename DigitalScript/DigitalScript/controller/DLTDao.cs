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



        public Script GetScript(string id)
        {
            Script result = new Script();
            string sql = "SELECT * FROM films WHERE id = " + id;
            List<Dictionary<string, object>> lst = DBTools.Query(sql, conn);
            foreach (Dictionary<string, object> item in lst)
            {
                result.id = (int)item["id"];
                result.title = (string)item["name"];
            }
            sql = "SELECT * FROM class_member";  //所以actor的id是要另外生一個，還是從資料表中抓id????
            lst = DBTools.Query(sql, conn);
            foreach (Dictionary<string, object> item in lst)
            {
                //Actor actor = new Actor();

                //actor = item["classMember_id"]; 
            }
            sql = "SELECT scene_id FROM film_scenesorder WHERE film_id = " + id;
            lst = DBTools.Query(sql, conn);
            foreach (Dictionary<string, object> item in lst)
            {

                Scene scene = new Scene();
                scene.id = (int)item["scene_id"];

            }

            return result;
        }

        public Scene GetScene(string id)
        {
            Scene result = new Scene();
            //...
            return result;
        }

        public Actor GetActor(Guid id)
        {
            Actor result = new Actor();
            //...
            return result;
        }

        /*static void Main(string[] args)
        {
            List<Dictionary<string, object>> lst = GetFilms();

            Console.WriteLine();
        }*/
    }
}
