using System.Collections;
using System.Collections.Generic;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using BBUnity.Actions;
using UnityEngine;

[Action("Chapter09/ShootOnce")]
[Help("Clone a 'bullet' and shoots it through the Forward axis with the " +
      "specified velocity.")]

public class ShootOnce : GOAction
{
    [InParam("shootPoint")] public Transform shootPoint;
    [InParam("bullet")] public GameObject bullet;
    [InParam("velocity", DefaultValue = 30f)] public float velocity;


    public override void OnStart()
    {
        if (shootPoint == null)
        {
            shootPoint = gameObject.transform.Find("shootPoint");
            if(shootPoint == null)
            {
                Debug.LogWarning("Shoot point not specifiedd. ShootOncewill not wrok for " + gameObject.name);
            }
        }
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        if(shootPoint == null || bullet == null)
        {
            return TaskStatus.FAILED;
        }

        GameObject newBullet = Object.Instantiate(bullet, shootPoint.position, 
            shootPoint.rotation * bullet.transform.rotation);
        if(newBullet.GetComponent<Rigidbody>() == null)
        {
            newBullet.AddComponent<Rigidbody>();
        }
        newBullet.GetComponent<Rigidbody>().velocity = velocity * shootPoint.forward;
        return TaskStatus.COMPLETED;
    }
}
