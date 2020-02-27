using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

    [SerializeField] static PoolManager poolManagerAux;
    [SerializeField] private BulletPool[] array;
    [SerializeField] private Vector3 num;
    Bullet[] arrayGameObject;
    Vector3 pos;
    Transform parent;
    

    public static PoolManager GetInstance()
    {
        return poolManagerAux;
    }

    public void InicializePool()
    {
        for(int i = 0; i < array.Length;i++)
        {
            //es una funcion de pool el initialize
            array[i].InstanceArray(transform.position, transform);
        }
        
    }
    private void Awake()
    {
        if (PoolManager.poolManagerAux == null)
        {
            poolManagerAux = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        //DontDestroyOnLoad(transform.gameObject);
        InicializePool();
    }

    public Bullet CallByName(string name)
    {
        //recorro el array y obtengo el nombre y lo comparo y luego lo retorno si no encontre retorno null
        for(int i = 0; i < array.Length;i++)
        {
            if (array[i].GetName() == name)
            {
                var auxObject = array[i].GetObject(transform.position, transform);
                auxObject.gameObject.SetActive(true);
                return auxObject;
            }
        }
        return null;
    }

    public void TurnOffByName(string name, Bullet objectToTurnOff)
    {
        //Recorro los pools hasta encontrar el correcto
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].GetName() == name)
            {
                //Llamo a la función que apaga el objeto
                array[i].ReturnObject(objectToTurnOff, transform);
            }
        }
    }
}
