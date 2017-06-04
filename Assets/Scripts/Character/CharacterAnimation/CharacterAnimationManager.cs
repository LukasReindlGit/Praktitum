using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterAnimationManagerConfiguration))]
public abstract class CharacterAnimationManager : MonoBehaviour {

    private CharacterAnimationManagerConfiguration m_configuration;
    public CharacterAnimationManagerConfiguration configuration
    {
        get
        {
            return this.m_configuration;
        }

        protected set
        {
            m_configuration = value;
            Initialize();
        }
    }


    [SerializeField]
    private ICharacterAnimationDataProvider crntCADP;
    protected Animator animator;
    protected Vector3 lastForward;




    protected Dictionary<CharacterAnimationDirection.Type, CharacterAnimationDirection> directions = new Dictionary<CharacterAnimationDirection.Type, CharacterAnimationDirection>();

    public Dictionary<CharacterAnimationDirectionBehaviour, CharacterAnimationDirectionFilter> filters = new Dictionary<CharacterAnimationDirectionBehaviour, CharacterAnimationDirectionFilter>();


    public Vector4 GetAnimationDirectionCurrentDirection(CharacterAnimationDirection.Type type)
    {
        return directions[type].currentDirection;
    }

    public Vector4 GetAnimationDirectionForFilters(CharacterAnimationDirection.Type type)
    {
        return directions[type].candidateDirection;
    }

    // GIZMO Stuff:

    private Dictionary<CharacterAnimationDirection.Type, string> gizmoPaths = new Dictionary<CharacterAnimationDirection.Type, string>()
    {
        { CharacterAnimationDirection.Type.Attention, "CharacterAnimationManager/Attention"},
        { CharacterAnimationDirection.Type.Movement, "CharacterAnimationManager/Movement"},
        { CharacterAnimationDirection.Type.Aim, "CharacterAnimationManager/Target"},
        { CharacterAnimationDirection.Type.Look, "CharacterAnimationManager/Look"},
        { CharacterAnimationDirection.Type.Body, "CharacterAnimationManager/Body"},

    };
    private Dictionary<CharacterAnimationDirection.Type, float> gizmoDistances = new Dictionary<CharacterAnimationDirection.Type, float>()
          {
        { CharacterAnimationDirection.Type.Attention, 5.0f },
        { CharacterAnimationDirection.Type.Movement, 4.0f },
        { CharacterAnimationDirection.Type.Aim, 3.0f},
        { CharacterAnimationDirection.Type.Look, 2.0f},
        { CharacterAnimationDirection.Type.Body, 1.0f},

    };

    private float gizmosVerticalOffset = 1.0f;


    /// <summary>
    /// Initialize the CharacterAnimationManager. 
    /// </summary>
    protected virtual void Initialize()
    {
        // Initialize animation vector dictionaries
        for (int i = 0; i < Enum.GetValues(typeof(CharacterAnimationDirection.Type)).Length; i++)
        {
            directions.Add((CharacterAnimationDirection.Type)i, new CharacterAnimationDirection(this, (CharacterAnimationDirection.Type)i));

        }
    }

    public void Awake()
    {

        animator = GetComponent<Animator>();
        configuration = GetComponent<CharacterAnimationManagerConfiguration>();

        var tempComponents = GetComponents(typeof(Component));
        for (int i = 0; i < tempComponents.Length; i++)
        {
            if (tempComponents[i] is ICharacterAnimationDataProvider)
            {
                crntCADP = tempComponents[i] as ICharacterAnimationDataProvider;
                break;

            }
        }

        InitializeAllFilters();
    }

    public void InitializeAllFilters()
    {
        InitializeFiltersFromDirectionConfiguration(configuration.movementDirectionConfiguration);

        InitializeFiltersFromDirectionConfiguration(configuration.attentionDirectionrConfiguration);

        InitializeFiltersFromDirectionConfiguration(configuration.lookDirectionConfiguration);

        InitializeFiltersFromDirectionConfiguration(configuration.aimDirectionConfiguration);

        InitializeFiltersFromDirectionConfiguration(configuration.bodyDirectionConfiguration);



    }
    public void InitializeFiltersFromDirectionConfiguration( CharacterAnimationDirectionConfiguration configuration)
    {

        for (int i = 0; i < configuration.normalVectorBehaviour.Count; i++)
        {
            var tempBehaviour = configuration.normalVectorBehaviour[i];
            filters.Add(tempBehaviour, new CharacterAnimationDirectionFilter(tempBehaviour.filter, configuration.normalVector,this,tempBehaviour.averageTime));
            

        }

        for (int i = 0; i < configuration.fallbackVectorBehaviour.Count; i++)
        {
            var tempBehaviour = configuration.fallbackVectorBehaviour[i];
            filters.Add(tempBehaviour, new CharacterAnimationDirectionFilter(tempBehaviour.filter, configuration.fallbackVector, this, tempBehaviour.averageTime));

        }
    }

    public void UpdateAllFilters()
    {

        foreach (CharacterAnimationDirectionBehaviour key in filters.Keys)
        {
            filters[key].UpdateValuesInFixedUpdate();
        }


    }

    private void OnDrawGizmos()
    {
        foreach(CharacterAnimationDirection.Type key in Enum.GetValues(typeof(CharacterAnimationDirection.Type)))
        {
            if (directions.ContainsKey(key))
            {
                Gizmos.DrawIcon(directions[key].currentDirection * gizmoDistances[key] + new Vector4(0, gizmosVerticalOffset,0,0), gizmoPaths[key]);
            }else
            {
                Gizmos.DrawIcon(transform.forward * gizmoDistances[key] + new Vector3(0, gizmosVerticalOffset, 0), gizmoPaths[key]);

            }
        }
       
    }



    // Gets angle around y axis from a world space direction
    public float GetAngleFromForward(Vector3 worldDirection)
    {
        Vector3 local = transform.InverseTransformDirection(worldDirection);
        return Mathf.Atan2(local.x, local.z) * Mathf.Rad2Deg;
    }

    /// Gets angle around y axis from a world space direction




    private void FetchAnimationVectorData()
    {

        directions[CharacterAnimationDirection.Type.Movement].fetchedDirection = crntCADP.MovementVectorUpdate();

        directions[CharacterAnimationDirection.Type.Attention].fetchedDirection = crntCADP.AttentionVectorVectorUpdate();

        directions[CharacterAnimationDirection.Type.Look].fetchedDirection = crntCADP.LookVectorUpdate();

        directions[CharacterAnimationDirection.Type.Aim].fetchedDirection = crntCADP.AimVectorUpdate();

        // ToDo: This is only for debug purposes
        //fetchedBodyVector = fetchedAttentionVector;
    }



    protected virtual void ApplyCurrentAnimationVectors()
    {
        ApplyMovementVector();

        ApplyAttentionVector();

        ApplyBodyVector();

        ApplyAimVector();

        ApplyLookVector();

    }

    protected virtual void UpdateCurrentAnimationVectors()
    {
        
        CalculateMovementVector();

        CalculateAttentionVector();

        CalculateBodyVector();

        CalculateAimVector();

        CalculateLookVector();
    }

    protected virtual void Update()
    {
        // Return if game is paused --> updating the animator is unnecessary
        if (Time.deltaTime == 0f)
        {
            return;
        }

        FetchAnimationVectorData();

        UpdateCurrentAnimationVectors();

        ApplyCurrentAnimationVectors();
    }

    private void FixedUpdate()
    {
        UpdateAllFilters();
    }






    protected void CalculateMovementVector() {
        directions[CharacterAnimationDirection.Type.Movement].CalculateCandidate();
        directions[CharacterAnimationDirection.Type.Movement].CalculateTarget();
        directions[CharacterAnimationDirection.Type.Movement].UpdateCurrentVector();

    }

    protected abstract void ApplyMovementVector();

    protected  void CalculateAttentionVector() {
        directions[CharacterAnimationDirection.Type.Attention].CalculateCandidate();
        directions[CharacterAnimationDirection.Type.Attention].CalculateTarget();
        directions[CharacterAnimationDirection.Type.Attention].UpdateCurrentVector();
    }

    protected abstract void ApplyAttentionVector();

    protected  void CalculateBodyVector() {
        directions[CharacterAnimationDirection.Type.Body].CalculateCandidate();
        directions[CharacterAnimationDirection.Type.Body].CalculateTarget();
        directions[CharacterAnimationDirection.Type.Body].UpdateCurrentVector();
    }

    protected abstract void ApplyBodyVector();

    protected  void CalculateAimVector() {
        directions[CharacterAnimationDirection.Type.Aim].CalculateCandidate();
        directions[CharacterAnimationDirection.Type.Aim].CalculateTarget();
        directions[CharacterAnimationDirection.Type.Aim].UpdateCurrentVector();
    }

    protected abstract void ApplyAimVector();

    protected  void CalculateLookVector() {
        directions[CharacterAnimationDirection.Type.Look].CalculateCandidate();
        directions[CharacterAnimationDirection.Type.Look].CalculateTarget();
        directions[CharacterAnimationDirection.Type.Look].UpdateCurrentVector();
    }

    protected abstract void ApplyLookVector();


    private bool VectorIsValid(Vector4 v4)
    {
        if(v4 != null)
        {
            if(v4 != default(Vector4))
            {
                return false;
            }
        }

        return true;
    }


}

