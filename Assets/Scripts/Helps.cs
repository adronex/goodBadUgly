using UnityEngine;

class Helps
{
    public const int AddedForce = 400;
    public const int AddedTorque = 1000;
    //Game
    public readonly static Quaternion OwnHeroStartRotation = Quaternion.identity;
    public readonly static Quaternion EnemyHeroStartRotation = Quaternion.Euler(0, 180f, 0);
    public static Vector2 OwnHeroStartPosition = new Vector3(-8f, -0.34f);
    public static Vector2 EnemyHeroStartPosition = new Vector3(4f, -0.34f);
    public readonly static Quaternion BulletStartRotation = Quaternion.Euler(0, 0, -90f);
    public const int MinHp = 0;
    public const int MinAmmo = 0;
    public const string OwnHeroName = "OwnHero";
    public const string EnemyHeroName = "EnemyHero";
    public const float ReloadTime = 0.05f;
    public const int HeroLayer = 1 << 8;
    public const int BodyPartIsNull = -1;
    public const float ONE_PERCENT = 0.01f;
    
    //Game prompts
    public const float HidingStartRoundPromptsStep = 0.01f;
    public const float AreasPromptsDefaultAlpha = 0.390625f;
    public const float CountdownTime = 1f;

    //Blood
    public const float BloodCreateRadius = 0.3f;
    public const int BloodLimit = 2;
    public const string BloodStorageName = "BloodStorage";
    public const int BloodSortingLayer = 100;
    public const string BloodTexture = "_MainTex";

    //Blood holes
    public const int BloodHoleSortingLayerOffset = -1;
    public const int BloodHoleLimit = 50;
    public const string BloodHoleStorageName = "BloodHolesStorage";
    public const float MinBloodHoleOffset = 1.3f;
    public const float MaxBloodHoleOffset = 1.7f;

    //Bullets
    public const int BulletLimit = 4;
    public const string BulletStorageName = "BulletStorage";
    public const float MaxWorldPosition = 20f;

    //Data paths
    public const string CurrentOwnHeroData = "CurrentOwnHero";
    public const string HeroesPath = "Prefabs/Heroes/";

    //Game animations
    public const string BodyPartAnim = "BodyPart";
    public const string HitAnim = "Hit";
    public const string FalseStartAnim = "FalseStart";
    public const string IdleAnim = "Idle";

    //Menu animations
    public const string TavernAnimatorPath = "Prefabs/Tavern";
    public const string HeroTypeIdTavernAnim = "HeroTypeId";

    //Menu
    public const string HeroSelected = "PICKED";
    public const string HeroUnselected = "PICK";

    //Scenes
    public const string BattleScene = "Battle";
    public const string MenuScene = "Menu";
}
