using UnityEngine;

public class TankerPassive : IEventReactiveSkill
{
    private float _timer = 0f;

    public SkillData Data { get; set; }

    public void Update()
    {
        _timer += Time.deltaTime;
    }

    public GameObject GetIndicatorPrefab()
    {
        return Resources.Load<GameObject>($"Indicators/{Data.IndicatorPrefabName}");
    }

    public void Activate(CharacterBase character)
    {
        if (_timer < Data.Cooltime)
        {
            Debug.Log($"{character.Name} TankerPassive is on cooldown.");
            return;
        }
        Debug.Log($"{character.Name} activated TankerPassive.");

        _timer = 0f;
    }

    public void OnEvent(ECharacterEvent evt)
    {
        if (evt != ECharacterEvent.OnBasicAttack)
        {
            return;
        }

        Debug.Log($"패시브 추뎀 발동 : {Data.Damage}");
    }
}
