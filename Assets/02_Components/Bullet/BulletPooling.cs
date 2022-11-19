using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    internal static BulletPooling instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform gunTransform;

    private List<Bullet> deactivatedBullets = new List<Bullet>();

    public void DeactivateBullet(Bullet bulletToDeactivate)
    {
        bulletToDeactivate.gameObject.SetActive(false);
        deactivatedBullets.Add(bulletToDeactivate);
    }

    private Bullet ActivateBullet(Bullet bulletToActivate)
    {
        bulletToActivate.gameObject.SetActive(true);

        bulletToActivate.transform.position = gunTransform.position;
        bulletToActivate.transform.rotation = gunTransform.rotation;

        bulletToActivate.RB.velocity = Vector3.zero;
        bulletToActivate.RB.angularVelocity = Vector3.zero;

        deactivatedBullets.Remove(bulletToActivate);

        return bulletToActivate;
    }

    public Bullet RequestBullet()
    {
        if (deactivatedBullets.Count > 0)
        {
            return ActivateBullet(deactivatedBullets.Last());
        }

        var bulletToReturn = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);

        return bulletToReturn;
    }
}
