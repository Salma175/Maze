using UnityEngine;

public static class TransformExtensions
{
    /// <summary>
    /// Copies the transform properties (position, rotation, and scale) from the source to the target.
    /// </summary>
    /// <param name="target">The target transform that will receive the properties.</param>
    /// <param name="source">The source transform from which the properties will be copied.</param>
    public static void CopyTransformFrom(this Transform target, Transform source)
    {
        if (target == null || source == null)
        {
            Debug.LogError("Source or Target Transform is null.");
            return;
        }

        target.position = source.position;
        target.rotation = source.rotation;
        target.localScale = source.localScale;
    }
}
