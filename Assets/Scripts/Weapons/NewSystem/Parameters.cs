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
        [SerializeField]
        private bool isContinuousFire = false;
        public bool IsContinuousFire
        {
            get
            {
                return isContinuousFire;
            }

            set
            {
                isContinuousFire = value;
            }
        }

        /// <summary>
        /// Amount of shots per button press
        /// </summary>
        [SerializeField]
        private int salveCount = 1;
        public int SalveCount
        {
            get
            {
                return salveCount;
            }

            set
            {
                salveCount = value;
            }
        }

        /// <summary>
        /// Time beween shots
        /// </summary>
        [SerializeField]
        private float cooldownTime;
        public float CooldownTime
        {
            get
            {
                return cooldownTime;
            }

            set
            {
                cooldownTime = value;
            }
        }

        /// <summary>
        /// How many times can we shoot before we have to reload
        /// </summary>
        [SerializeField]
        private int clipSize = 6;
        public int ClipSize
        {
            get
            {
                return clipSize;
            }

            set
            {
                clipSize = value;
            }
        }

        /// <summary>
        /// Time to fill up clip
        /// </summary>
        [SerializeField]
        private float reloadTime;
        public float ReloadTime
        {
            get
            {
                return reloadTime;
            }

            set
            {
                reloadTime = value;
            }
        }


        /// <summary>
        /// How far can the weapon reach
        /// </summary>
        [SerializeField]
        private float range = 10;
        public float Range
        {
            get
            {
                return range;
            }

            set
            {
                range = value;
            }
        }

        /// <summary>
        /// How much damagepoints one salve shot inflicts on target
        /// </summary>
        [SerializeField]
        private float damage = 1;
        public float Damage
        {
            get
            {
                return damage;
            }

            set
            {
                damage = value;
            }
        }

        /// <summary>
        /// Time need pressed before shot
        /// </summary>
        [SerializeField]
        private float loadingTime;
        public float LoadingTime
        {
            get
            {
                return loadingTime;
            }

            set
            {
                loadingTime = value;
            }
        }

        [SerializeField]
        private float criticalChance=0.1f;
        public float CriticalChance
        {
            get
            {
                return criticalChance;
            }

            set
            {
                criticalChance = value;
            }
        }

        [SerializeField]
        private float precision=0.75f;
        public float Precision
        {
            get
            {
                return precision;
            }

            set
            {
                precision = value;
            }
        }

        [SerializeField]
        private float accuracy=0.5f;
        public float Accuracy
        {
            get
            {
                return accuracy;
            }

            set
            {
                accuracy = value;
            }
        }
        
        [SerializeField]
        private float angle = 2;
        public float Angle
        {
            get
            {
                return angle;
            }

            set
            {
                angle = value;
            }
        }

        [SerializeField]
        private GameObject targetProjector;
        public GameObject TargetProjector
        {
            get
            {
                return targetProjector;
            }

            set
            {
                targetProjector = value;
            }
        }

    }
}