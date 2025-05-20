using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractable//� ���������� �Ǵ��ϰ�, �����۸��� �ۿ��ϴ� class�� �� ������� �� ���� ������
{
    public string GetInteractPrompt(); //ȭ�鿡 ����� ������Ʈ
    public void Oninteract(); //� ȿ���� �߻���Ű�� �� ����
    
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data; //�����۵��� ������ ����
    
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}"; //������ �̸��� �����
        return str;
    }

    public void Oninteract()
    {
        CharacterManager.Instance.Player.itemData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();

        Destroy(gameObject);
    }
}
