using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UMA;
using UMA.CharacterSystem;
using System.Collections.Generic;

public partial class UICharacterCreation : MonoBehaviour
{
    public NetworkManagerMMO manager; // singleton is null until update
    public GameObject panel;
    public InputField nameInput;
    public Dropdown classDropdown;
    public Button createButton;
    public Button cancelButton;
    public Button male;
    public Button female;
    public Button random;
    public Slider height;
    public Slider weight;
    public Slider breast;
    public Slider cleavage;
    public Slider gluteus;
    public Slider upperMuscle;
    public Slider upperWeight;
    public Slider waist;
    public Slider legSize;
    public Slider feetSize;
    public Slider lowerWeight;
    public Slider legSeparation;
    public Slider lowerMuscle;
    public Slider armLength;
    public Slider forearmLength;
    public Slider armWidth;
    public Slider forearmWidth;
    public Slider handsSize;

    public Slider headSize;
    public Slider headWidth;
    public Slider neckThickness;
    public Slider foreheadSize;
    public Slider foreheadPosition;

    public Slider earsSize;
    public Slider earsPosition;
    public Slider earsRotation;
    public Slider cheekSize;
    public Slider cheekPosition;
    public Slider lowCheekPronounced;
    public Slider lowCheekPosition;

    public Slider noseSize;
    public Slider noseCurve;
    public Slider noseWidth;
    public Slider noseInclination;
    public Slider nosePosition;
    public Slider nosePronouced;
    public Slider noseFlatten;

    public Slider chinSize;
    public Slider chinPronouced;
    public Slider chinPosition;
    public Slider mandibleSize;
    public Slider jawSize;
    public Slider jawsPosition;

    public Slider lipsSize;
    public Slider mouthSize;
    public Slider eyeRotation;
    public Slider eyeSize;
    public Slider eyeSpacing;

    private string dnastr;

    public List<string> hairModelsMale = new List<string>();
    private int currentHairMale;

    public List<string> hairModelsFemale = new List<string>();
    private int currentHairFemale;

    public DynamicCharacterAvatar avatar;
    private Dictionary<string, DnaSetter> dna;

    public FlexibleColorPicker hairfcp;

    private void Start()
    {
        height.value = 0.5f;
        weight.value = 0.5f;
        breast.value = 0.5f;
        cleavage.value = 0.5f;
        gluteus.value = 0.5f;
        upperMuscle.value = 0.5f;
        upperWeight.value = 0.5f;
        waist.value = 0.5f;
        legSize.value = 0.5f;
        feetSize.value = 0.5f;
        lowerWeight.value = 0.5f;
        legSeparation.value = 0.5f;
        lowerMuscle.value = 0.5f;
        forearmLength.value = 0.5f;
        armLength.value = 0.5f;
        armWidth.value = 0.5f;
        forearmWidth.value = 0.5f;
        handsSize.value = 0.5f;
        headSize.value = 0.5f;
        headWidth.value = 0.5f;
        neckThickness.value = 0.5f;
        earsSize.value = 0.5f;
        earsPosition.value = 0.5f;
        earsRotation.value = 0.5f;
        noseSize.value = 0.5f;
        noseCurve.value = 0.5f;
        noseWidth.value = 0.5f;
        noseInclination.value = 0.5f;
        nosePosition.value = 0.5f;
        nosePronouced.value = 0.5f;
        noseFlatten.value = 0.5f;
        chinSize.value = 0.5f;
        chinPronouced.value = 0.5f;
        chinPosition.value = 0.5f;
        mandibleSize.value = 0.5f;
        jawSize.value = 0.5f;
        jawsPosition.value = 0.5f;
        cheekSize.value = 0.5f;
        cheekPosition.value = 0.5f;
        lowCheekPronounced.value = 0.5f;
        lowCheekPosition.value = 0.5f;
        foreheadSize.value = 0.5f;
        foreheadPosition.value = 0.5f;
        lipsSize.value = 0.5f;
        mouthSize.value = 0.5f;
        eyeRotation.value = 0.5f;
        eyeSize.value = 0.5f;
        eyeSpacing.value = 0.5f;
    }

    public void RemoveAllListeners()
    {
        avatar.CharacterUpdated.RemoveAllListeners();
        breast.onValueChanged.RemoveAllListeners();
        cleavage.onValueChanged.RemoveAllListeners();
        gluteus.onValueChanged.RemoveAllListeners();
        height.onValueChanged.RemoveAllListeners();
        weight.onValueChanged.RemoveAllListeners();
        upperMuscle.onValueChanged.RemoveAllListeners();
        upperWeight.onValueChanged.RemoveAllListeners();
        waist.onValueChanged.RemoveAllListeners();
        legSize.onValueChanged.RemoveAllListeners();
        feetSize.onValueChanged.RemoveAllListeners();
        lowerWeight.onValueChanged.RemoveAllListeners();
        legSeparation.onValueChanged.RemoveAllListeners();
        lowerMuscle.onValueChanged.RemoveAllListeners();
        armLength.onValueChanged.RemoveAllListeners();
        forearmLength.onValueChanged.RemoveAllListeners();
        armWidth.onValueChanged.RemoveAllListeners();
        forearmWidth.onValueChanged.RemoveAllListeners();
        handsSize.onValueChanged.RemoveAllListeners();
        headSize.onValueChanged.RemoveAllListeners();
        headWidth.onValueChanged.RemoveAllListeners();
        headSize.onValueChanged.RemoveAllListeners();
        headWidth.onValueChanged.RemoveAllListeners();
        neckThickness.onValueChanged.RemoveAllListeners();
        earsSize.onValueChanged.RemoveAllListeners();
        earsPosition.onValueChanged.RemoveAllListeners();
        earsRotation.onValueChanged.RemoveAllListeners();
        noseSize.onValueChanged.RemoveAllListeners();
        noseCurve.onValueChanged.RemoveAllListeners();
        noseWidth.onValueChanged.RemoveAllListeners();
        noseInclination.onValueChanged.RemoveAllListeners();
        nosePosition.onValueChanged.RemoveAllListeners();
        nosePosition.onValueChanged.RemoveAllListeners();
        noseFlatten.onValueChanged.RemoveAllListeners();
        chinSize.onValueChanged.RemoveAllListeners();
        chinPronouced.onValueChanged.RemoveAllListeners();
        chinPosition.onValueChanged.RemoveAllListeners();
        mandibleSize.onValueChanged.RemoveAllListeners();
        jawSize.onValueChanged.RemoveAllListeners();
        jawsPosition.onValueChanged.RemoveAllListeners();
        cheekSize.onValueChanged.RemoveAllListeners();
        cheekPosition.onValueChanged.RemoveAllListeners();
        lowCheekPronounced.onValueChanged.RemoveAllListeners();
        lowCheekPosition.onValueChanged.RemoveAllListeners();
        foreheadSize.onValueChanged.RemoveAllListeners();
        foreheadPosition.onValueChanged.RemoveAllListeners();
        lipsSize.onValueChanged.RemoveAllListeners();
        mouthSize.onValueChanged.RemoveAllListeners();
        eyeRotation.onValueChanged.RemoveAllListeners();
        eyeSize.onValueChanged.RemoveAllListeners();
        eyeSpacing.onValueChanged.RemoveAllListeners();
    }

    public void MainMenu()
    {

        GameObject maincamera = GameObject.Find("Main Camera");
        maincamera.transform.SetPositionAndRotation(new Vector3(1, 2, 3), Quaternion.identity);

        RemoveAllListeners();

        avatar.CharacterUpdated.AddListener(Updated);
        upperMuscle.onValueChanged.AddListener(UpperMuscleChange);
        upperWeight.onValueChanged.AddListener(UpperWeightChange);
        height.onValueChanged.AddListener(HeightChange);
        weight.onValueChanged.AddListener(WeightChange);
    }

    public void ArmMenu()
    {
        RemoveAllListeners();

        armLength.onValueChanged.AddListener(ArmLengthChange);
        forearmLength.onValueChanged.AddListener(ForearmLengthChange);
        armWidth.onValueChanged.AddListener(ArmWidthChange);
        forearmWidth.onValueChanged.AddListener(ForearmWidthChange);
        handsSize.onValueChanged.AddListener(HandsSizeChange);
        avatar.CharacterUpdated.AddListener(Updated);
    }

    public void FaceMenu()
    {
        GameObject maincamera = GameObject.Find("Main Camera");
        maincamera.transform.SetPositionAndRotation(new Vector3(0.97f, 2.8f, 4.96f), Quaternion.Euler(3.48f, 31.81f, 0f));

        RemoveAllListeners();

        headSize.onValueChanged.AddListener(HeadSizeChange);
        headWidth.onValueChanged.AddListener(HeadWidthChange);
        neckThickness.onValueChanged.AddListener(NeckThicknessChange);
        earsSize.onValueChanged.AddListener(EarsSizeChange);
        earsPosition.onValueChanged.AddListener(EarsPositionChange);
        earsRotation.onValueChanged.AddListener(EarsRotationChange);
        noseSize.onValueChanged.AddListener(NoseSizeChange);
        noseCurve.onValueChanged.AddListener(NoseCurveChange);
        noseWidth.onValueChanged.AddListener(NoseWidthChange);
        noseInclination.onValueChanged.AddListener(NoseInclinationChange);
        nosePosition.onValueChanged.AddListener(NosePositionChange);
        nosePosition.onValueChanged.AddListener(NosePronouncedChange);
        noseFlatten.onValueChanged.AddListener(NoseFlattenChange);
        chinSize.onValueChanged.AddListener(ChinSizeChange);
        chinPronouced.onValueChanged.AddListener(ChinPronouncedChange);
        chinPosition.onValueChanged.AddListener(ChinPositionChange);
        mandibleSize.onValueChanged.AddListener(MandibleSizeChange);
        jawSize.onValueChanged.AddListener(JawSizeChange);
        jawsPosition.onValueChanged.AddListener(JawPositionChange);
        cheekSize.onValueChanged.AddListener(CheekSizeChange);
        cheekPosition.onValueChanged.AddListener(CheekPositionChange);
        lowCheekPronounced.onValueChanged.AddListener(LowCheekPronouncedChange);
        lowCheekPosition.onValueChanged.AddListener(LowCheekPositionChange);
        foreheadSize.onValueChanged.AddListener(ForeheadSizeChange);
        foreheadPosition.onValueChanged.AddListener(ForeheadPositionChange);
        lipsSize.onValueChanged.AddListener(LipSizeChange);
        mouthSize.onValueChanged.AddListener(MouthSizeChange);
        eyeRotation.onValueChanged.AddListener(EyeRotationChange);
        eyeSize.onValueChanged.AddListener(EyeSizeChange);
        eyeSpacing.onValueChanged.AddListener(EyeSpacingChange);
        avatar.CharacterUpdated.AddListener(Updated);
    }

    public void LegMenu()
    {
        GameObject maincamera = GameObject.Find("Main Camera");
        maincamera.transform.SetPositionAndRotation(new Vector3(0.28f, 1.88f, 4.28f), Quaternion.Euler(1.6f, 30.98f, 0f));

        RemoveAllListeners();

        lowerMuscle.onValueChanged.AddListener(LowerMuscleChange);
        legSeparation.onValueChanged.AddListener(LegSeparationChange);
        lowerWeight.onValueChanged.AddListener(LowerWeightChange);
        feetSize.onValueChanged.AddListener(FeetSizeChange);
        legSize.onValueChanged.AddListener(LegsSizeChange);
        gluteus.onValueChanged.AddListener(GluteusChange);
        waist.onValueChanged.AddListener(WaistChange);
        avatar.CharacterUpdated.AddListener(Updated);
    }

    public void BoobMenu()
    {
        RemoveAllListeners();

        breast.onValueChanged.AddListener(BreastChange);
        cleavage.onValueChanged.AddListener(CleavageChange);
        avatar.CharacterUpdated.AddListener(Updated);
    }

    #region "Face"

    public void ChangeHairColor()
    {
        avatar.SetColor("Hair", hairfcp.color);
        avatar.UpdateColors(true);
    }

    public void ChangeHair(bool plus)
    {

        if (avatar.activeRace.name == "HumanMaleDCS")
        {
            if (plus)
            {
                currentHairMale++;
            }
            else
            {
                currentHairMale--;
            }
            currentHairMale = Mathf.Clamp(currentHairMale, 0, hairModelsMale.Count - 1);

            if (hairModelsMale[currentHairMale] == "None")
            {
                avatar.ClearSlot("Hair");
            }
            else
            {
                avatar.SetSlot("Hair", hairModelsMale[currentHairMale]);
            }
            avatar.BuildCharacter();
        }
        else if (avatar.activeRace.name == "HumanFemaleDCS")
        {
            if (plus)
            {
                currentHairFemale++;
            }
            else
            {
                currentHairFemale--;
            }
            currentHairFemale = Mathf.Clamp(currentHairFemale, 0, hairModelsFemale.Count - 1);

            if (hairModelsFemale[currentHairFemale] == "None")
            {
                avatar.ClearSlot("Hair");
            }
            else
            {
                avatar.SetSlot("Hair", hairModelsFemale[currentHairFemale]);
            }
            avatar.BuildCharacter();
        }
    }
    void HeadSizeChange(float val)
    {
        dna["headSize"].Set(val);
        avatar.BuildCharacter();
    }
    //headWidth
    void HeadWidthChange(float val)
    {
        dna["headWidth"].Set(val);
        avatar.BuildCharacter();
    }
    //neckThickness
    void NeckThicknessChange(float val)
    {
        dna["neckThickness"].Set(val);
        avatar.BuildCharacter();
    }
    //earsSize
    void EarsSizeChange(float val)
    {
        dna["earsSize"].Set(val);
        avatar.BuildCharacter();
    }
    //earsPosition
    void EarsPositionChange(float val)
    {
        dna["earsPosition"].Set(val);
        avatar.BuildCharacter();
    }
    //earsRotation
    void EarsRotationChange(float val)
    {
        dna["earsRotation"].Set(val);
        avatar.BuildCharacter();
    }
    //noseSize
    void NoseSizeChange(float val)
    {
        dna["noseSize"].Set(val);
        avatar.BuildCharacter();
    }
    //noseCurve
    void NoseCurveChange(float val)
    {
        dna["noseCurve"].Set(val);
        avatar.BuildCharacter();
    }
    //noseWidth
    void NoseWidthChange(float val)
    {
        dna["noseWidth"].Set(val);
        avatar.BuildCharacter();
    }
    //noseInclination
    void NoseInclinationChange(float val)
    {
        dna["noseInclination"].Set(val);
        avatar.BuildCharacter();
    }
    //nosePosition
    void NosePositionChange(float val)
    {
        dna["nosePosition"].Set(val);
        avatar.BuildCharacter();
    }
    //nosePronounced
    void NosePronouncedChange(float val)
    {
        dna["nosePronounced"].Set(val);
        avatar.BuildCharacter();
    }
    //noseFlatten
    void NoseFlattenChange(float val)
    {
        dna["noseFlatten"].Set(val);
        avatar.BuildCharacter();
    }
    //chinSize
    void ChinSizeChange(float val)
    {
        dna["chinSize"].Set(val);
        avatar.BuildCharacter();
    }
    //chinPronounced
    void ChinPronouncedChange(float val)
    {
        dna["chinPronounced"].Set(val);
        avatar.BuildCharacter();
    }
    //chinPosition
    void ChinPositionChange(float val)
    {
        dna["chinPosition"].Set(val);
        avatar.BuildCharacter();
    }
    //mandibleSize
    void MandibleSizeChange(float val)
    {
        dna["mandibleSize"].Set(val);
        avatar.BuildCharacter();
    }
    //jawsSize
    void JawSizeChange(float val)
    {
        dna["jawsSize"].Set(val);
        avatar.BuildCharacter();
    }
    //jawsPosition
    void JawPositionChange(float val)
    {
        dna["jawsPosition"].Set(val);
        avatar.BuildCharacter();
    }
    //cheekSize
    void CheekSizeChange(float val)
    {
        dna["cheekSize"].Set(val);
        avatar.BuildCharacter();
    }
    //cheekPosition
    void CheekPositionChange(float val)
    {
        dna["cheekPosition"].Set(val);
        avatar.BuildCharacter();
    }
    //lowCheekPronounced
    void LowCheekPronouncedChange(float val)
    {
        dna["lowCheekPronounced"].Set(val);
        avatar.BuildCharacter();
    }
    //lowCheekPosition
    void LowCheekPositionChange(float val)
    {
        dna["lowCheekPosition"].Set(val);
        avatar.BuildCharacter();
    }
    //foreheadSize
    void ForeheadSizeChange(float val)
    {
        dna["foreheadSize"].Set(val);
        avatar.BuildCharacter();
    }
    //foreheadPosition
    void ForeheadPositionChange(float val)
    {
        dna["foreheadPosition"].Set(val);
        avatar.BuildCharacter();
    }
    //lipsSize
    void LipSizeChange(float val)
    {
        dna["lipsSize"].Set(val);
        avatar.BuildCharacter();
    }
    //mouthSize
    void MouthSizeChange(float val)
    {
        dna["mouthSize"].Set(val);
        avatar.BuildCharacter();
    }
    //eyeRotation
    void EyeRotationChange(float val)
    {
        dna["eyeRotation"].Set(val);
        avatar.BuildCharacter();
    }
    //eyeSize
    void EyeSizeChange(float val)
    {
        dna["eyeSize"].Set(val);
        avatar.BuildCharacter();
    }
    //eyeSpacing
    void EyeSpacingChange(float val)
    {
        dna["eyeSpacing"].Set(val);
        avatar.BuildCharacter();
    }
    #endregion

    #region "Arms"
    void ArmLengthChange(float val)
    {
        dna["armLength"].Set(val);
        avatar.BuildCharacter();
    }

    void ForearmLengthChange(float val)
    {
        dna["forearmLength"].Set(val);
        avatar.BuildCharacter();
    }

    void ArmWidthChange(float val)
    {
        dna["armWidth"].Set(val);
        avatar.BuildCharacter();
    }

    void ForearmWidthChange(float val)
    {
        dna["forearmWidth"].Set(val);
        avatar.BuildCharacter();
    }

    void HandsSizeChange(float val)
    {
        dna["handsSize"].Set(val);
        avatar.BuildCharacter();
    }
    #endregion

    #region "Legs"
    public void GluteusChange(float val)
    {
        dna["gluteusSize"].Set(val);
        avatar.BuildCharacter();
    }

    public void LowerMuscleChange(float val)
    {
        dna["lowerMuscle"].Set(val);
        avatar.BuildCharacter();
    }

    public void LegSeparationChange(float val)
    {
        dna["legSeparation"].Set(val);
        avatar.BuildCharacter();
    }

    public void LowerWeightChange(float val)
    {
        dna["lowerWeight"].Set(val);
        avatar.BuildCharacter();
    }

    public void FeetSizeChange(float val)
    {
        dna["feetSize"].Set(val);
        avatar.BuildCharacter();
    }

    public void LegsSizeChange(float val)
    {
        dna["legsSize"].Set(val);
        avatar.BuildCharacter();
    }

    public void WaistChange(float val)
    {
        dna["waist"].Set(val);
        avatar.BuildCharacter();
    }
    #endregion

    #region "Main"

    public void UpperMuscleChange(float val)
    {
        dna["upperMuscle"].Set(val);
        avatar.BuildCharacter();
    }

    public void UpperWeightChange(float val)
    {
        dna["upperWeight"].Set(val);
        avatar.BuildCharacter();
    }

    public void ChangeSkinColor(Color col)
    {
        avatar.SetColor("Skin", col);
        avatar.UpdateColors(true);
    }

    void WeightChange(float val)
    {
        dna["belly"].Set(val);
        avatar.BuildCharacter();
    }

    void HeightChange(float val)
    {
        val = 0.2f * val + 0.4f;
        dna["height"].Set(val);
        avatar.BuildCharacter();
    }

    void Updated(UMAData data)
    {
        dna = avatar.GetDNA();
        string stink = "";
        foreach (var entry in dna)
        {
            stink += entry.Key + "\n";
        }
    }
    #endregion

    #region "Boobs"
    public void BreastChange(float val)
    {
        dna["breastSize"].Set(val);
        avatar.BuildCharacter();
    }

    public void CleavageChange(float val)
    {
        dna["breastCleavage"].Set(val);
        avatar.BuildCharacter();
    }

    #endregion

    private int i = 0;
    void Update()
    {

        // only update while visible (after character selection made it visible)
        if (panel.activeSelf)
        {
            // still in lobby?
            if (manager.state == NetworkState.Lobby)
            {
                Show();

                // Create reference to new warrior prefab with (almost) everything disabled
                avatar = GameObject.Find("Creation(Clone)").GetComponent<DynamicCharacterAvatar>();

                if (avatar != null && i < 1)
                {
                    MainMenu();
                    i++;
                }

                // Instantiate new local prefab and move camera and disable a lot of the things :)

                // Add new inputs for modifying player appearance


            }

            if (panel.transform.GetChild(6).gameObject.activeInHierarchy)
            {
                ChangeHairColor();
            }

            male.onClick.SetListener(() =>
            {
                upperMuscle.onValueChanged.AddListener(UpperMuscleChange);
                if (avatar.activeRace.name != "HumanMaleDCS")
                {
                    avatar.ChangeRace("HumanMaleDCS");
                    avatar.SetSlot("Underwear", "MaleUnderwear");
                    avatar.BuildCharacter();
                }
                //Debug.Log("Male");
            });

            female.onClick.SetListener(() =>
            {
                upperMuscle.onValueChanged.RemoveAllListeners();
                if (avatar.activeRace.name != "HumanFemaleDCS")
                {
                    avatar.ChangeRace("HumanFemaleDCS");
                    avatar.SetSlot("Underwear", "FemaleUndies2");
                    avatar.BuildCharacter();
                }
                upperMuscle.onValueChanged.AddListener(UpperMuscleChange);
            });

            try
            {
                dnastr = avatar.GetCurrentRecipe();
            }
            catch { }

            // copy player classes to class selection
            classDropdown.options = manager.playerClasses.Select(
                p => new Dropdown.OptionData(p.name)
            ).ToList();

            // only show GameMaster option for host connection
            // -> this helps to test and create GameMasters more easily
            // -> use the database field for dedicated servers!
            //gameMasterToggle.gameObject.SetActive(NetworkServer.localClientActive);

            // create
            createButton.interactable = manager.IsAllowedCharacterName(nameInput.text);
            createButton.onClick.SetListener(() =>
            {
                CharacterCreateMsg message = new CharacterCreateMsg
                {
                    name = nameInput.text,
                    classIndex = classDropdown.value,
                    dna = dnastr,
                    gameMaster = false
                };
                NetworkClient.Send(message);
                Destroy(GameObject.Find("Creation(Clone)"));

                RemoveAllListeners();

                Hide();
            });

            // cancel
            cancelButton.onClick.SetListener(() =>
            {
                Destroy(GameObject.Find("Creation(Clone)"));

                RemoveAllListeners();

                GameObject.Find("Main Camera").transform.SetPositionAndRotation(new Vector3(43.60357f, 12.41508f, -41.2471f), new Quaternion(0.2f, -0.3f, 0.1f, 0.9f));
                nameInput.text = "";
                Hide();
            });
        }
        else Hide();

    }

    public void Hide() { panel.SetActive(false); }
    public void Show() { panel.SetActive(true); }
    public bool IsVisible() { return panel.activeSelf; }
}