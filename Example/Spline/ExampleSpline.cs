using UnityEngine;
using UnityEngine.Splines;
using UnityGameLib.Component;

namespace UnityGameLib.Example.Spline
{
    public class ExampleSpline : MonoBehaviour
    {
        [SerializeField] SplineContainer _spline1;
        [SerializeField] SplineContainer _spline2;
        [SerializeField] SplineContainer _spline3d;
        [SerializeField] Transform _object1;
        [SerializeField] Transform _object2;
        [SerializeField] Transform _object3d;

        private SplineMover _mover1;
        private SplineMover _mover2;
        private SplineMover _mover3;
        void Start()
        {
            _mover1 = new SplineMover(_spline1, _object1);
            _mover2 = new SplineMover(_spline2, _object2);
            _mover3 = new SplineMover(_spline3d, _object3d);
            _mover1.MoveSpeed=1;
            _mover2.MoveSpeed=1;
            _mover3.MoveSpeed=1;
            _mover2.CurrentPos = _spline1.Spline.GetLength();
            _mover2.OnCirclePass+=()=>Debug.Log("OnCirclePass");
            _mover1.OnBoundaryPosition+=()=>
            {
                Debug.Log("OnBoundaryPosition");
                _mover1.MoveSpeed *= -1;
            };
        }

        // Update is called once per frame
        void Update()
        {
            _mover1.Move2D(Time.deltaTime,true);
            _mover2.Move2D(Time.deltaTime,false);
            _mover3.Move3D(Time.deltaTime,true);
        }
    }
}
