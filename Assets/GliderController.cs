using UnityEngine;
using UnityEngine.UI;

public class GliderController : MonoBehaviour
{
    // Public Airplane Variables
    [HideInInspector] public bool isAlive = true;
    public SO_PlaneSettings planeSettings;
    public SO_GlobalSettings globalSettings;

    // Hidden
    private Rigidbody rigidBody;
    private Text variableText;
    private GameObject mainCamera;
    private PlayerSpawn playerSpawnScript;
    private GameObject launchMenu;
    private RectTransform launchPowerGUI;
    private GameObject speedParticle;
    private float inputDeadZone = 0.01f;
    private static float accelerationMax = 10f;
    public static float acceleration = 0f;
    private float accelerationStrength = 1000f;
    private float verticalTurning = 1f;
    private float rollStrength = 1f;
    private float horizontalTurning = 1f;
    private float launchForce = 0f;
    private float launchForceMIN = 0.1f;
    private float launchForceMAX = 1f;
    private bool showLaunch = true;
    private bool showDebugText = false;

    // Hidden - Collision Avoidance
    private Quaternion start = new Quaternion();
    private Quaternion end = new Quaternion();
    private float duration = 0f;
    
    private void Start()
    {
        // Initialize paramaters when the scene starts
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        variableText = GameObject.FindWithTag("VariableText").GetComponent<Text>();
        mainCamera = GameObject.FindWithTag("MainCamera");

        SetupLaunchGUI();
        SetupSpeedParticles();
    }

    private void Update()
    {
        UpdateSettings();
        if (showDebugText)
            DebugDisplayText();

        
        if (!isAlive) return;
        Launch();
        ToggleSpeedParticles();
    }

    private void FixedUpdate()
    {
        // physics
        if (isAlive && rigidBody.useGravity)
        {
            Accelerate();
            Turn();
            Pitch();
            PitchAcceleration();
            AvoidBuildings();
        }
        else
            acceleration = 0f;
    }

    private void UpdateSettings()
    {
        if (planeSettings == null)
        {
            Debug.LogWarning("Plane has no Settings attached!");
            return;
        }
        showDebugText = planeSettings.showDebugText;
        accelerationStrength = planeSettings.accelerationStrength;
        verticalTurning = planeSettings.verticalTurning;
        rollStrength = planeSettings.rollStrength;
        horizontalTurning = planeSettings.horizontalTurning;
    }

    private void DebugDisplayText()
    {
        // Displays information at runtime
        string text = "";
 
        text += $"\nGame Mode: {globalSettings.GetGameMode()}";
        text += $"\nPlane: {globalSettings.GetPlaneSelection().name}";
        text += $"\nPitch: {transform.forward.y}";
        text += $"\nAltitude: {transform.position.y}";
        text += $"\nSpeed: {rigidBody.velocity.magnitude}";
        text += $"\nAcceleration: {acceleration}";
        
        
        variableText.text = text;
    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "WindForce":
                rigidBody.AddForce(other.transform.up * 5000f);
                acceleration = accelerationMax;
                break;
            case "HWindForce":
                rigidBody.AddForce(other.transform.up * 10000f);
                acceleration = accelerationMax;
                break;
            case "RWindForce":
                rigidBody.AddForce(other.transform.up * 10000f);
                acceleration = accelerationMax;
                break;
            default:
            break;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        // Drop the plane if it collides against a solid object
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Building")
            isAlive = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        switch (col.tag)
        {
            case "Waypoint":
                col.transform.parent.GetComponent<GameMode_Waypoint>().onPlayerCollection(col.gameObject);
                break;
            case "Building":
                col.GetComponent<DiscoverBuilding>().Discover();
                break;
            default:
            break;
        }
    }

    private void Launch()
    {
        // Plane will be in Launch mode while gravity is off.
        if (rigidBody.useGravity) return;

        // Charge Launch bar
        if (Input.GetKey(KeyCode.Space))
        {
            launchForce += 0.01f;
            if (launchForce > launchForceMAX)
                launchForce = launchForceMAX;
        }
        else
        {
            launchForce -= 0.01f;
            if (launchForce < launchForceMIN)
                launchForce = launchForceMIN;
        }
        launchPowerGUI.localScale = new Vector3(launchForce, 1f, 1f);

        // Launch on release
        if (Input.GetKeyUp(KeyCode.Space) && launchForce > 0f)
        {
            // deactive launch gui
            foreach (Transform child in launchMenu.transform)
                child.gameObject.SetActive(false);

            // enable flight
            rigidBody.useGravity = true;
            acceleration = accelerationMax * launchForce;
        }
    }

    private void Accelerate()
    {
        rigidBody.AddForce(transform.forward * acceleration * accelerationStrength);
    }

    private void Turn()
    {
        float input = Input.GetAxis("Horizontal");

        if (Mathf.Abs(input) > inputDeadZone)
        {
            // Turn & Roll via horizontal input
            Vector3 rotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(rotation.x, rotation.y + (horizontalTurning * input), rotation.z + (rollStrength * -input));
        }
        else if (Mathf.Abs(transform.forward.y) <= 1f)
        {
            // Roll Smoothing, levels out the plane when no input is recieved
            Vector3 rotation = transform.rotation.eulerAngles;
            Quaternion smoothRoll = Quaternion.Euler(rotation.x, rotation.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, smoothRoll, 0.02f);
        }
    }

    // rotates the planes facing direction up/down via input
    private void Pitch()
    {
        // Made improvements to the function to prevent gimbal lock.
        float input = Input.GetAxis("Vertical");
        if (Mathf.Abs(input) > inputDeadZone)
            transform.Rotate(Vector3.right, verticalTurning * input);

        // if (Mathf.Abs(input) > inputDeadZone)
        // {
        //     // Change the Pitch via horizontal input
        //     //Vector3 rotation = transform.rotation.eulerAngles;
        //     //transform.rotation = Quaternion.Euler(rotation.x + (verticalTurning * input), rotation.y, rotation.z);
        // }
    }

    private void PitchAcceleration()
    {
        float pitchAngle = transform.forward.y;

        // While pointing down, acceleration increases rappidly
        if (pitchAngle < 0f) { acceleration += 0.05f * -pitchAngle; }

        // While pointing up, acceleration decreases rappidly
        if (pitchAngle > 0f) { acceleration -= 0.025f * pitchAngle; }
            

        // Restrict acceleration between 0 and maximum
        acceleration = Mathf.Clamp(acceleration, 0f, accelerationMax);
    }

    private void AvoidBuildings()
    {
        RaycastHit hit;
        float rayLength = rigidBody.velocity.magnitude *2f;
        Ray colliderRay = new Ray(transform.position, transform.forward);
        
        // Check For Collisions
        if (duration <= 0f)
        {
            if (Physics.Raycast(colliderRay, out hit, rayLength, -1, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.tag == "Building")
                {
                    // Calculate reflection angle and desired location
                    Vector3 reflection = Vector3.Reflect(transform.forward, hit.normal);
                    float distance = Vector3.Distance(transform.position, hit.point);
                    Vector3 target = hit.point + reflection * (distance + 25f);

                    // Flatten the Y axis
                    target = new Vector3(target.x, transform.position.y, target.z);

                    // assign the start & end rotation transforms to be rotated smoothly over times.
                    start = transform.rotation;
                    end = Quaternion.LookRotation(target - transform.position, Vector3.up);
                    duration = 1f;
                }
            }
        }

        // Rotate the Plane
        if (duration > 0f)
        {
            transform.rotation = Quaternion.Slerp(start, end, 1f-duration);
            duration -= Time.deltaTime * 1f;
        }
    }

    public void AttatchSpawnScript(PlayerSpawn script)
    {
        playerSpawnScript = script;
    }

    private void SetupSpeedParticles()
    {
        bool AttachToPlayer = true;

        // ON PLAYER
        if (AttachToPlayer)
        {
            GameObject speedforcePrefab = Resources.Load<GameObject>("ParticleEffects/GAME_PE/PE_SPEEDFORCE");
            speedParticle = Instantiate(speedforcePrefab, transform, false);
            return;
        }

        // ON CAMERA
        if (mainCamera)
        {
            if (mainCamera.transform.childCount == 0)
            {
                GameObject speedforcePrefab = Resources.Load<GameObject>("ParticleEffects/GAME_PE/PE_SPEEDFORCE");
                speedParticle = Instantiate(speedforcePrefab, mainCamera.transform, false);
            }
            else
            {
                speedParticle = mainCamera.transform.GetChild(0).gameObject;
            }
        }
    }

    private void SetupLaunchGUI()
    {
        launchMenu = GameObject.FindWithTag("LaunchScript");
        if (launchMenu && playerSpawnScript)
        {
            // activate launch gui
            foreach (Transform child in launchMenu.transform)
                child.gameObject.SetActive(true);
            
            // apply tweet to building
            SO_TextureIndex tweets = Resources.Load<SO_TextureIndex>("Tweets");
            int spawn_index = playerSpawnScript.GetSpawnIndex();
            if (spawn_index < tweets.textures.Length)
            {
                launchMenu.transform.GetChild(3).GetComponent<RawImage>().texture = tweets.textures[spawn_index];
                launchMenu.transform.GetChild(3).GetComponent<Animator>().Play("tweet_enter",0,0);
            }
            // Setup buttons
            launchMenu.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => playerSpawnScript.NextSpawn() );
            launchMenu.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => playerSpawnScript.PreviousSpawn() );

            // setup launch bar
            launchPowerGUI = launchMenu.transform.GetChild(2).GetComponent<RectTransform>(); // doesnt allways assign??
        }
    }

    private void ToggleSpeedParticles()
    {
        speedParticle.SetActive(acceleration >= accelerationMax * 0.9f);
    }
}
