using UnityEngine;

public class CameraControl : MonoBehaviour {//esta cosa deberia esar en dentro de un obejto adicional, no en la camara
    public GameObject m_Player;// al jugador que sigue la camara
    private Vector3 m_Offset;
    private Vector3 m_InicialPosition;
    private Quaternion m_InicialRotation;
    public Transform m_InitialParent;

    public void Start(){
        m_InicialRotation = transform.rotation;
        m_InicialPosition = transform.position;
    }
    public void Update(){
        transform.position = m_Player.transform.position;
        Quaternion rotate = m_Player.transform.rotation * Quaternion.AngleAxis(45f, Vector3.right);//para corregir la rotacion, por haber tomado el spawnpint como inical
        transform.rotation = rotate;
    }
    public void SetStartPosition(){
        transform.position = m_InicialPosition;
        transform.rotation = m_InicialRotation;
    }
    public void SetToPlayer(){//seria necesaria si utilizo varios jugadores
        SetStartPosition();
    }
}