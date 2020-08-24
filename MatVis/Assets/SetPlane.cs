using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditCompUI : MonoBehaviour
{
    public EditableComponent component;
    public Transform Transform;
    public TextMeshProUGUI Name;
    public void Delete()
    {
        component.Dissolve();
        GameObject.Destroy(gameObject);
    }
}
public class SetPlane : EditCompUI
{
    public Vector3 Position;
    public Vector3 Normale;
    public Vector3 Uvec;
    public Vector3 Vvec;
    public Vector4 Paramform;
    public TMP_InputField PosXN;
    public TMP_InputField PosYN;
    public TMP_InputField PosZN;
    public TMP_InputField PosXK;
    public TMP_InputField PosYK;
    public TMP_InputField PosZK;
    public TMP_InputField NormX;
    public TMP_InputField NormY;
    public TMP_InputField NormZ;
    public TMP_InputField UX;
    public TMP_InputField UY;
    public TMP_InputField UZ;
    public TMP_InputField VX;
    public TMP_InputField VY;
    public TMP_InputField VZ;
    public TMP_InputField ParamX;
    public TMP_InputField ParamY;
    public TMP_InputField ParamZ;
    public TMP_InputField ParamW;
    public int variant = 0;
    public List<GameObject> variants;
    // Start is called before the first frame update
    void Start()
    {

        Name.text = component.Name;
        Name.color = component.color;
        /*int actwidth = System.Math.Min(Screen.currentResolution.width, Screen.currentResolution.height);
        float scalefactor = ((float)actwidth / 1440.0f);
        GetComponent<RectTransform>().localScale = new Vector3(scalefactor, scalefactor, 1.0f);*/
        if (PosXN != null)
        {
            PosXN.onValueChanged.AddListener(setPosXN);
        }
        if (PosYN != null)
        {
            PosYN.onValueChanged.AddListener(setPosYN);
        }
        if (PosZN != null)
        {
            PosZN.onValueChanged.AddListener(setPosZN);
        }
        if (PosXK != null)
        {
            PosXK.onValueChanged.AddListener(setPosXK);
        }
        if (PosYK != null)
        {
            PosYK.onValueChanged.AddListener(setPosYK);
        }
        if (PosZK != null)
        {
            PosZK.onValueChanged.AddListener(setPosZK);
        }
        if (NormX != null)
        {
            NormX.onValueChanged.AddListener(setNormX);
        }
        if (NormY != null)
        {
            NormY.onValueChanged.AddListener(setNormY);
        }
        if (NormZ != null)
        {
            NormZ.onValueChanged.AddListener(setNormZ);
        }
        if (UX != null)
        {
            UX.onValueChanged.AddListener(setUvecX);
        }
        if (UY != null)
        {
            UY.onValueChanged.AddListener(setUvecY);
        }
        if (UZ != null)
        {
            UZ.onValueChanged.AddListener(setUvecZ);
        }
        if (VX != null)
        {
            VX.onValueChanged.AddListener(setParamX);
        }
        if (VY != null)
        {
            VY.onValueChanged.AddListener(setVvecY);
        }
        if (VZ != null)
        {
            VZ.onValueChanged.AddListener(setVvecZ);
        }
        if (ParamX != null)
        {
            ParamX.onValueChanged.AddListener(setParamX);
        }
        if (ParamY != null)
        {
            ParamY.onValueChanged.AddListener(setParamY);
        }
        if (ParamZ != null)
        {
            ParamZ.onValueChanged.AddListener(setParamZ);
        }
        if (ParamW != null)
        {
            ParamW.onValueChanged.AddListener(setParamW);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPosXN(string x)
    {
        Position.x = float.Parse(x, CultureInfo.InvariantCulture) / 50;
        settransformNormale();
    }
    public void setPosYN(string y)
    {
        Position.z = float.Parse(y, CultureInfo.InvariantCulture) / 50;//Umwandlung von Unity koordinatensystem in reales koordinatensystem
        settransformNormale();
    }
    public void setPosZN(string z)
    {
        Position.y = float.Parse(z, CultureInfo.InvariantCulture) / 50;
        settransformNormale();
    }
    public void setPosXK(string x)
    {
        Position.x = float.Parse(x, CultureInfo.InvariantCulture) / 50;
        settransformKoordinate();
    }
    public void setPosYK(string y)
    {
        Position.z = float.Parse(y, CultureInfo.InvariantCulture) / 50;//Umwandlung von Unity koordinatensystem in reales koordinatensystem
        settransformKoordinate();
    }
    public void setPosZK(string z)
    {
        Position.y = float.Parse(z, CultureInfo.InvariantCulture) / 50;
        settransformKoordinate();
    }
    public void setNormX(string x)
    {
        Normale.x = float.Parse(x, CultureInfo.InvariantCulture);
        settransformNormale();
        NormZ.placeholder.GetComponent<TextMeshProUGUI>().text = "0";
    }
    public void setNormY(string y)
    {
        Normale.z = float.Parse(y, CultureInfo.InvariantCulture);
        settransformNormale();
        NormZ.placeholder.GetComponent<TextMeshProUGUI>().text = "0";
    }
    public void setNormZ(string z)
    {
        Normale.y = float.Parse(z, CultureInfo.InvariantCulture);
        settransformNormale();
    }
    public void setUvecX(string x)
    {
        Uvec.x = float.Parse(x, CultureInfo.InvariantCulture);
        settransformKoordinate();
    }
    public void setUvecY(string y)
    {
        Uvec.z = float.Parse(y, CultureInfo.InvariantCulture);
        settransformKoordinate();
    }
    public void setUvecZ(string z)
    {
        Uvec.y = float.Parse(z, CultureInfo.InvariantCulture);
        settransformKoordinate();
    }
    public void setParamX(string x)
    {
        Paramform.x = float.Parse(x, CultureInfo.InvariantCulture);
        settransformParam();
    }
    public void setParamY(string y)
    {
        Paramform.z = float.Parse(y, CultureInfo.InvariantCulture);
        settransformParam();
    }
    public void setParamZ(string z)
    {
        Paramform.y = float.Parse(z, CultureInfo.InvariantCulture);
        settransformParam();
    }
    public void setParamW(string w)
    {
        Paramform.w = float.Parse(w, CultureInfo.InvariantCulture) / 50;
        settransformParam();
    }
    public void setVvecX(string x)
    {
        Vvec.x = float.Parse(x, CultureInfo.InvariantCulture);
        settransformKoordinate();
    }
    public void setVvecY(string y)
    {
        Vvec.z = float.Parse(y, CultureInfo.InvariantCulture);
        settransformKoordinate();
    }
    public void setVvecZ(string z)
    {
        Vvec.y = float.Parse(z, CultureInfo.InvariantCulture);
        settransformKoordinate();
    }

    public void nextVariant()
    {
        variant = (variant + 1) % variants.Count;
        for(int i = 0; i < variants.Count; i++)
        {
            if (i == variant)
            {
                variants[i].SetActive(true);
            }
            else
            {
                variants[i].SetActive(false);
            }
        }
    }
    public void previousVariant()
    {
        variant = (variant - 1) % variants.Count;
        if (variant < 0)
        {
            variant += 3;
        }
        for (int i = 0; i < variants.Count; i++)
        {
            if (i == variant)
            {
                variants[i].SetActive(true);
            }
            else
            {
                variants[i].SetActive(false);
            }
        }
    }

    public void settransformNormale()
    {
        Transform.localPosition = Position;
        Quaternion rotation = Quaternion.FromToRotation(new Vector3(0, 1, 0), Normale);
        Transform.localRotation = rotation;
    }
    public void settransformKoordinate()
    {
        Transform.localPosition = Position;
        Normale = Vector3.Cross(Uvec, Vvec);
        Quaternion rotation = Quaternion.FromToRotation(new Vector3(0, 1, 0), Normale);
        Transform.localRotation = rotation;
    }
    public void settransformParam()

    {
        Normale = new Vector3(Paramform.x, Paramform.y, Paramform.z);
        if (Vector3.Dot(Normale, new Vector3(0, 0, 1)) != 0)
        {
            Position = new Vector3(0, 0, (-Paramform.w) / Paramform.z);
        }
        else if (Vector3.Dot(Normale, new Vector3(0, 1, 0)) != 0){
            Position = new Vector3(0, (-Paramform.w) / Paramform.y,0);
        }
        else
        {
            Position = new Vector3((-Paramform.w) / Paramform.x,0 , 0);
        }
        Transform.localPosition = Position;
        Quaternion rotation = Quaternion.FromToRotation(new Vector3(0, 1, 0), Normale);
        Transform.localRotation = rotation;
    }
}
