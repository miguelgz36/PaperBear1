using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Misc;


public class Dron : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] List<GameObject> granades;
    [SerializeField] private float maxDispersion = 2;
    [SerializeField] float throwingDelay = 1f;
    [SerializeField] float returningDelay = 1f;

    bool deployed = false;
    bool returning = false;
    private Vector3 target;
    protected Rigidbody2D rigidBody;
    private float proximityThreshold = 0.3f;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (deployed || returning)
        {
            if (Vector3.Distance(transform.position, target) < proximityThreshold)
            {
                if (returning)
                {
                    Destroy(gameObject);
                }
                if(deployed)
                {
                    deployed = false;
                    StartCoroutine(ThrowProjectiles());
                }
            }
            else
            {
                rigidBody.MovePosition(speed * Time.fixedDeltaTime * transform.up + transform.position);
            };
        }
    }

    public void Deploy(Vector3 target)
    {
        this.target = target;
        DefineRotation();
        this.deployed = true;
    }

    private void DefineRotation()
    {
        float angle = RotationUtils.CalculateRotationToAimObject(gameObject.transform.position, target);
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private IEnumerator ThrowProjectiles()
    {
        foreach (GameObject granadePrefab in granades)
        {
            yield return new WaitForSeconds(throwingDelay);
            float x = Random.Range(gameObject.transform.position.x - maxDispersion, gameObject.transform.position.x + maxDispersion);
            float y = Random.Range(gameObject.transform.position.y - maxDispersion, gameObject.transform.position.y + maxDispersion);
            GameObject granadeInstance = Instantiate(granadePrefab, gameObject.transform.position, Quaternion.identity);
            granadeInstance.GetComponent<DronGranade>().FireShell(new Vector3(x, y));
        }

        StartCoroutine(ReturnToSpawn());
    }

    private IEnumerator ReturnToSpawn()
    {
        yield return new WaitForSeconds(returningDelay);
        target = SupportFireManager.Instance.PositionAlliedSupportingFire.transform.position;
        DefineRotation();
        returning = true;
    }

}
