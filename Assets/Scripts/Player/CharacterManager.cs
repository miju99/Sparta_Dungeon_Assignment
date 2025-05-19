using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;

    public static CharacterManager Instance //외부에서는 Instance로 접근해서 _instance를 반환
    {
        get
        {
            if(_instance == null) //비어 있다면 새로 만들어준다.
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return _instance;
        }
    }

    public Player _player;

    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private void Awake()// Awake 실행 시 이미 게임오브젝트로 Scripts에 붙어있는 상태로 실행이 됨
    {
        if( _instance == null)
        {
            _instance = this; //GameObject를 만들 필요는 없고, 나 자신을 넣어주기만 하면 충분!
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if( _instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
