using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletPool : Singleton<BulletPool>
{
    [Header("Bullet Settings")]
    public int poolSite;
    public List<BaseBullet> listBullet;

    //private Queue<BaseBullet> bulletPool = new Queue<BaseBullet>();
    public Dictionary<int, List<BaseBullet>> mapBullet = new Dictionary<int, List<BaseBullet>>();


    private void Start()
    {
        for (int j = 0; j < listBullet.Count; j++)
        {
            mapBullet[j] = new List<BaseBullet>();
            for (int i = 0; i < poolSite; i++)
            {
                BaseBullet instanceBullet = Instantiate(listBullet[j]);
                instanceBullet.gameObject.SetActive(false);
                mapBullet[j].Add(instanceBullet);
            }
        }
        //Debug.Log("mapBullet =" + mapBullet.Count);
    }
    public BaseBullet GetBullet(int index)
    {
        List<int> keys = mapBullet.Keys.ToList();

        for (int i = 0; i < keys.Count; i++)
        {
            if (index == keys[i])
            {
                List<BaseBullet> listBullet = mapBullet[index].Where(x => x.isMoved == false).ToList();
                BaseBullet tempbullet = listBullet.FirstOrDefault();
                tempbullet.isMoved = true;
                return tempbullet;
            }
        }
        return null;
    }
    public void ReturnBullet(BaseBullet bullet)
    {
        bullet.isMoved = false;
        bullet.gameObject.SetActive(false);
        mapBullet[bullet.index].Add(bullet);
    }
}
