//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MessageManager : MonoBehaviour {

//    public static MessageManager _instance = null;

//    private string _document_path = "Documents/TowerBossDialogue";
//    private string _document_path_2 = "Documents/TowerBossDialogue2";

//    bool foiaaprimeira = false;

//    List<string[]> _towerbossDialogues = new List<string[]>();
//    List<string[]> _towerbossDialogues2 = new List<string[]>();

//    private int vez = 0 ;
//    GameObject MessageBox;
//    GameObject BossMessageBox;

//    GameObject player_ref;
//    GameObject Instantiated_MB;
//    bool messagewait = false;



//    bool firsttimedialogue = true;
//    int dialogue = 0;

//    void Awake()
//    {
//        if (_instance == null)
//            _instance = this;
//        else if (_instance != this)
//            Destroy(this.gameObject);
//        DontDestroyOnLoad(this.gameObject);
//    }



//    // Use this for initialization
//    void Start () {
//        MessageBox = Resources.Load<GameObject>("Prefabs/HUD/MessageBox");
//        BossMessageBox = Resources.Load<GameObject>("Prefabs/HUD/BossMessageBox");
//        XmlLoader.LoadDialogue(_document_path,_towerbossDialogues);
//        XmlLoader.LoadDialogue(_document_path_2, _towerbossDialogues2);
//    }

//    // Update is called once per frame
//    void Update () {
//        if (messagewait)
//        {
//            if (Instantiated_MB != null)
//                Time.timeScale = 0.0f;
//            else
//                Time.timeScale = 1;

//            if (Input.GetKeyDown(KeyCode.M))
//            {
//                string[] m = new string[1];
//                m[0] = "está funcionando";
//                CreateMessage(m);
//            }
//        }
        
//    }

//    public void CreateMessage(string[] messages)
//    {
//        messagewait = true;
//        GameObject c = GameObject.FindGameObjectWithTag("Canvas");
//        Instantiated_MB = Instantiate(MessageBox, c.transform);
        
//        Instantiated_MB.transform.parent = c.transform;
//        Instantiated_MB.transform.localScale = new Vector3(1,1,1);
//       // Instantiated_MB.transform.localPosition = new Vector3(0, 0, 0);
//        Instantiated_MB.GetComponent<MessageBoxScript>().CreatingMessages(new Vector2(0,-1),messages);
//    }


//    public void CreateBooya()
//    {
//        string[] teste = new string[1];
//        teste[0] = "BOOOOYA";

//        CreateMessage(teste);
//    }
    
    
//    public GameObject ReturnMessageBox(string[] messages)
//    {
//        GameObject m  = new GameObject();
//        GameObject c = GameObject.FindGameObjectWithTag("Canvas");
//        m = Instantiate(MessageBox, c.transform);

//        m.transform.parent = c.transform;
//        m.transform.localScale = new Vector3(1, 1, 1);
//        // Instantiated_MB.transform.localPosition = new Vector3(0, 0, 0);
//        m.GetComponent<MessageBoxScript>().CreatingMessages(new Vector2(0, -1), messages);
//        return m;
//    }
//    public GameObject ReturnMessageBoxofSize(string[] messages, int fs, Vector3 scale)
//    {
//        GameObject m = new GameObject();
//        GameObject c = GameObject.FindGameObjectWithTag("Canvas");
//        m = Instantiate(MessageBox, c.transform);

//        m.transform.parent = c.transform;
//        m.transform.localScale = scale;
//        m.GetComponent<MessageBoxScript>().ChangeFontSize(fs);
//        // Instantiated_MB.transform.localPosition = new Vector3(0, 0, 0);
//        m.GetComponent<MessageBoxScript>().CreatingMessages(new Vector2(0, -1),messages);
//        return m;
//    }



//    public GameObject ReturnBossMsgBox(string[] messages,int piecetype)
//    {
//        GameObject m = new GameObject();
//        GameObject c = GameObject.FindGameObjectWithTag("Canvas");
//        m = Instantiate(BossMessageBox, c.transform);

//        m.transform.parent = c.transform;
//        m.transform.localScale = new Vector3(3,0.5f,0);
//        //m.GetComponent<MessageBoxScript>().ChangeFontSize(fs);
//        // Instantiated_MB.transform.localPosition = new Vector3(0, 0, 0);
//        m.GetComponent<BossDialogue>().CreatingMessages(new Vector2(0, -1), messages);
//        m.GetComponent<BossDialogue>().ChangeBossImage(piecetype);

//        return m;
//    }

//    public GameObject ReturnBossMsgBox()
//    {
//        GameObject m = new GameObject();
//        GameObject c = GameObject.FindGameObjectWithTag("Canvas");
//        m = Instantiate(BossMessageBox, c.transform);

//        m.transform.parent = c.transform;
//        m.transform.localScale = new Vector3(3, 0.5f, 0);
//        //m.GetComponent<MessageBoxScript>().ChangeFontSize(fs);
//        // Instantiated_MB.transform.localPosition = new Vector3(0, 0, 0);

//        string[] messages = choosedialogue();
//        m.GetComponent<BossDialogue>().CreatingMessages(new Vector2(0, -1), messages);
//        m.GetComponent<BossDialogue>().ChangeBossImage(2);

//        return m;
//    }


//    string[] choosedialogue()
//    {
//        if (firsttimedialogue)
//        {
//            dialogue = 0;
//            firsttimedialogue = false;
//        }
//        else
//            dialogue++;


//        return returndialogue(vez,dialogue) ;
//    }

//    string[] returndialogue(int d,int dd)
//    {
//        if (d == 1 && foiaaprimeira)
//        {
//            dd = 0;
//            foiaaprimeira = true;
//        }

//        if(d == 0)
//        {
//            return _towerbossDialogues[dd];
//        }

//        if(dd > _towerbossDialogues2.Count)
//        {
//            return _towerbossDialogues[0];
//        }
//        return _towerbossDialogues2[dd];
//    }



//    public void DestroyMessage()
//    {
//        Destroy(Instantiated_MB.gameObject);
//    }


//    public void mudavez()
//    {
//        vez++;
//    }
//}
