using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    private bool playerInRange;
    private string tagToCompare;
    [SerializeField] private PlayerAttack player;

    private string buttonPromptText = "Pick up";
    [SerializeField] private string buttonText = "E";
    // Start is called before the first frame update
    void Start()
    {
        this.tagToCompare = "PlayerTrigger";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && this.playerInRange)
        {
            this.player.SetCurrentWeapon(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(this.tagToCompare))
        {
            this.playerInRange = true;
            GameManager.Instance.SetButtonPromptActive(true, this.buttonPromptText, this.buttonText);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(this.tagToCompare))
        {
            this.playerInRange = true;
            GameManager.Instance.SetButtonPromptActive(true, this.buttonPromptText, this.buttonText);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(this.tagToCompare))
        {
            this.playerInRange = false;

            GameManager.Instance.SetButtonPromptActive(false, this.buttonPromptText, this.buttonText);
        }
    }

    public Weapon GetWeapon()
    {
        return this.weapon;
    }

    public void DropWeapon(GameObject weaponDrop, Weapon weapon, PlayerAttack player)
    {
        WeaponDrop drop = Instantiate(weaponDrop, this.transform.position, Quaternion.identity).GetComponent<WeaponDrop>();
        drop.SetWeapon(weapon);
        drop.SetPlayer(player);
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }

    public void SetPlayer(PlayerAttack player)
    {
        this.player = player;
    }
}
