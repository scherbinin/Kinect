using System;
using System.Collections.Generic;
using System.Globalization;
using BvhConverter.Stuff;
using BvhConverter_new.Stuff;
using Microsoft.Kinect;
using JointType = BvhConverter_new.Stuff.JointType;

namespace BvhConverter_new
{
    /// <summary>
    /// Линейный список для потомков
    /// </summary>
    public class ChildsVector
    {
        private int _current; 
        private List<HierarchyNode> _childs;

        public ChildsVector()
        {
            _current = -1;
            _childs = new List<HierarchyNode>();
        }

        public void Reset()
        {
            _current = -1;
        }

        public HierarchyNode GetCurrent()
        {
            return _childs[_current];
        }

        public bool CanMooveNext()
        {
            if (_current + 1 >= _childs.Count)
                return false;
            else
                return true;
        }

        public bool GetNext()
        {
            if (_current + 1 >= _childs.Count)
            {
                Reset();
                return false;
            }
            else
            {
                _current++;
                return true;
            }
        }

        public void AddChild(HierarchyNode childe)
        {
            _childs.Add(childe);
        }
    }

    /// <summary>
    /// Узел скилета
    /// </summary>
    public class HierarchyNode : IEnumerable<HierarchyNode>
    {
        //Потомки
        ChildsVector _childs = null;
        //Тип точки
        private string _BvhPointType;
        private JointType _jointType;
        private SkeletonPoint _point;
        private SkeletonPoint _lastPointValue;
        //Количество табов (табуляция слева)
        public int TreeDeepValue { get; set; }
        //Родитель
        public HierarchyNode Parent { get; set; }
        //Количетво дабов, нужное что бы подняться до первого предка, где было много потомков
        public int TabValue { get; set; }

        public HierarchyNode(SkeletonPoint point, JointType jointType, string type)
        {
            _point = point;
            Parent = null;
            _BvhPointType = type;
            _jointType = jointType;

            if (Parent != null)
                TreeDeepValue = 0;
        }

        /// <summary>
        /// Получить текущую позицию
        /// </summary>
        public SkeletonPoint Point
        {
            get { return _point; }
            set
            {
                _lastPointValue = _point;
                _point = value;
            }
        }
        
        /// <summary>
        /// Получить предыдущее значение позиции (как бы история на 1 изменение)
        /// </summary>
        public SkeletonPoint LastPointValue
        {
            get { return _lastPointValue; }
        }

        public HierarchyNode AddChild(HierarchyNode node)
        {
            node.TreeDeepValue = TreeDeepValue + 1;
            node.Parent = this;

            if (_childs == null)
                _childs = new ChildsVector();
            
            _childs.AddChild(node);

            return node;
        }

        /// <summary>
        /// Получить потомка
        /// </summary>
        /// <returns></returns>
        public ChildsVector GetChilds()
        {
            return _childs;
        }

        /// <summary>
        /// Перегрузка метода
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //Табуляция + префикс + {
            string rez = Tabulate(TreeDeepValue) + GetPrefix() + _BvhPointType + Environment.NewLine;
            rez += Tabulate(TreeDeepValue) + ConstsKeyWords.LeftBrace + Environment.NewLine;

            //OFFSET + координаты
            rez += Tabulate(TreeDeepValue + 1) + ConstsKeyWords.Offset + _point.PrintCoordinate() + Environment.NewLine;

            //Если это Не лист (Root не может быть листом)
            if (_childs != null)
                if(Parent==null)
                    rez += Tabulate(TreeDeepValue + 1) + ConstsKeyWords.ChannelsPozitionRotation + Environment.NewLine;
                else
                    rez += Tabulate(TreeDeepValue + 1) + ConstsKeyWords.ChannelsRotation + Environment.NewLine;
            else
                //Если лист, то сделать концовку до нового чайлда
                for (int i = 0; i < TabValue; i++)
                    rez += Tabulate(TreeDeepValue - i) + ConstsKeyWords.RightBrace + Environment.NewLine;

            return rez;
        }

        /// <summary>
        /// Получить тип точки в BVH
        /// </summary>
        /// <returns></returns>
        public string GetTypeNode()
        {
            //if (_pointType == ConstsKeyWords.End)
            //{
            //    //Поймем для какого предка
            //    return _pointType + Parent.GetTypeNode();
            //}
            //else
                return _BvhPointType;
        }

        /// <summary>
        /// Получить префикс для каждой точки (ROOT, JOINT, END)
        /// </summary>
        /// <returns></returns>
        private string GetPrefix()
        {
            if (Parent==null)
                return ConstsKeyWords.Root;
            else
                //Если лист
                if (_childs != null)
                    return ConstsKeyWords.Joint;
                else
                    return ConstsKeyWords.End;
        }

        /// <summary>
        /// вставить необходимое количество табов
        /// </summary>
        /// <param name="tabValue"></param>
        /// <returns></returns>
        private string Tabulate(int tabValue)
        {
            string rez = "";

            for (int i = 0; i < tabValue; i++)
                rez += "\t";

            return rez;
        }

        public JointType GetJointType
        {
            get { return _jointType; }
        }

        public IEnumerator<HierarchyNode> GetEnumerator()
        {
            if (Parent!=null)
                throw new Exception("Невозможно получить IEnumerator для не корневого элемента дерева");

            return new HierarchyEnumerator(this);
        }

        public IEnumerator<KeyValuePair<HierarchyNode,HierarchyNode>> GetBoneEnumerator()
        {
            if (Parent != null)
                throw new Exception("Невозможно получить IEnumerator для не корневого элемента дерева");

            return new HierarchyBoneEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
