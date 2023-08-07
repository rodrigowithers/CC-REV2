using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System.Text;

public class JsonHelper : MonoBehaviour
{

    private static JsonHelper _instance;
    public static JsonHelper Instance
    {
        get
        {
            if (_instance == null)
            {
                if (FindObjectOfType<JsonHelper>() == null)
                {
                    GameObject go = new GameObject("JsonHelper");
                    go.AddComponent<JsonHelper>();

                    DontDestroyOnLoad(go);

                    _instance = go.GetComponent<JsonHelper>();
                }
                else
                    _instance = FindObjectOfType<JsonHelper>();
            }
            return _instance;
        }
    }



    public void Awake()
    {


    }
    //public void LoadAllLevels()
    //{
    //    TextAsset Text = (TextAsset)Resources.Load("Levels", typeof(TextAsset));
    //    string JsonString = Text.text;
    //    JsonData jsonD = JsonMapper.ToObject(JsonString);
    //    List<Dialogue> ltemp = new List<Dialogue>(); //lista temporaria de dialogos
    //    Dialogue ftemp; 
    //    for (int i = 0; i < jsonD.Count; i++)
    //    {
    //        ftemp = new Dialogue();
    //        ftemp.Level = int.Parse(jsonD[i]["level"].ToString());
    //        ftemp.IsLocked = bool.Parse(jsonD[i]["locked"].ToString());
    //        ftemp.Points = int.Parse(jsonD[i]["points"].ToString());
    //        int s1 = int.Parse(jsonD[i]["star1"].ToString());
    //        int s2 = int.Parse(jsonD[i]["star2"].ToString());
    //        int s3 = int.Parse(jsonD[i]["star3"].ToString());

    //        ftemp.StarPointsAdjust(s1, s2, s3);
    //        ltemp.Add(ftemp);
    //    }

    //}
    //public void SaveAllLevels()
    //{
    //    StringBuilder sb = new StringBuilder();
    //    JsonWriter writer = new JsonWriter(sb);

    //    writer.WriteArrayStart();
    //    foreach (Fase f in GameManager.Instance.LevelList)
    //    {

    //        writer.WriteObjectStart();
    //        writer.WritePropertyName("level");
    //        writer.Write(f.Level);
    //        writer.WritePropertyName("locked");
    //        writer.Write(f.Locked);
    //        writer.WritePropertyName("points");
    //        writer.Write(f.Points);
    //        writer.WritePropertyName("star1");
    //        writer.Write(f.starpoints1);
    //        writer.WritePropertyName("star2");
    //        writer.Write(f.starpoints2);
    //        writer.WritePropertyName("star3");
    //        writer.Write(f.starpoints3);
    //        writer.WriteObjectEnd();

    //    }
    //    writer.WriteArrayEnd();

    //    string s = sb.ToString();
    //    File.WriteAllText(Application.dataPath + "/Resources/Levels.json", s);
    //}

    JsonData Retrieve_Data(string path)
    {
        if (path == "")
            return null;

        TextAsset Text = (TextAsset)Resources.Load(path, typeof(TextAsset));
        string JsonString = Text.text;
        JsonData Jdata = JsonMapper.ToObject(JsonString);
        return Jdata;
    }


    public List<Dialogue> Retrieve_Conversation(string filename)
    {
        string path = "Documents/Conversas/" + filename;
        JsonData JData = Retrieve_Data(path);

        //Debug.Log(JData.Count);

        List<Dialogue> ltemp = new List<Dialogue>(); //lista temporaria de dialogos
        Dialogue ftemp;
        for (int i = 0; i < JData.Count; i++)
        {
            ftemp = new Dialogue();
            ftemp.index = int.Parse(JData[i]["index"].ToString());
            ftemp.person = int.Parse(JData[i]["person"].ToString());
            ftemp.Speed = float.Parse(JData[i]["speed"].ToString());
            ftemp.VocalTone = float.Parse(JData[i]["tone"].ToString());

            List<string> stemp = new List<string>();

            for (int s = 0; s < JData[i]["string"].Count; s++)
            {
                stemp.Add(JData[i]["string"][s]["s"].ToString());
                Debug.Log(stemp[s]);
            }
            ftemp.Text = stemp.ToArray();

            ltemp.Add(ftemp);
        }
        return ltemp;
    }

    public Dialogue Get(string filename, int index)
    {
        string path = "Documents/Conversas/" + filename;
        JsonData JData = Retrieve_Data(path);

        for (int i = 0; i < JData.Count; i++)
        {
            if(int.Parse(JData[i]["index"].ToString()) == index)
            {
                Dialogue ftemp = new Dialogue();
                ftemp.index = int.Parse(JData[i]["index"].ToString());
                ftemp.person = int.Parse(JData[i]["person"].ToString());

                List<string> stemp = new List<string>();
                for (int s = 0; s < JData[i]["string"].Count; s++)
                {
                    stemp.Add(JData[i]["string"][s]["s"].ToString());
                }

                ftemp.Text = stemp.ToArray();
                return ftemp;
            }
        }

        throw new System.Exception("Não encontrou dialogo com o index " + index + " no filename " + filename);
    }
public List<Movement> Retrieve_Movement(string filename)
    {
        string path = "Documents/Animations/" + filename;
        JsonData data = Retrieve_Data(path);
        List<Movement> temp = new List<Movement>();
        Movement mtemp;
        for (int i = 0; i < data.Count; i++)
        {
            mtemp = new Movement();
            mtemp.walk_x = float.Parse(data[i]["x"].ToString());
            mtemp.walk_y = float.Parse(data[i]["y"].ToString());
            mtemp.walk_dir = int.Parse(data[i]["dir"].ToString());
            mtemp.duration = float.Parse(data[i]["time"].ToString());
            temp.Add(mtemp);
        }
        return temp;
    }


}
