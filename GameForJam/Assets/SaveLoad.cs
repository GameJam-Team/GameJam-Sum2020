using UnityEngine;
using System.Collections;

public class SaveLoad : MonoBehaviour
{
    void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
			savePosition();

		if (Input.GetKeyDown(KeyCode.L))
			if (PlayerPrefs.HasKey("PosX"))  // проверяем, есть ли в сохранении подобная информация
				loadPosition();
	}

	public void savePosition()
	{

		Transform CurrentPlayerPosition = this.gameObject.transform;
		HealthController playerHealth = GetComponent<HealthController>();

		PlayerPrefs.SetFloat("PosX", CurrentPlayerPosition.position.x); // т.к. автоматической работы 
		PlayerPrefs.SetFloat("PosY", CurrentPlayerPosition.position.y); // с массивами нет, разбиваем на
		PlayerPrefs.SetFloat("PosZ", CurrentPlayerPosition.position.z);  // отдельные float и записываем

		PlayerPrefs.SetFloat("AngX", CurrentPlayerPosition.eulerAngles.x);
		PlayerPrefs.SetFloat("AngY", CurrentPlayerPosition.eulerAngles.y);

		PlayerPrefs.SetInt("HP", (int)playerHealth.Health);
	}

	public void loadPosition()
	{

		Transform CurrentPlayerPosition = this.gameObject.transform;
		HealthController playerHealth = GetComponent<HealthController>();

		Vector3 PlayerPosition = new Vector3(PlayerPrefs.GetFloat("PosX"),
					PlayerPrefs.GetFloat("PosY"), PlayerPrefs.GetFloat("PosZ"));
		Vector3 PlayerDirection = new Vector3(PlayerPrefs.GetFloat("AngX"), // генерируем новые вектора 
					PlayerPrefs.GetFloat("AngY"), 0);  // на основе загруженных данных

		CurrentPlayerPosition.position = PlayerPosition; // и применяем их
		CurrentPlayerPosition.eulerAngles = PlayerDirection;

		playerHealth.Health = (uint)PlayerPrefs.GetInt("HP");
	}
}
