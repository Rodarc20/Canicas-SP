using UnityEngine;
using UnityEngine.UI;//aqui hare modificaciones al ui, por ejemplo la barra de fuerza o marcador de llegada

public class PlayerThorw : MonoBehaviour {
    public int m_PlayerNumber = 1;
    public int m_Rigidbdody;//rigidbody de la canica del player
    public Transform m_ThrowDirection;
    //public Slider slider;
    public float m_MinForce = 0f;
    public float m_MaxForce = 50f;
    public float m_MaxChargeTime = 1f;//un segudno en cargar toda la barra de fuerza

    private string m_ThrowButton;
    private float m_CurrentThrowForce;
    private float m_ChargeSpeed;
    private bool m_Throwed;

    private void OnEnable(){
        m_CurrentThrowForce = m_MinForce;
        //aui se debe reiniciar el slider posterior
    }

    private void Update(){
        //si me paso del maximo de la barra no debo lanzar la canica, por que puede que el jugador aun quiera modificar la direccion, por ello podra aun moverse, solo se disparara cuando el jugador suelte la tecla de deisparo
    }
}