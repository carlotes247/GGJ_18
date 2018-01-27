//---------------------------------------------------------------------------
// Carlos Gonzalez Diaz - TFG - Simulador Virtual Carabina M4 - 2016
// Universidad Rey Juan Carlos - ETSII
//---------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using ReusableMethods;

/// <summary>
/// The Weapon Controller, controls the amount of bullets and how to shoot them
/// </summary>
[AddComponentMenu("CarlosFramework/WeaponController")]
public class WeaponController : MonoBehaviour
{

    #region Fields&Properties
    /// The position of the pointer in screen coordinates [deprecated. Use now cursor from inputController]
    //Vector3 pointerPosition;

    /// The bool to know if the wiimote is pointing to the screen [deprecated. Use now WiimoteOnScreen from WiimoteInput]
    //bool wiimoteOnScreen;

    /// <summary>
    /// (Field) The timer of the weapon, to know when to erase shotPositionShort
    /// </summary>
    [SerializeField]
    private TimerController m_WeaponTimerShotPositionShort;
    /// <summary>
    /// (Property) The timer of the weapon, to know when to erase shotPositionShort
    /// </summary>
    public TimerController WeaponTimerShotPositionShort { get { return this.m_WeaponTimerShotPositionShort; } }

    /// <summary>
    /// (Field) The timer of the weapon, to control the shooting delay
    /// </summary>
    private TimerController m_WeaponTimerShotDelay;
    /// <summary>
    /// (Property) The timer of the weapon, to control the shooting delay
    /// </summary>
    public TimerController WeaponTimerShotDelay { get { return m_WeaponTimerShotDelay; } }    

    /// The bullet to put ingame
    [SerializeField, Header ("Ammo Setup")]
    GameObject bullet;

    /// <summary>
    /// The definition the type of ammo amount
    /// </summary>
    public enum AmmoClipTypeEnum
    {
        Infinite,
        Finite
    }

    /// <summary>
    /// (Field) The current type of ammo amount
    /// </summary>
    [SerializeField]
    private AmmoClipTypeEnum m_AmmoClipType;

    /// <summary>
    /// (Field) Flag to control if the ammo is empty
    /// </summary>
    private bool m_AmmoClipEmpty;

    /// <summary>
    /// (Field) Timer of the ammo reload
    /// </summary>
    private TimerController m_AmmoClipReloadTimer;
    /// <summary>
    /// (Field) Instance of the coroutine
    /// </summary>
    private IEnumerator m_ReloadCoroutine;
    /// <summary>
    /// (Field) Flag that controls if the coroutine is running
    /// </summary>
    private bool m_ReloadCoroutineFlag;

    /// <summary>
    /// (Field) Seconds to reload the ammo clip
    /// </summary>
    [SerializeField, Range (0.1f, 5f)]
    private float m_TimeToReload;

    /// Size of the ammo clip
    [SerializeField]
    int ammoClipSize;

    /// <summary>
    /// (Field) Current amount of bullets
    /// </summary>
    [SerializeField]
    private int m_CurrentAmmo;

    /// The array of bullets to do object pooling
    [SerializeField]
    GameObject[] bulletArray;

    /// <summary>
    /// (Field) The distance from where to shoot bullets
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    float m_DistanceBulletFromObject;

    private Vector3 shotPosition;
    /// <summary>
    /// (Porperty) The position of the last shot performed
    /// </summary>
    public Vector3 ShotPosition { get { return this.shotPosition; } set { this.shotPosition = value; } }

    private Vector3? shotPositionShort;
    /// <summary>
    /// (Porperty) The position of the last shot performed, staying in time for a short time (1 second) 
    /// </summary>
    public Vector3? ShotPositionShort { get { return this.shotPositionShort; } set { this.shotPositionShort = value; } }

    /// <summary>
    /// (Field) The delay between the shots
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    private float m_DelayBetweenShots;

    /// The bool of the button A [deprecated. Use ButtonA from WiimoteInput]
    //bool buttonA;
    /// The state of the button B [deprecated. Use ButtonB from WiimoteInput]
    //bool buttonB; 

    /// <summary>
    /// (Field) The particles of the fire shown when the shooting gets fired
    /// </summary>
    [SerializeField]
    private ParticleSystem m_WeaponParticles;

    /// <summary>
    /// (Field) The Transform component of the Weapon
    /// </summary>
    [SerializeField]
    private Transform m_WeaponTransform;
    /// <summary>
    /// (Property) The Transform component of the Weapon
    /// </summary>
    public Transform WeaponTransform { get { return this.m_WeaponTransform; } }

    /// <summary>
    /// (Field) The AudioSource of the Weapon
    /// </summary>
    [SerializeField]
    private AudioSource m_WeaponAudioSource;

    [Header("Shooting Spread Error")]
    /// <summary>
    /// (Field) The coeficient of the error of the shooting
    /// </summary>
    [SerializeField, Range (0f, 1f)]
    private float m_ShootingErrorCoeficient;
    /// <summary>
    /// (Field) The shooting error 
    /// </summary>
    private Vector3 m_ShootingErrorVector;
    /// <summary>
    /// (Field) The limits of the error 
    /// </summary>
    [SerializeField, Range (0f, 1f)]
    private float m_MaxShootingError;


    #endregion


    // Use this for initialization
    void Start()
    {
        CreateBulletArray();

        // We add the weaponTimerShotDelay TimeController to the GameObject as a component
        m_WeaponTimerShotPositionShort = this.gameObject.AddComponent<TimerController>();
        // We label the timerController, to identify it in the inspector
        m_WeaponTimerShotPositionShort.ObjectLabel = "WeaponTimerShotPositionShort";

        // We add the weaponTimerShotDelay TimeController to the GameObject as a component
        m_WeaponTimerShotDelay = this.gameObject.AddComponent<TimerController>();
        // We label the timerController, to identify it in the inspector
        m_WeaponTimerShotDelay.ObjectLabel = "Weapon Timer Shot Delay";

        // We add the m_AmmoClipReloadTimer TimeController to the GameObject as a component
        m_AmmoClipReloadTimer = this.gameObject.AddComponent<TimerController>();
        // We label the timerController, to identify it in the inspector
        m_AmmoClipReloadTimer.ObjectLabel = "Ammo Clip Reload Timer";                      

        // We assign the transform to WeaponTransform       
        m_WeaponTransform = this.transform;

        // We update the current ammo
        m_CurrentAmmo = ammoClipSize;
        // We update the ammo in the HUD
        //Toolbox.Instance.GameManager.HudController.AmmoText.text = m_CurrentAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        // We erase the shotPositionShort every second
        if (WeaponTimerShotPositionShort.GenericCountDown(1f))
        {
            //ShotPositionShort = Vector3.zero;
            shotPositionShort = null;
        }
    }

    /// <summary>
    /// Shoots from the gun itself
    /// </summary>
    public void ShootFromGun()
    {
        Shoot(this.transform.position, this.transform.forward);
    }
    
    /// <summary>
    /// The function that will be in charge of shooting bullets from the main camera
    /// </summary>
    /// <param name="pointerPos"> The screen pointer position in Screen Coordinates</param>
    /// <param name="shootDir"> The normalized shooting direction</param>
    public void ShootFromCamera(Vector3 pointerPos, Vector3 shootDir)
    {
        // We go through all the array of bullets
        for (int i = 0; i < bulletArray.Length; i++)
        {
            // Because of the object pooling, we only want the objects that are deactivated
            if (!bulletArray[i].activeInHierarchy)
            {
                // If we have finite ammo...
                if (m_AmmoClipType == AmmoClipTypeEnum.Finite)
                {
                    // If the ammo is depleted...
                    if (m_CurrentAmmo <= 0)
                    {
                        // We start the reload coroutine
                        //Toolbox.Instance.GameManager.CoroutineController.StartCoroutineFlag(ReloadAmmo(m_TimeToReload), ref m_ReloadCoroutine, ref m_ReloadCoroutineFlag);

                        // We exit the method
                        return;
                    }
                    // If the ammo is not depleted, we run this code
                    // We remove one unit from the ammo clip
                    m_CurrentAmmo--;
                    // We update the ammo in the HUD
                    //Toolbox.Instance.GameManager.HudController.AmmoText.text = m_CurrentAmmo.ToString();
                }

                // We upate the position of the shot to shoot
                ShotPosition = pointerPos;
                
                // We duplicate the shotPos into a variable that will last only one second. Useful for AIs
                ShotPositionShort = ShotPosition;

                // We put them in the position of our pointer plus an offset to not shoot from the inside of the player
                //bulletArray[i].transform.position = Camera.main.ScreenToWorldPoint(shotPosition);                
                bulletArray[i].transform.position = ((Camera.main.ScreenToWorldPoint(ShotPosition)) + (shootDir * m_DistanceBulletFromObject));
                // We position the WeaponParticles in the same place as the bullet plus an offset to see it from the camera
                m_WeaponParticles.transform.position = bulletArray[i].transform.position + (shootDir * m_DistanceBulletFromObject);

                // If there will be an error applied to the shot...
                if (m_ShootingErrorCoeficient > 0f)
                {
                    // ...We update the shooting error
                    m_ShootingErrorVector.x = Random.Range(-m_MaxShootingError, m_MaxShootingError);                    
                    m_ShootingErrorVector.z = Random.Range(-m_MaxShootingError, m_MaxShootingError);
                    m_ShootingErrorVector.y = Random.Range(-m_MaxShootingError, m_MaxShootingError);
                }

                // An prepare to shoot them with the right direction!
                bulletArray[i].GetComponent<BulletBehaviour>().BulletDirection = shootDir + m_ShootingErrorVector*m_ShootingErrorCoeficient;
                // We activate the object for the bullet to fly free
                bulletArray[i].SetActive(true);

                // We activate the WeaponParticles for one shot
                m_WeaponParticles.Play();
                // We play one shot of the audio
                m_WeaponAudioSource.Play();

                // And then, we break the for loop to only shoot once
                break;

            }
        }
    }

    /// <summary>
    /// The function that will be in charge of shooting bullets
    /// </summary>
    /// <param name="positionToShootFrom"> The position where we want to shoot from </param>
    /// <param name="shootDir"> The direction where to shoot at</param>
    public bool Shoot(Vector3 positionToShootFrom, Vector3 shootDir)
    {
        // We shoot a bullet every m_DelayBetweenShots seconds
        if (m_WeaponTimerShotDelay.GenericCountDown(m_DelayBetweenShots))
        {
            // We go through all the array of bullets
            for (int i = 0; i < bulletArray.Length; i++)
            {
                // Because of the object pooling, we only want the objects that are deactivated
                if (!bulletArray[i].activeInHierarchy)
                {
                    // If we have finite ammo...
                    if (m_AmmoClipType == AmmoClipTypeEnum.Finite)
                    {
                        // If the ammo is depleted...
                        if (m_CurrentAmmo <= 0)
                        {
                            // We start the reload coroutine
                            //Toolbox.Instance.GameManager.CoroutineController.StartCoroutineFlag(ReloadAmmo(m_TimeToReload), ref m_ReloadCoroutine, ref m_ReloadCoroutineFlag);

                            // We exit the method
                            return false;
                        }
                        // If the ammo is not depleted, we run this code
                        // We remove one unit from the ammo clip
                        m_CurrentAmmo--;
                        // We update the ammo in the HUD
                        //Toolbox.Instance.GameManager.HudController.AmmoText.text = m_CurrentAmmo.ToString();
                    }

                    // We upate the position of the shot to shoot
                    ShotPosition = positionToShootFrom;
                    // We place an offset to the shotPosition
                    //shotPosition += (this.gameObject.transform.forward * m_DistanceBulletFromObject);
                    shotPosition += (shootDir * m_DistanceBulletFromObject);
                    // We duplicate the shotPos into a variable that will last only one second. Useful for AIs
                    ShotPositionShort = ShotPosition;
                    // We put them in the position of our pointer
                    //bulletArray[i].transform.position = Camera.main.ScreenToWorldPoint(pointerPosition);                
                    bulletArray[i].transform.position = ShotPosition;
                    // We position the WeaponParticles in the same place as the bullet
                    m_WeaponParticles.transform.position = ShotPosition;

                    // If there will be an error applied to the shot...
                    if (m_ShootingErrorCoeficient > 0f)
                    {
                        // ...We update the shooting error
                        m_ShootingErrorVector.x = Random.Range(-m_MaxShootingError, m_MaxShootingError);
                        m_ShootingErrorVector.z = Random.Range(-m_MaxShootingError, m_MaxShootingError);
                        m_ShootingErrorVector.y = Random.Range(-m_MaxShootingError, m_MaxShootingError);

                    }


                    // An prepare to shoot them with the right direction! We add an error based on the coeficient
                    bulletArray[i].GetComponent<BulletBehaviour>().BulletDirection = shootDir + m_ShootingErrorVector*m_ShootingErrorCoeficient;
                    // We activate the object for the bullet to fly free
                    bulletArray[i].SetActive(true);

                    // We activate the WeaponParticles for one shot
                    m_WeaponParticles.Play();
                    // We play one shot of the audio
                    m_WeaponAudioSource.Play();

                    // And then, we break the for loop to only shoot once
                    // We return true so that we know the shot has been performed                    
                    return true;

                }
            }
        }
        // If the shot was not performed, we return false
        return false;
    }    

    /// <summary>
    /// Reloads the ammo. Is a coroutine
    /// </summary>
    /// <param name="timeToReload"> seconds to reload</param>
    /// <returns></returns>
    private IEnumerator ReloadAmmo (float timeToReload)
    {
        // We wait one frame 
        yield return null;

        while (true)
        {
            // We wait for the time to reload
            if (m_AmmoClipReloadTimer.GenericCountDown(m_TimeToReload))
            {
                // We replete the ammo with the size of the clip
                m_CurrentAmmo = ammoClipSize;
                // We update the ammo in the HUD
                //Toolbox.Instance.GameManager.HudController.AmmoText.text = m_CurrentAmmo.ToString();
                // We update the reload bar progress in the HUD to 0 because the reload ended
                //Toolbox.Instance.GameManager.HudController.UpdateAmmoReloadBar(0f);
                // We make sure that the timer stops for the next time to be called without issues
                m_AmmoClipReloadTimer.StopTimer();

                // Before exiting the coroutine, we set the flag to false so that it can start again
                m_ReloadCoroutineFlag = false;

                // We exit the coroutine
                yield break;
            }
            // if it is still reloading...
            else
            {
                // We update the reload bar progress in the HUD
                //Toolbox.Instance.GameManager.HudController.UpdateAmmoReloadBar(m_AmmoClipReloadTimer.NormalizedTimer);
            }

            // We wait one frame
            yield return null;
        }        

    }

    /// The function will create 
    void CreateBulletArray()
    {
        if (ammoClipSize > 0)
        {
            this.bulletArray = new GameObject[ammoClipSize];
            for (int i = 0; i < ammoClipSize; i++)
            {
                bulletArray[i] = (GameObject)Instantiate(bullet);
                bulletArray[i].SetActive(false);
                bulletArray[i].transform.parent = this.transform;
            }
        }
    }
   
}
