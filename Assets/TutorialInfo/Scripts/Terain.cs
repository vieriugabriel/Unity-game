using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class Terain : MonoBehaviour
{
    public SpriteShapeController _spriteShapeController;
    public int _width = 200;
    [Range(1f, 50f)] public float _xMultiplier = 2f;
    [Range(1f, 50f)] public float _yMultiplier = 2f;
    [Range(0f, 1f)] public float _curveSmoothness = 0.5f;
    public float _noise = 0.5f;
    public float _bottom = 10f;
    [Range(1, 10)] public int _flatSectionWidth = 5; // New parameter for flat section width
    private Vector3 _lastPos;

    public void OnValidate()
    {
        _spriteShapeController.spline.Clear();

        for (int i = 0; i < _flatSectionWidth; i++)
        {
            _lastPos = transform.position + new Vector3(i * _xMultiplier, transform.position.y);
            _spriteShapeController.spline.InsertPointAt(i, _lastPos);

            if (i != 0 && i != _flatSectionWidth - 1)
            {
                _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                _spriteShapeController.spline.SetLeftTangent(i, Vector3.left * _xMultiplier * _curveSmoothness);
                _spriteShapeController.spline.SetRightTangent(i, Vector3.right * _xMultiplier * _curveSmoothness);
            }
        }

        for (int i = _flatSectionWidth; i < _width; i++)
        {
            _lastPos = transform.position + new Vector3(i * _xMultiplier, Mathf.PerlinNoise(0, i * _noise) * _yMultiplier);
            _spriteShapeController.spline.InsertPointAt(i, _lastPos);

            if (i != 0 && i != _width - 1)
            {
                _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                _spriteShapeController.spline.SetLeftTangent(i, Vector3.left * _xMultiplier * _curveSmoothness);
                _spriteShapeController.spline.SetRightTangent(i, Vector3.right * _xMultiplier * _curveSmoothness);
            }
        }

        _spriteShapeController.spline.InsertPointAt(_width, new Vector3(_lastPos.x, transform.position.y - _bottom));
        _spriteShapeController.spline.InsertPointAt(_width + 1, new Vector3(transform.position.x, transform.position.y - _bottom));
    }
}
