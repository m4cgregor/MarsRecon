using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConfigurationSlider : MonoBehaviour {

    const float FUEL_WEIGHT = 1f;
    const float SUIT_WEIGHT = 20f;

    public enum Property
    {
        YOUR_WEIGHT,
        MAX_FORCE,
        FUEL_AMOUNT
    }

    public Property property;
    public Text value;

    static float weight = 80f;
    static float force = 750f;
    static float fuel = 20f;

    private Slider slider;
    string sufix = " Kg";

    void Start()
    {
        if (property == Property.MAX_FORCE)
            sufix = " N";
        else if (property == Property.FUEL_AMOUNT)
			sufix = " L";
		/*float val = GetComponent<Slider> ().value;
		if (property == Property.MAX_FORCE)
			val *= 10;
		value.text = (val.ToString () + sufix);
		*/
		Slider slider = GetComponent<Slider> ();
		switch (property)
		{
		case Property.YOUR_WEIGHT:
			slider.value = weight;
			break;
		case Property.MAX_FORCE:
			slider.value = force/10;
			break;
		case Property.FUEL_AMOUNT:
			slider.value = fuel;
			break;
		}
    }

	public void UpdateLabel(float val) {
		val = GetComponent<Slider> ().value;
		if (property == Property.MAX_FORCE)
			val *= 10;
		value.text = (val.ToString () + sufix);
        
		switch (property)
		{
		case Property.YOUR_WEIGHT:
			weight = val;
			break;
		case Property.MAX_FORCE:
			force = val;
			break;
		case Property.FUEL_AMOUNT:
			fuel = val;
			break;
		}

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Rigidbody body = player.GetComponent<Rigidbody>();
        JetPack pack = player.GetComponent<JetPack>();

        body.mass = SUIT_WEIGHT + FUEL_WEIGHT * fuel + weight;
        pack.rocketPowerScale = force;
        pack.fuel = fuel;
    }


}
