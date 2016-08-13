using UnityEngine;

public class GroundControler : MonoBehaviour {
    public void OnTriggerEnter(Collider other){//cambiar esto a Stay hace que sea muy pesado
        GameObject canica = other.gameObject;
        if(canica.layer == LayerMask.NameToLayer("Jugador")){
            CanicaPlayer canicaPlayer = canica.GetComponent<CanicaPlayer>();
            if(canicaPlayer.m_Fired){
            Rigidbody canicaRigidbody = canica.GetComponent<Rigidbody>();
            print(canicaRigidbody.velocity);
            print(canicaRigidbody.velocity * -1f);
            canicaRigidbody.AddForce(canicaRigidbody.velocity.normalized * -100f, ForceMode.Acceleration);
            print("Desacelerar");
            if(canicaRigidbody.IsSleeping())
                print("Sleeping");
            }
        } 
        if(canica.layer == LayerMask.NameToLayer("Objetivo")){
            Rigidbody canicaRigidbody = canica.GetComponent<Rigidbody>();
            print(canicaRigidbody.velocity);
            print(canicaRigidbody.velocity * -1f);
            canicaRigidbody.AddForce(canicaRigidbody.velocity * -2f, ForceMode.Force);
        }
    }
    public void OnTriggerExit(Collider other){

    }
}
    //loque se me ocurre es hacer que cuando una canica colisione con la superficie, le añada una desaceleracin en contra de su velocidad, hasta hacerla c0, debo usar velocity, una fracion de esto
    //cuando salga o deje esa colicion, debo esa desaceleracin ya no sumar a la velocidad, que siga su curso normalized
    //el vector velocidad, tiene sus tres componentes, su magnitud es la velocida de esa direccion, al cambiar el sentido, debo multiplicarla por un factor, la pregunta es si asl usar el operador * es la forma correcta, de obtener un multiplo de ese vector o no
    //o poner la desaceleracion como un vector estatico, es decir siempre deshacelera digamos  5/ms2 entonces la magnitu del vector resultante, cada segudno debo reducirlo en 5 unidades, esto cambiarlo en cada fixedupdate, suavizando con el delta time