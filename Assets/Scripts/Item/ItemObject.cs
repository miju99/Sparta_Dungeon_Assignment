using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractable//어떤 아이템인지 판단하고, 아이템마다 작용하는 class를 다 만들어줄 수 없기 때문에
{
    public string GetInteractPrompt(); //화면에 띄워줄 프롬프트
    public void Oninteract(); //어떤 효과를 발생시키게 할 건지
    
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data; //아이템들의 정보를 담음
    
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}"; //아이템 이름과 설명란
        return str;
    }

    public void Oninteract()
    {
        CharacterManager.Instance.Player.itemData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();

        Destroy(gameObject);
    }
}
