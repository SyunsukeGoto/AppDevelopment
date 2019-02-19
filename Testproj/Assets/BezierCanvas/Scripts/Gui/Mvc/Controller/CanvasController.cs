using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

namespace BezierCanvas
{

    public class CanvasController : MonoBehaviour
    {
        [SerializeField]
        RectTransform curveContainer;
        [SerializeField]
        CanvasView canvasView;
        [SerializeField]
        ControlPointView controlPointView;
        [SerializeField]
        PathView pathView;
        [SerializeField]
        PathModel model;
        [SerializeField]
        string bezier;
        [SerializeField]
        List<Vector3> m_point;

        ControlPointView currentEditingControlPoint;
        readonly Dictionary<Guid, ControlPointView> controlPointViews =
            new Dictionary<Guid, ControlPointView>();

        void Start()
        {
            canvasView.OnPointerDown(position =>
           {
               AddControlPoint(position);
           });

            canvasView.OnDrag(position =>
           {
               if (!IsSelected())
               {
                   return;
               }
               MoveHandle2Symmetry(currentEditingControlPoint.Id, position);
           });

            canvasView.OnEndDrag(position =>
           {
               if (!IsSelected())
               {
                   return;
               }
               MoveHandle2Symmetry(currentEditingControlPoint.Id, position);
           });

            canvasView.OnClickButtonCorner(() =>
            {
                if (!IsSelected())
                {
                    return;
                }
                ToCorner(currentEditingControlPoint.Id);
            });

            canvasView.OnClickButtonSmooth(() =>
            {
                if (!IsSelected())
                {
                    return;
                }
                ToSmooth(currentEditingControlPoint.Id);
            });

            canvasView.OnClickButtonDelete(() =>
            {
                if (!IsSelected())
                {
                    return;
                }
                DeleteControlPoint(currentEditingControlPoint.Id);
            });

       
            canvasView.OnClickButtonClear(Clear);

            canvasView.OnClickButtonSave(Save);
        }

        void Select(Guid id)
        {
            var point = model.GetControlPoint(id);
            if (point == null)
            {
                return;
            }
            foreach (var view in controlPointViews.Values)
            {
                view.SetSelected(view.Id == id);
            }
            currentEditingControlPoint = controlPointViews[id];
        }

        bool IsSelected()
        {
            return currentEditingControlPoint != null;
        }

        void AddControlPoint(Vector3 anchore)
        {
            var id = Guid.NewGuid();
            var view = InstantiateControlPointView(id);
            controlPointViews.Add(id, view);

            var controlPoint = CreateControlPoint(anchore);
            model.AddControlPoint(id, controlPoint);
            view.UpdateControlPoint(controlPoint);

            Select(id);
        }

        void DeleteControlPoint(Guid id)
        {
            if (controlPointViews.ContainsKey(id))
            {
                DestroyObject(controlPointViews[id].gameObject);
                controlPointViews.Remove(id);
            }
            model.DeleteControlPoint(id);
        }

        void Clear()
        {
            foreach (var view in controlPointViews.Values)
            {
                model.DeleteControlPoint(view.Id);
                DestroyObject(view.gameObject);
            }
            controlPointViews.Clear();
        }

        void Save()
        {
            ChengeVector3ToString();
            StreamWriter sw = new StreamWriter(@"BezierData.csv", false, Encoding.GetEncoding("Shift_JIS"));
            sw.WriteLine(bezier);
            sw.Close();
         

        }

        void ChengeVector3ToString()
        {
            for(int i = 0; i < m_point.Count; i++)
            {
                bezier += m_point[i].x.ToString() + "," + m_point[i].y.ToString() + "," + m_point[i].z.ToString() + "\n";
            }
        }


        void ToSmooth(Guid id)
        {
            model.ToSmooth(id);
            UpdateControlPoint(id);
        }

        void ToCorner(Guid id)
        {
            model.ToCorner(id);
            UpdateControlPoint(id);
        }

        void MoveHandle1(Guid id, Vector2 handle)
        {
            model.MoveHandle1(id, handle);
            UpdateControlPoint(id);
        }

        void MoveHandle2(Guid id, Vector2 handle)
        {
            model.MoveHandle2(id, handle);
            UpdateControlPoint(id);
        }

        void MoveHandle1Symmetry(Guid id, Vector2 handle)
        {
            model.MoveHandle1Symmetry(id, handle);
            UpdateControlPoint(id);
        }

        void MoveHandle2Symmetry(Guid id, Vector2 handle)
        {
            model.MoveHandle2Symmetry(id, handle);
            UpdateControlPoint(id);
        }

        void MoveControlPoint(Guid id, Vector2 anchore)
        {
            model.MoveControlPoint(id, anchore);
            UpdateControlPoint(id);
        }

        void UpdateControlPoint(Guid id)
        {
            var point = model.GetControlPoint(id);
            controlPointViews[id].UpdateControlPoint(point);
        }

        void UpdatePath(Path path)
        {
            const int Segments = 20;
            var points = CubicBezier.GetPoints(path, Segments);
            // リストにベジェ曲線を登録
            pathView.UpdatePath(points);
            m_point = points;
            //ChengeVector3ToString();
           
        }

        void LateUpdate()
        {
            if (model.Dirty)
            {
                UpdatePath(model.GetPath());
                model.ClearDirty();
            }
        }

        ControlPoint CreateControlPoint(Vector3 position)
        {
            return new ControlPoint
            {
                Anchore = position,
                Handle1 = position,
                Handle2 = position
            };
        }

        ControlPointView InstantiateControlPointView(Guid id)
        {
            var viewObject = GameObject.Instantiate(controlPointView.gameObject, curveContainer);
            var view = viewObject.GetComponent<ControlPointView>();
            view.Id = id;
            view.OnPointerDown(Select);
            view.OnDragHandle1(MoveHandle1);
            view.OnDragHandle2(MoveHandle2);
            view.OnDragAnchore(MoveControlPoint);
            return view;
        }
    }
}
