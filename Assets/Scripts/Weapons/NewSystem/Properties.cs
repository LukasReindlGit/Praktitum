using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Weapons {

    [Serializable]
    public class Properties {

        /// <summary>
        /// Time need pressed before shot
        /// </summary>
        public float loadingTime;

        /// <summary>
        /// Time beween shots
        /// </summary>
        public float cooldownTime;

        /// <summary>
        /// Time to fill up clip
        /// </summary>
        public float reloadTime;

        /// <summary>
        /// Can hold the button down
        /// </summary>
        public bool isContinuousFire = false;

        /// <summary>
        /// Amount of shots per button press
        /// </summary>
        public int salveCount = 1;

        /// <summary>
        /// How many times can we shoot before we have to reload
        /// </summary>
        public int clipSize = 6;
    }
}