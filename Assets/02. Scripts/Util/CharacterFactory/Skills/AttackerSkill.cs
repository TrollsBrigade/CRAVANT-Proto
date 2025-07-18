using UnityEngine;

/// <summary>
/// 투사체 발사형 스킬
/// </summary>
public class AttackerSkill : ITargetableSkill
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

    public void Activate(CharacterBase character, Vector3 target)
    {
        if (_timer < Data.Cooltime)
        {
            Debug.Log($"{character.Name} Skill is on cooldown.");
            return;
        }

        Vector3 origin = character.Behaviour.transform.position;
        target.y = origin.y; // Y축 고정하여 2D 발사 느낌을 주기 위함
        Vector3 dir = (target - origin).normalized;

        // 방향을 바라보도록 회전 (3D 기준으로 z축 전방)
        Quaternion rotation = Quaternion.LookRotation(dir);

        GameObject prefab = Resources.Load<GameObject>("Projectiles/" + Data.ProjectilePrefabName);

        if (prefab == null)
        {
            Debug.LogError($"프리팹 {Data.ProjectilePrefabName} 을(를) Resources/Projectiles 에서 찾을 수 없습니다.");
            return;
        }

        GameObject projectile = GameObject.Instantiate(prefab, origin, rotation);
        projectile.GetComponent<IProjectile>().SetData(Data, character, dir);

        _timer = 0f;
    }
}