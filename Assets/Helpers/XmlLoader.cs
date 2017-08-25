using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("ChessChampions")]
public class XmlLoader
{
  
    public static XmlNodeList Parse(string s, string p)
    {
        TextAsset xml = Resources.Load<TextAsset>(s);

        XmlDocument doc = new XmlDocument();
        doc.Load(new StringReader(xml.text));

        XmlNodeList list = doc.SelectNodes(p);

        return list;
    }

    //public static void LoadPieces(string path, List<Class> lc)
    //{
    //    string pathpattern = "//ChessChampions/ChessClasses/Classes/Class";

    //    XmlNodeList list = Parse(path, pathpattern);

    //    Class newclass;

    //    foreach (XmlNode node in list)
    //    {
    //        XmlNode name = node.FirstChild;
    //        XmlNode life = name.NextSibling;
    //        XmlNode speed = life.NextSibling;
    //        XmlNode dodge = speed.NextSibling;
    //        XmlNode dodgerecover = dodge.NextSibling;
    //        XmlNode atktime = dodgerecover.NextSibling;
    //        XmlNode atkrec = atktime.NextSibling;
    //        XmlNode atkexit = atkrec.NextSibling;
    //        XmlNode moverec = atkexit.NextSibling;
    //        XmlNode pattern = moverec.NextSibling;

    //        newclass = new Class();
    //        //Debug.Log(node.FirstChild.InnerXml);
    //        newclass.Name = name.InnerXml;
    //        newclass.Life = int.Parse(life.InnerXml);
    //        newclass.Speed = float.Parse(speed.InnerXml);
    //        newclass.DodgeDist = float.Parse(dodge.InnerXml);
    //        newclass.DodgeRecoverTime = float.Parse(dodgerecover.InnerXml);
    //        newclass.AtkTime = float.Parse(atktime.InnerXml);
    //        newclass.AtkRecoverTime = float.Parse(atkrec.InnerXml);
    //        newclass.AtkExitTime = float.Parse(atkexit.InnerXml);
    //        newclass.MoveRecoverTime = float.Parse(moverec.InnerXml);

    //        lc.Add(newclass);

    //    }
    //}

    //public static void LoadWaves(string path, List<Wave> lw)
    //{
    //    string pathpattern = "//ChessChampions/WaveInfo/Waves/Wave";

    //    XmlNodeList list = Parse(path, pathpattern);

    //    Wave newWave;

    //    foreach (XmlNode node in list)
    //    {
    //        XmlNode level =  node.FirstChild;
    //        XmlNode kings =  level.NextSibling;
    //        XmlNode queen =  kings.NextSibling;
    //        XmlNode tower =  queen.NextSibling;
    //        XmlNode horse =  tower.NextSibling;
    //        XmlNode bishop = horse.NextSibling;
    //        XmlNode pawn =   bishop.NextSibling;

    //        newWave = new Wave();
    //        //Debug.Log(node.FirstChild.InnerXml);
    //        newWave.Level =  int.Parse(level.InnerXml);
    //        newWave.Kings =  int.Parse(kings.InnerXml);
    //        newWave.Queen =  int.Parse(queen.InnerXml);
    //        newWave.Tower =  int.Parse(tower.InnerXml);
    //        newWave.Horse =  int.Parse(horse.InnerXml);
    //        newWave.Bishop = int.Parse(bishop.InnerXml);
    //        newWave.Pawn =   int.Parse(pawn.InnerXml);

    //        lw.Add(newWave);
    //    }
    //}

    public static void LoadBossDialogue(string path, List<string[]> ld)
    {
        string pathpattern = "//ChessChampions/Dialogue/MSG";

        XmlNodeList list = Parse(path, pathpattern);

        XmlNode current;
        XmlNode next;

        int num;
        string[] s;

        foreach (XmlNode node in list)
        {
            current = node.FirstChild;
            num = int.Parse(current.InnerXml);
            s = new string[num];

            for(int i = 0 ; i < num ; i++)
            {
                next = current.NextSibling;
                s[i] = next.InnerXml;
                current = next;
            }
            ld.Add(s);
        }
    }
    
    public static void LoadDialogue(string path, List<string[]> ld)
    {
        string pathpattern = "//ChessChampions/Dialogue";

        XmlNodeList list = Parse(path, pathpattern);

        XmlNode current;
        XmlNode next;

        int num;
        string[] s;

        foreach (XmlNode node in list)
        {
            current = node.FirstChild;
            num = int.Parse(current.InnerXml);
            s = new string[num];

            for (int i = 0; i < num; i++)
            {
                next = current.NextSibling;
                s[i] = next.InnerXml;
                current = next;
            }
            ld.Add(s);
        }
    }
}

