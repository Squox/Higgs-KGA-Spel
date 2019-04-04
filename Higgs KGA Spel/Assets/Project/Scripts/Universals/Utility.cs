using System.Collections;
using UnityEngine;

public static class Utility
{
    /// <summary>
    /// Requires either only one object, which's value is to be faded, not multiple.
    /// </summary>
    /// <param name="sprite">Spriterenderer of image to fade.</param>
    /// <param name="audio">AudioSource of audio to fade.</param>
	public static IEnumerator FadeOut(SpriteRenderer sprite = null, AudioSource audio = null, float fadeTime = 60f, float startValue = 1f, float endValue = 0f)
    {
        //XNOR gate to ensure only one of the parameters are set.
        if (!(sprite == null ^ audio == null))
        {
            Debug.LogError("More than one objects to fade are set.");
            yield break;
        }

        if (audio != null)
        {
            audio.volume = startValue;

            while (audio.volume > endValue)
            {
                audio.volume -= startValue / fadeTime;

                yield return null;
            }
        }
        else
        {
            float alphaLevel = startValue;

            while (sprite.color.a > endValue)
            {
                alphaLevel -= startValue / fadeTime;
                sprite.color = new Color(1f, 1f, 1f, alphaLevel);

                yield return null;
            }

            sprite.enabled = false;
        }
    }

    /// <summary>
    /// Requires either only a SpriteRenderer or only an AudioSource, not both.
    /// </summary>
    /// <param name="sprite">Spriterenderer of image to fade.</param>
    /// <param name="audio">AudioSource of audio to fade.</param>
    public static IEnumerator FadeIn(SpriteRenderer sprite = null, AudioSource audio = null, float fadeTime = 60f, float startValue = 0f, float endValue = 1f)
    {
        //XNOR gate to ensure only one of the parameters are set.
        if (!(sprite == null ^ audio == null))
        {
            Debug.LogError("More than one objects to fade are set.");
            yield break;
        }

        if (audio != null)
        {
            audio.volume = startValue;

            while (audio.volume < endValue)
            {
                audio.volume += startValue / fadeTime;

                yield return null;
            }
        }
        else
        {
            float alphaLevel = startValue;
            sprite.enabled = true;
            sprite.color = new Color(1f, 1f, 1f, alphaLevel);

            while (sprite.color.a > endValue)
            {
                alphaLevel += startValue / fadeTime;
                sprite.color = new Color(1f, 1f, 1f, alphaLevel);

                yield return null;
            }
        }
    }

    /// <summary>
    /// Flips an object around an axis in 2D space via a given method.
    /// </summary>
    /// <param name="go">GameObject to be flipped.</param>
    /// <param name="method">Method of flipping:
    /// "SpriterRenderer" utilizes the built in flip function of the SpriteRenderer;
    /// "Rotation" rotates the Transform by 180 degrees;
    /// "Scale" inverts the scale.</param>
    /// <param name="axis">Axis around which to rotate.</param>
    public static void Flip2D(GameObject go, string method = "SpriterRenderer", string axis = "x")
    {
        string[] flipMethods = new string[3];

        flipMethods[0] = "SpriterRenderer";
        flipMethods[1] = "Rotation";
        flipMethods[2] = "Scale";

        if (!flipMethods.ArrayContains(method) || !(axis == "x" || axis == "y"))
            return;

        if (method == flipMethods[0])
        {
            if (axis == "x")
                go.GetComponent<SpriteRenderer>().flipX = !go.GetComponent<SpriteRenderer>().flipX;
            else
                go.GetComponent<SpriteRenderer>().flipY = !go.GetComponent<SpriteRenderer>().flipY;
        }
        else if (method == flipMethods[1])
        {
            if (axis == "x")
                go.transform.rotation = Quaternion.Euler(go.transform.rotation.x - 180, go.transform.rotation.y, go.transform.rotation.z);
            else
                go.transform.rotation = Quaternion.Euler(go.transform.rotation.x, go.transform.rotation.y - 180, go.transform.rotation.z);
        }
        else
        {
            if (axis == "x")
                go.transform.localScale = new Vector3(go.transform.localScale.x * -1, go.transform.localScale.y, go.transform.localScale.z);
            else
                go.transform.localScale = new Vector3(go.transform.localScale.x, go.transform.localScale.y * -1, go.transform.localScale.z);
        }
    }

    /// <summary>
    /// Checks whether two Transforms are whithin a given distance of each other.
    /// </summary>
    /// <returns>Returns true if the Transforms are in range of each other.</returns>
    public static bool IsInRange(Transform transform1, Transform transform2, float rangeX, float rangeY = 0)
    {
        if (transform1 == null || transform2 == null)
            return false;

        if (rangeY == 0)
        {
            if (Mathf.Abs(transform1.position.x - transform2.position.x) < rangeX)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (Mathf.Abs(transform1.position.x - transform2.position.x) < rangeX && Mathf.Abs(transform1.position.y - transform2.position.y) < rangeX)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Sets the layer of the passed GameObject and all of it's children to the passed layer.
    /// </summary>
    public static void SetLayerOfAllChildren(GameObject go, string layer)
    {
        go.layer = LayerMask.NameToLayer(layer);

        foreach (Transform child in go.transform)
        {
            SetLayerOfAllChildren(child.gameObject, layer);
        }
    }
}