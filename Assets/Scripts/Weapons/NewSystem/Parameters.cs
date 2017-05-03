using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Weapons {

    [Serializable]
    public class Parameters {

        /// <summary>
        /// Can hold the button down
        /// </summary>
        public bool isContinuousFire = false;

        /// <summary>
        /// Amount of shots per button press
        /// </summary>
        public int salveCount = 1;
        
        /// <summary>
        /// Time beween shots
        /// </summary>
        public float cooldownTime;

        /// <summary>
        /// How many times can we shoot before we have to reload
        /// </summary>
        public int clipSize = 6;

        /// <summary>
        /// Time to fill up clip
        /// </summary>
        public float reloadTime;

        /// <summary>
        /// How far can the weapon reach
        /// </summary>
        public float range = 10;

        /// <summary>
        /// How much damagepoints one salve shot inflicts on target
        /// </summary>
        public float damage = 1;
        
        /// <summary>
        /// Time need pressed before shot
        /// </summary>
        public float loadingTime;



    }
}