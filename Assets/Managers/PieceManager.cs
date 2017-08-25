using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using UnityEngine.UI;

public enum CHESSPIECE
{
    NONE = -1,
    KING = 0,
    QUEEN = 1,
    TOWER = 2,
    HORSE = 3,
    BISHOP = 4,
    PAWN = 5
}

public enum LEVEL
{
    NPC = -1,
    LESSER = 0,
    NORMAL = 1,
    GREATER = 2,
}

public class PieceManager : MonoBehaviour
{

    private static PieceManager _instance = null;
    public static PieceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                if (FindObjectOfType<PieceManager>() == null)
                {
                    GameObject go = new GameObject("PieceManager");
                    go.AddComponent<PieceManager>();

                    DontDestroyOnLoad(go);

                    _instance = go.GetComponent<PieceManager>();
                }
                else
                {
                    _instance = FindObjectOfType<PieceManager>();
                }
            }

            return _instance;
        }
    }

    string _pieces_path = "Documents/PieceInfo_Fast";

    [XmlArray("Classes")]
    [XmlArrayItem("Class")]
    private List<Class> _classes = new List<Class>();
    private List<GameObject> _atkPatterns = new List<GameObject>();

   // private List<CHESSPIECE> _possiblePlayerClasses; // Classes que o player pode 
    //public Transform LivesParent;                    // Parent para o mostrador das classes possíveis
    public GameObject Piece;                         // Prefab da peça a ser instanciada

    //public void RemovePossibleClass(CHESSPIECE toRemove)
    //{
    //    // Checa se existe
    //    if (_possiblePlayerClasses.IndexOf(toRemove) >= LivesParent.childCount)
    //        return;

    //    // Remove do mostrador
    //    var sprite = LivesParent.GetChild(_possiblePlayerClasses.IndexOf(toRemove));

    //    Destroy(sprite.gameObject);

    //    _possiblePlayerClasses.Remove(toRemove);
    //}

    //public void AddPossibleClass()
    //{
    //    if (_possiblePlayerClasses.Count >= 6)
    //        return;

    //    if (!_possiblePlayerClasses.Contains(CHESSPIECE.KING))
    //    {
    //        _possiblePlayerClasses.Add(CHESSPIECE.KING);
    //        return;
    //    }
    //    if (!_possiblePlayerClasses.Contains(CHESSPIECE.QUEEN))
    //    {
    //        _possiblePlayerClasses.Add(CHESSPIECE.QUEEN);
    //        return;
    //    }
    //    if (!_possiblePlayerClasses.Contains(CHESSPIECE.BISHOP))
    //    {
    //        _possiblePlayerClasses.Add(CHESSPIECE.BISHOP);
    //        return;
    //    }
    //    if (!_possiblePlayerClasses.Contains(CHESSPIECE.HORSE))
    //    {
    //        _possiblePlayerClasses.Add(CHESSPIECE.HORSE);
    //        return;
    //    }
    //    if (!_possiblePlayerClasses.Contains(CHESSPIECE.TOWER))
    //    {
    //        _possiblePlayerClasses.Add(CHESSPIECE.TOWER);
    //        return;
    //    }
    //    if (!_possiblePlayerClasses.Contains(CHESSPIECE.PAWN))
    //    {
    //        _possiblePlayerClasses.Add(CHESSPIECE.PAWN);
    //        return;
    //    }

    //}

    //public bool IsClassPossible(CHESSPIECE cp)
    //{
    //    if (_possiblePlayerClasses.Contains(cp))
    //        return true;
    //    return false;
    //}


    //public bool HasPossibleClasses()
    //{
    //    if (_possiblePlayerClasses.Count > 0)
    //        return true;
    //    else
    //        return false;
    //}

    //private void UpdatePossibleClasses()
    //{

    //}

    void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //}
        //else if (Instance != this)
        //{
        //    Destroy(gameObject);
        //}
        //DontDestroyOnLoad(gameObject);

       // XmlLoader.LoadPieces(_pieces_path, _classes);
        //FillAtkPattern();

    }

    void Start()
    {
        //Loader pc = Loader.Load(_path);
        //XmlLoader.LoadPieces(_pieces_path, _classes);

        //_possiblePlayerClasses = new List<CHESSPIECE>
        //{
        //    CHESSPIECE.KING,
        //    CHESSPIECE.QUEEN,
        //    CHESSPIECE.TOWER,
        //    CHESSPIECE.HORSE,
        //    CHESSPIECE.BISHOP,
        //    CHESSPIECE.PAWN
        //};

        //FillAtkPattern();
        //SpawnLivesHUD();
    }

    /// <summary>
    /// Faz com que um LESSER enemy se torne um NORMAL enemy
    /// e um NORMAL enemy vire um GREATER enemy
    /// </summary>
    /// <param name="e"></param>
    public void PromoveEnemy(Enemy e)
    {
        if (e.Level >= LEVEL.GREATER)
            return;
        //Destroy(e.GetComponent<Class>());
        e._Level++;

        e._StateMachine.ChangeState(new EvolveState());
    }

    public void ChangeClass(BattlePiece p, System.Type newType)
    {
        if (p.transform.childCount != 0)
        {
            Destroy(p.transform.GetChild(0).gameObject);

            Destroy(p.GetComponent<Class>());
        }
        p.GetClass = (Class)p.gameObject.AddComponent(newType);
       // p.GetClass.Start();
    }


    public void DecideRandomClass(BattlePiece p)
    {
        switch(Random.Range(0, 5))
        {
            case 0:
                ChangeClass(p,typeof(King_Normal));
                break;
            case 1:
                ChangeClass(p, typeof(Queen_Normal));
                break;
            case 2:
                ChangeClass(p, typeof(Tower_Normal));
                break;
            case 3:
                ChangeClass(p, typeof(Horse_Normal));
                break;
            case 4:
                ChangeClass(p, typeof(Bishop_Normal));
                break;
            case 5:
                ChangeClass(p, typeof(Pawn_Normal));
                break;

        }
    }




    //public void SpawnLivesHUD()
    //{
    //    GameObject temp = Tag.FindUsingTag("UILives");
    //    if (temp != null)
    //        LivesParent = temp.transform;

    //    Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Generic/peças");
    //    Piece = Resources.Load<GameObject>("Prefabs/PieceHUD");

    //    // Instancia uma sprite com a mesma sprite da _possiblePlayerClass atual
    //    foreach (CHESSPIECE piece in _possiblePlayerClasses)
    //    {
    //        // Cria uma nova Sprite
    //        var sprite = Instantiate(Piece, LivesParent);

    //        // Atualiza sprite para a da piece
    //        if (piece == CHESSPIECE.BISHOP)
    //        {
    //            sprite.GetComponent<Image>().sprite = sprites[0];
    //            sprite.name = "bishop";
    //        }
    //        if (piece == CHESSPIECE.HORSE)
    //        {
    //            sprite.GetComponent<Image>().sprite = sprites[2];
    //            sprite.name = "horse";
    //        }
    //        if (piece == CHESSPIECE.KING)
    //        {
    //            sprite.GetComponent<Image>().sprite = sprites[4];
    //            sprite.name = "king";
    //        }
    //        if (piece == CHESSPIECE.PAWN)
    //        {
    //            sprite.GetComponent<Image>().sprite = sprites[6];
    //            sprite.name = "pawn";
    //        }
    //        if (piece == CHESSPIECE.QUEEN)
    //        {
    //            sprite.GetComponent<Image>().sprite = sprites[8];
    //            sprite.name = "queen";
    //        }
    //        if (piece == CHESSPIECE.TOWER)
    //        {
    //            sprite.GetComponent<Image>().sprite = sprites[10];
    //            sprite.name = "tower";
    //        }

    //        sprite.transform.localScale = Vector3.one;
    //    }
    //}

    //private void Update()
    //{
    //    UpdatePossibleClasses();
    //}

    //void FillAtkPattern()
    //{
    //    _atkPatterns = new List<GameObject>();

    //    string path = "Prefabs/Attack Patterns/";

    //    for (int i = 0; i < 6; i++)
    //    {
    //        _atkPatterns.Add(Resources.Load<GameObject>(path + _classes[i].Name + "AttackPattern"));
    //    }
    //}

    //int SelectRandomClass(int actual)
    //{
    //    int cp;
    //    do
    //    {
    //        cp = Random.Range(0, 6);
    //    } while (cp == actual);
    //    return cp;
    //}

    //CHESSPIECE SelectRandomPlayerClass(CHESSPIECE actual)
    //{
    //    CHESSPIECE cp;
    //    do
    //    {
    //        int i = Random.Range(0, _possiblePlayerClasses.Count);
    //        cp = _possiblePlayerClasses[i];

    //    } while (cp == actual);
    //    return cp;
    //}

    //public void AdjustToClass(GameObject go, bool isEnemy = true, int index = -1)
    //{
    //    Piece piece = go.GetComponent<Piece>();

    //    if (go.tag == "Player")
    //    {
    //        isEnemy = false;
    //    }
    //    else
    //        isEnemy = true;

    //    if (index == -1)
    //    {
    //        CHESSPIECE newClass = SelectRandomPlayerClass(piece.Type);
    //        if (isEnemy)
    //            ChangeEnemy(piece, (int)newClass);
    //        else
    //            ChangePiece(piece, (int)newClass);
    //    }
    //    else
    //    {
    //        if (isEnemy)
    //            ChangeEnemy(piece, index);
    //        else
    //            ChangePiece(piece, index);
    //    }
    //}

    //public void ChangePiece(Piece p, int cp)
    //{
    //    //Debug.Log("FUNCIONANDO");

    //    // Se _atkPattern for null, refaz
    //    if (_atkPatterns == null)
    //        FillAtkPattern();

    //    p.Type = (CHESSPIECE)cp;
    //    p.Speed = _classes[cp].Speed;
    //    p.DodgeDist = _classes[cp].DodgeDist;
    //    p.ChangePattern(_atkPatterns[cp]);
    //    p.ChangeSprite(_classes[cp].Name);

    //    p.AtkRecoverTime = _classes[cp].AtkRecoverTime;

    //}

    //public void ChangeEnemy(Piece p, int cp)
    //{
    //    ChangePiece(p, cp);
    //    Enemy e = (Enemy)p;
    //    p.Life = _classes[cp].Life;
    //    //e.normalAtkExitTime = _classes[cp].AtkExitTime;
    //    //e.moveRecoverTime = _classes[cp].MoveRecoverTime;
    //    //e.dodgeRecoverTime = _classes[cp].DodgeRecoverTime;
    //    //e.atkTime = _classes[cp].AtkTime;
    //    //e.ChangePatternColor();

    //}

    //public GameObject GetPattern(int i)
    //{
    //    return _atkPatterns[i];
    //}

    //public Sprite GetSprite(int i)
    //{
    //    return GameManager._instance.GetPieceSpriteWithName(_classes[i].Name);
    //}

}
