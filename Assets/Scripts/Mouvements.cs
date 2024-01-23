using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Le script permet de controler les mouvements du missile en utilisant des forces de poussee, de la trainee et de la gravite.
/// Le script commence par definir des constantes et des variables, comme le rayon du missile, la densite de l'air, la quantite
/// de carburant, la vitesse de rotation et le temps de combustion. Ensuite, il y a des textes pour afficher les informations telles
/// que la vitesse, l'acceleration, le deplacement, la masse totale et le temps. Le script utilise un Rigidbody2D pour 
/// gerer la physique du missile. Il y a aussi un ParticleSystem pour representer la trainee du missile. Les physiques sont 
/// desactivees avant le lancement du missile. Dans la methode FixedUpdate(), le script ajoute les forces de poussee et de trainee,
/// calcule l'acceleration et met a jour les textes. La methode Thrust() est appelee pour ajouter les forces de poussee et de trainee.
/// La methode CalculAcceleration() calcule l'acceleration du missile en utilisant la deuxieme loi de Newton. 
/// La methode UpdateText() met a jour les textes avec les informations actuelles du missile. Le script a egalement une methode
/// masseVariation() qui retourne la variation de la masse selon le temps. Cette methode est utilisee pour calculer la masse totale
/// du missile en fonction de la quantite de carburant restante.
/// </summary>
public class Mouvements : MonoBehaviour
{
    // Constantes
    public const float densiteAirIni = 1.225f; // kg/m^3 : densite de l'air a 15 degres
    public float rayonMissile;// le rayon du missile
    private const float rayonMissile1 = 2.2f / 2; // le rayon du missile dans la phase 1
    private const float rayonMissile2 = 1.8f / 2; // le rayon du missile dans la phase 2
    private const float rayonMissile3 = 1.5f / 2; // le rayon du missile dans la phase 3

    // Variables
    private float coefTrainee; // coefficient de trainee du missile

    public float carburant;// carburant du missile en kg
    public float rotationSpeed = 1f; // vitesse de rotation du missile
    public float tempBrule;// le temps de la phase de propulsion du missile
    public float timeOffset = 10;// offset du temps apres la phase de boost avant que le missile s'aligne avec le vecteur de sa vitesse

    // texte de l'acceleration
    public GameObject accelerationText;
    TextMeshProUGUI actualText;
    //texte de la vitesse
    public GameObject vitText;
    TextMeshProUGUI actualVitText;
    // texte du deplacement
    public GameObject depText;
    TextMeshProUGUI actualDepText;
    // texte de la masse totale
    public GameObject masseTText;
    TextMeshProUGUI actualMasseTText;
    // texte du temps de brulure
    public GameObject burnTimeText;
    TextMeshProUGUI actualBurnTimeText;

    public Slider slider;// slider pour choisir la quantite de carburant
    public Slider slider2;// slider qui indique la quantite de carburant restant

    public bool launch = false;// est-ce que le missile a ete lance?
    public float dryMass = 2833.8f;// la masse du missile sans de carburant

    private Rigidbody2D rb;// le rigidbody2d attache au missile(permet de gerer la physique du missile
    private float firstStage = 776.00115456f;// module de la force de poussee de la phase 1
    private float secondStage = 419.8836511f;// module de la force de poussee de la phase 2
    private float thirdStage = 124.883821f;// module de la force de poussee de la phase 3
    public float thrustForce;// module de la force de poussee dependamment du stage
    private float temps = 0;// pour calculer le temps de brulure
    private Vector2 forcePoussee;// vector de la force de poussee
    private Vector2 prevVelocity;// la vitesse du fixed update precedent
    private Vector2 acceleration;// l'acceleration du missile
    private ParticleSystem trail;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();// Get the Rigidbody2D component
        trail = GetComponent<ParticleSystem>();
        rb.bodyType = RigidbodyType2D.Kinematic;// les physiques sont desactives avant le lancement

        trail.Pause(true);// le feu derriere le missile est desactive

        //thrustForce = firstStage;// on assigne la force de la premiere phase
        prevVelocity = rb.velocity;// on assigne la vitesse precedente

        // assignation des variables textes
        actualText = accelerationText.GetComponent<TextMeshProUGUI>();
        actualVitText = vitText.GetComponent<TextMeshProUGUI>();
        actualDepText = depText.GetComponent<TextMeshProUGUI>();
        actualMasseTText = masseTText.GetComponent<TextMeshProUGUI>();
        actualBurnTimeText = burnTimeText.GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        // si le missile n'est pas lancer, les sliders peuvent etre ajuster
        if (!launch)
        {
            carburant = slider.value;
            slider2.maxValue = carburant;
            slider2.value = carburant;
            rb.mass = carburant + dryMass;
            tempBrule = 246.30f * (carburant / 31004);// calcul du temps de brulure dependamment du carburant choisi par l'utilisateur
        }
        Thrust();// ajout des forces de poussee
        CalculAcceleration();// calcul de l'acceleration
        UpdateText();// la mise a jour des textes
    }

    /// <summary>
	/// Retourne la variation de la masse selon le temps
	/// </summary>
	/// <returns> 
    /// un float qui represente la variation de la masse du missile. 
    /// </returns>
    private float masseVariation()
    {
        float variation = 0;

        if (temps <= tempBrule * (0.75f / 3))
        {
            variation = 272.155f;
            Debug.Log("variation:" + variation);
        }
        else if (temps > tempBrule * (0.75f / 3) && temps <= (tempBrule * (1.75f / 3)))
        {
            variation = 121.869f;
            Debug.Log("variation:"+ variation);
        }
        else if (temps > (tempBrule * (1.75f / 3)) && temps <= tempBrule)
        {
            variation = 41.304f;
            Debug.Log("variation:" + variation);
        }

        return -variation / 50f;// on le divise par 50 car le fixed update, update 50 fois par seconde
    }
    /// <summary>
    /// Assigne la force de poussee et le rayon du missile a son stage respectif
    /// </summary>
    private void burnTime()
    {
        if (temps <= tempBrule * (0.75f / 3))
        {
            thrustForce = firstStage;
            Debug.Log("poussee:" + thrustForce);
            rayonMissile = rayonMissile1;
            Debug.Log("Rayon:" + rayonMissile);
            dryMass = 2833.8f;
            Debug.Log("Drymass" + dryMass);
            rb.mass = carburant + dryMass;
            
        }
        else if (temps > tempBrule * (0.75f / 3) && temps <= (tempBrule * (1.75f / 3)))
        {
            thrustForce = secondStage;
            rayonMissile = rayonMissile2;
            dryMass = 1392f;
            rb.mass = carburant + dryMass;
            Debug.Log("poussee:" + thrustForce);
            Debug.Log("Rayon:" + rayonMissile);
            Debug.Log("Drymass" + dryMass);
        }
        else if (temps > tempBrule * (1.75f / 3) && temps <= tempBrule)
        {
            thrustForce = thirdStage;
            rayonMissile = rayonMissile3;
            dryMass = 727.5f;
            rb.mass = carburant + dryMass;
            Debug.Log("poussee:" + thrustForce);
            Debug.Log("Rayon:" + rayonMissile);
            Debug.Log("Drymass" + dryMass);
        }
    }
    /// <summary>
    /// Pour gerer l'evenement generer en cliquant sur le bouton lancement
    /// </summary>
    public void setLaunch()
    {
        if (carburant > 0)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;// activation des physiques
            launch = true;// est-ce que le missile est lance? oui
            slider.interactable = false;// interaction avec le slider pour la selection du carburant impossible
        }
    }
    /// <summary>
    /// Gerer le coefficient de trainee du missile dependamment de sa vitesse en y
    /// </summary>
    /// <returns></returns>
    private float calculCoefficentTrainee()
    {

        float mach = Mathf.Abs(rb.velocity.magnitude) / 0.344f;// calcul du mach

        // si la vitesse en y est plus grande que 0
        if (rb.velocity.y > 0)
        {

            if (mach <= 0.8f)
            {
                coefTrainee = 0.29f;
            }
            else if (mach > 0.8f && mach <= 1.068f)
            {
                coefTrainee = mach - 0.51f;
            }
            else if (mach < 1.068f)
            {
                coefTrainee = 0.091f + 0.5f / mach;
            }
        }
        // sinon
        else
        {
            coefTrainee = 0.35f;
        }

        return coefTrainee;
    }
    /// <summary>
    /// Gere la force de trainee
    /// </summary>
    /// <returns></returns>
    private Vector2 Trainee()
    {

        rb.gravityScale = 0.001f * (6371 / (rb.position.y + 1.001398f + 6371)) * (6371 / (rb.position.y + 1.001398f + 6371));// mise e jour de la gravite en fonction de l'altitude

        float densitee = densiteAirIni * Mathf.Exp(-((rb.position.y + 1.001398f) * 1000) / 8430f);// equation de la densite

        float surfaceFrontale = Mathf.PI * Mathf.Pow(rayonMissile, 2); // m^2 : surface frontale du missile

        float forceTrainee = 0.5f * densitee * calculCoefficentTrainee() * surfaceFrontale * rb.velocity.magnitude * 1000 * rb.velocity.magnitude * 1000;// Calcul de la force de trainee

        return -forceTrainee * rb.velocity.normalized / 1000;

    }
    /// <summary>
    /// Calcul de l'acceleration du missile
    /// </summary>
    private void CalculAcceleration()
    {
        // La vitesse actuelle
        Vector2 currentVelocity = rb.velocity;

        // La variation de la vitesse
        Vector2 deltaVelocity = currentVelocity - prevVelocity;

        // La variation du temps
        float deltaTime = Time.fixedDeltaTime;

        acceleration = deltaVelocity / deltaTime;

        // Sauvegarde de la vitesse pour le prochain fixed update
        prevVelocity = currentVelocity;

    }
    /// <summary>
    /// Met a jour les informations textes du missiles
    /// </summary>
    private void UpdateText()
    {
        actualText.text = acceleration.ToString("F4") + ("km/s*s");
        actualVitText.text = rb.velocity.ToString("F4") + ("km/s");
        actualDepText.text = (rb.position - new Vector2(18.183f, -1.001398f)).ToString() + ("km");
        actualMasseTText.text = rb.mass.ToString("0") + ("kg");
        actualBurnTimeText.text = temps.ToString("F2");
        GameplayManager.Instance.UpdateRocketInfo(rb.velocity.magnitude, rb.transform.position.y + 1.001398f);
    }
    /// <summary>
    /// Application de la force de poussee et de la force de trainee.
    /// </summary>
    private void Thrust()
    {
        burnTime(); // assignation des modules des forces de poussee et des rayons dependamment de la phase du missile

        forcePoussee = thrustForce * (Vector2) gameObject.transform.up+Trainee();// calcul de la force de poussee

        if (carburant>0)// si le missile a du carburant

        {
            missileGuidance();// rotation du missile possible
            if (launch)// si le missile est lance
            {
                rb.AddForce(forcePoussee);// ajout de la force de poussee
                trail.Play(true);// activation du feu derriere le missile
                carburant += masseVariation();// mise a jour des masses
                slider2.value = carburant;// mise a jour du slider pour le carburant restant
                temps += 0.02f;// iteration du temps
            }

            else
            {
                trail.Pause(true);// le feu derriere le missile est desactive
            }
        }
        else
        {
            trail.Pause(true);// le feu derriere le missile est desactiveu
            if (launch)
            {
                ajustement();// ajustement de l'orientation du missile apres que tout le carburant a ete brule

            }

        }
        rb.AddForce(Trainee());

    }
    /// <summary>
    /// Pour gerer la rotation du missile
    /// </summary>
    private void missileGuidance()
    {
        float rotationAmount = 0;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            // Recupere l'entree de l'utilisateur pour tourner le missile
            float horizontalInput = Input.GetAxis("Horizontal");

            // Calcule la rotation a appliquer en fonction de l'entree utilisateur
            rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;

            transform.Rotate(0, 0, -rotationAmount);// rotation du missile
        }



    }
    /// <summary>
    /// Permet d'ajuster l'orientation du missile pour qu'il soit aligne avec le vecteur de sa vitesse
    /// </summary>
    private void ajustement()
    {
        var dir = rb.velocity;// vecteur vitesse
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;// angle du vecteur vitesse

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));// rotation desire
        if (temps < tempBrule + timeOffset)// si on est dans la phase de propulsion
        {
            // la rotation avec le vecteur vitesse est plus lente
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100 * Time.fixedDeltaTime);
        }
        else// sinon le missile a la meme rotation que le vecteur vitesse
        {
            transform.rotation = targetRotation;

        }
    }

}
