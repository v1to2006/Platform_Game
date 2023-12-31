using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackCooldownTime;
    [SerializeField] private Vector2 _attackRadius;
    [SerializeField] private LayerMask _playerLayer;

    private void Start()
    {
        StartCoroutine(TryAttackPlayer());
    }

    private IEnumerator TryAttackPlayer()
    {
        WaitForSeconds cooldown = new WaitForSeconds(_attackCooldownTime);

        while (true)
        {
            Collider2D hit = Physics2D.OverlapBox(transform.position, _attackRadius, 0f, _playerLayer);

            if (hit != null)
            {
                if (hit.TryGetComponent<Player>(out Player player))
                {
                    AttackPlayer(player);
                    yield return cooldown;
                }
            }

            yield return null;
        }
    }

    private void AttackPlayer(Player player)
    {
        player.TakeDamage(_attackDamage);
    }
}
