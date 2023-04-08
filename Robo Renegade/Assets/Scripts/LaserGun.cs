using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LaserGun : MonoBehaviour
{

    [SerializeField] private GameObject laserBeam;
    [SerializeField] private int cooldown;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private int direction;
    private float laserTimer;
    private bool timeStopValid = true;
    private bool isEvolved = false;
    [SerializeField] TextMeshProUGUI cooldownText;

    void Start()
    {
    }

    void Update()
    {
        float cooldownTimer = 0;
        if (Input.GetKeyUp(KeyCode.E) && Level.getActive2Level() >= 1)
        // if (Input.GetKeyUp(KeyCode.E))
        {
            cooldown = 11 - Level.getActive2Level();
            // cooldown = 2;
            isEvolved = Level.getActive2Evolved();
            if (timeStopValid && cooldown != 11)
            {
                cooldown = Mathf.Max(5, cooldown);
                Debug.Log("evolved: " + isEvolved); 
                cooldownTimer = cooldown;
                // Debug.Log("Laser beam shot");
                ShootLaser();
                StartCoroutine(StopTimeCooldown(cooldown));
            }
        }
        if (cooldownTimer > 0 && Level.getActive2Level() >= 1)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownText.text = "E: " + (int) cooldownTimer;
        } else if (cooldownTimer <= 0 && Level.getActive2Level() >= 1) {
            cooldownText.text = "E: READY";
        }
    }

    private IEnumerator StopTimeCooldown(int cooldown)
    {
        timeStopValid = false;
        yield return new WaitForSeconds(cooldown);
        timeStopValid = true;
    }

    private IEnumerator ShootSecondLaser(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(laserBeam, firingPoint.position, Quaternion.Euler(0, 0, direction-45f));
        FindObjectOfType<AudioManager>().Play("blaster_14");
    }

    void ShootLaser()
    {
        Instantiate(laserBeam, firingPoint.position, Quaternion.Euler(0, 0, direction-90f));
        FindObjectOfType<AudioManager>().Play("blaster_14");

        if (isEvolved == true) {
            StartCoroutine(ShootSecondLaser(0f));
        }
    }


    // [SerializeField] private float defDistance = 100f;
    // public Transform laserFirePoint;
    // public LineRenderer laserLine;
    // Transform mTransform;

    // private void Start()
    // {
    //     mTransform = GetComponent<Transform>();
    // }    
    
    // // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyUp(KeyCode.E))
    //     {
    //         Debug.Log("E pressed");
    //         // endpoint.position.y = beamPointUp.position.y + 5f;
    //         // SetUpLine(new Transform[] { beamPointUp, beamPointEnd });
    //         ShootLaser();
    //     }
    // }

    // void ShootLaser()
    // {
    //     if (Physics2D.Raycast(laserFirePoint.position, transform.right))
    //     {
    //         RaycastHit2D hitInfo = Physics2D.Raycast(laserFirePoint.position, transform.right);
    //         laserLine.SetPosition(0, laserFirePoint.position);
    //         laserLine.SetPosition(1, hitInfo.point);
    //     }
    //     else
    //     {
    //         laserLine.SetPosition(0, laserFirePoint.position);
    //         laserLine.SetPosition(1, laserFirePoint.transform.right * defDistance);
    //     }
    // }


    // private LineRenderer lr;
    // private int lengthOflr = 2;
    // private Color c1 = Color.yellow;
    // private Color c2 = Color.red;
    // private Transform[] points;
    // [SerializeField] private Transform beamPointUp;
    // [SerializeField] private Transform beamPointEnd;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     lr = GetComponent<LineRenderer>();
    //     lr.widthMultiplier = 0.2f;
	// 	lr.positionCount = lengthOflr;

	// 	// A simple 2 color gradient with a fixed alpha of 1.0f.
	// 	float alpha = 1.0f;
	// 	Gradient gradient = new Gradient();
	// 	gradient.SetKeys(
	// 		new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
	// 		new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
	// 		);
	// 	lr.colorGradient = gradient;
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyUp(KeyCode.E))
    //     {
    //         Debug.Log("E pressed");
    //         // endpoint.position.y = beamPointUp.position.y + 5f;
    //         SetUpLine(new Transform[] { beamPointUp, beamPointEnd });
    //     }
    // }

    // public void SetUpLine(Transform[] points)
    // {
    //     this.points = points;
    //     lr.positionCount = points.Length;
    //     for (int i = 0; i < points.Length; i++)
    //     {
    //         lr.SetPosition(i, points[i].position);
    //     }
    // }
}
