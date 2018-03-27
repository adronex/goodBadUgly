using UnityEngine;
using UnityEngine.UI;
public class HeroesManager : MonoBehaviour
{
    [Header("Own hero")]
    [SerializeField]
    private Text ownAmmoText;
    [SerializeField] private Image ownHpImage;
    [SerializeField] private Transform ownHandAxis;
    [Header("Enemy hero")]
    [SerializeField]
    private Text enemyAmmoText;
    [SerializeField] private Image enemyHpImage;
    [SerializeField] private Transform enemyHandAxis;


    public Text OwnAmmoText
    {
        get { return ownAmmoText; }
    }

    public Image OwnHpImage
    {
        get { return ownHpImage; }
    }

    public Transform OwnHandAxis
    {
        get { return ownHandAxis; }
    }

    public Text EnemyAmmoText
    {
        get { return enemyAmmoText; }
    }

    public Image EnemyHpImage
    {
        get { return enemyHpImage; }
    }

    public Transform EnemyHandAxis
    {
        get { return enemyHandAxis; }
    }
}