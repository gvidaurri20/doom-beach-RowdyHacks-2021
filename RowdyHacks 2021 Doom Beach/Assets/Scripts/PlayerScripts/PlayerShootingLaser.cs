using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * PlayerShootingLaser.cs
 * 
 * This program focuses on anything related to the player's attack and shooting.
 *
 */
public class PlayerShootingLaser : MonoBehaviour
{
    public int damagePerShot = 20;  // How much damage each shot does to the enemy
    public float timeAttackDelay = 10f; //0.15f;  // The next interval at which the player can shoot
    public float range = 100f; // How far each shot travels

    float timer; // Timer will be used to make sure everything runs within good timed intervals (not too fast/slow etc)
    Ray shootRay;  // Ray for the laser to follow
    RaycastHit shootHit; // Return back whatever the laser hit
    int shootableMask;  // Makes sure we can only hit shootable things
    LineRenderer laserLine;  // Reference to the line renderer
    AudioSource laserBlastAudio; // Reference to gun audio for firing sound
    Light gunLight;  // The light when we fire that displays
    float effectsDisplayTime = 0.2f;  // How long these effects are viewable before they disappear

    public Transform gunEnd; // Refers to where the gun on the screen ends

    // Awake is called regardless of when game is started.
    // Sets up the components from the gun in order to use in other functions
    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        laserLine = GetComponent<LineRenderer>();
        laserBlastAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }


    void Update()
    {
        timer = timer + Time.deltaTime;

        // If the player presses the mouse button or CTRL and is within range of the rate of fire
        if(Input.GetButton("Fire1") && timer >= timeAttackDelay)
        {
            Shoot();
        }

        // If it is within the rate of fire and enough time has progresses, then the gun light will turn off
        if(timer >= timeAttackDelay * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    // Disables all effects after a shot has been fired.
    public void DisableEffects()
    {
        laserLine.enabled = false;
        gunLight.enabled = false;
    }

    // Activates whenever the player shoots
    void Shoot()
    {
        timer = 0f; // Reset the amount of time you have to wait until next shot can be fired

        laserBlastAudio.Play();  // Plays the gun shot audio

        gunLight.enabled = true;  // Enables the light from the gun to emit

        StartCoroutine(EffectOfShot());

        // Turn on the line renderer (the visual representation of the gun shot)
        laserLine.SetPosition(0, gunEnd.position);  // Beginning at the end of the gun to its travel point

        shootRay.origin = transform.position;  // The shot begins at the end of the gun barrel
        shootRay.direction = transform.forward; // And then travels forward

        // Will fire a ray forward up to the range of the gun shot.  If it hits something, it will check if that is an enemy.
        // If it is an enemy, then that enemy will take damage.
        if(Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyLife enemyHealth = shootHit.collider.GetComponent<EnemyLife>();
            if (enemyHealth != null)  // Makes sure that the target hit was an enemy
            {
                enemyHealth.TakeDamage(damagePerShot);
            }
            laserLine.SetPosition(1, shootHit.point);
        }
        else
        {
            laserLine.SetPosition(1, gunEnd.position + (shootRay.direction * range)); 
        }
    }

    // When a laser is fired, this method is called
    private IEnumerator EffectOfShot()
    {
        laserLine.enabled = true; // Displays a laser on the screen
        yield return new WaitForSeconds(0.07f); // This is how long the laser gets displayed on the screen
        laserLine.enabled = false; // Removes the on screen laser after a specified amount of time
    }
}
