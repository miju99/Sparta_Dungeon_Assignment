using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;

    public static CharacterManager Instance //�ܺο����� Instance�� �����ؼ� _instance�� ��ȯ
    {
        get
        {
            if(_instance == null) //��� �ִٸ� ���� ������ش�.
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

    private void Awake()// Awake ���� �� �̹� ���ӿ�����Ʈ�� Scripts�� �پ��ִ� ���·� ������ ��
    {
        if( _instance == null)
        {
            _instance = this; //GameObject�� ���� �ʿ�� ����, �� �ڽ��� �־��ֱ⸸ �ϸ� ���!
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
