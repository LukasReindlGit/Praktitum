using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class Behaviour : MonoBehaviour
    {
        public Properties properties;

        public bool isPressingButton;
        public bool waitingForRelease;

        int currentClip = 0;

        IEnumerator ShootLoop()
        {
            do
            {
                // Load the shot
                yield return new WaitForSeconds(properties.loadingTime);

                // Check if we aborted the loading
                if (!isPressingButton)
                    yield return null;

                // Shoot each shot of the salve
                for (int i = properties.salveCount; i > 0; i--)
                {
                    PerformShoot();
                    yield return new WaitForSeconds(properties.cooldownTime);
                }

                // Use ammo from the clip
                currentClip--;

                // Reload if needed
                if(currentClip<=0)
                {
                    yield return new WaitForSeconds(properties.reloadTime);
                    currentClip = properties.clipSize;
                }

                waitingForRelease = true;

                // if we have a continuous fire weapon, Continue shooting until we release the button.
            } while (properties.isContinuousFire && isPressingButton);

        }

        public abstract void PerformShoot();

    }
}