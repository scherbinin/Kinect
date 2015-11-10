using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BvhConverter_new
{
    /// <summary>
    /// Обходящий дерево сверху вправо по узлам
    /// </summary>
    public class HierarchyEnumerator : IEnumerator<HierarchyNode> //IEnumerable<HierarchyNode> 
    {
        #region Инфрастуктура для второго энумератора

        /// <summary>
        /// Признак подьема по иерархии (от листа, к родителю)
        /// </summary>
        private bool _moovingUp;

        /// <summary>
        /// Родитель точки, которая будет возвращена в Current
        /// </summary>
        private HierarchyNode _parentForCurrentNode;
        
        #endregion

        /// <summary>
        /// Корень
        /// </summary>
        private HierarchyNode _root;
        /// <summary>
        /// Текущее ядро в обходе
        /// </summary>
        private HierarchyNode _currentNode;
        /// <summary>
        /// Порядковый номер каждой следующий точки в древе при обходе
        /// </summary>
        private int _index = 0;

        public HierarchyEnumerator(HierarchyNode root)
        {
            _root = root;
        }

        /// <summary>
        /// Родитель точки, которая будет возвращена в Current
        /// Родитель "корректен", т.е. если даже мы поднялись по дереву вверх, до ближайшего разветвления, также будет возвращен корректный родитель
        /// </summary>
        public HierarchyNode ParentForCurrentNode
        {
            get { return _parentForCurrentNode; }
        }

        /// <summary>
        /// Признак, не было ли в данной операции перемещения
        /// от узла к узлу поднятия по иерархии древа вверх
        /// </summary>
        public bool IsMoovingUp
        {
            get { return _moovingUp; }
        }

        /// <summary>
        /// Получить номер узла по порядку, который мы посещаем
        /// </summary>
        /// <returns></returns>
        public int GetCurrentPointNumber()
        {
            return _index;
        }

        #region IEnumerator public

        public void Reset()
        {
            _currentNode = _root;
            _index = 0;
        }

        public bool MoveNext()
        {
            if (_currentNode == null)
            {
                Reset();
                return true;
            }

            _currentNode = GetNextChild();

            if (_currentNode != null)
            {
                _index++;
                return true;
            }
            else
                return false;
        }

        public HierarchyNode Current
        {
            get { return _currentNode; }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        object System.Collections.IEnumerator.Current
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        /// <summary>
        /// Возьмем следующего потомка
        /// </summary>
        /// <returns></returns>
        private HierarchyNode GetNextChild()
        {
            _moovingUp = false;

            //Если не лист, то взять следующего потомка (Спуститься вниз)
            if (_currentNode.GetChilds() != null)
            {
                _currentNode.GetChilds().GetNext();
                _currentNode.GetChilds().GetCurrent().TabValue = GetTabsValue(_currentNode.GetChilds().GetCurrent());

            }
            else
            {
                _moovingUp = true;//Признак того что произошел подьем по иерархии

                //Если лист, взять первого родителя с незаконченным обходом списка потомков
                //Как только поднимемся до ROOT возвращаем NULL
                do
                {
                    _currentNode = _currentNode.Parent;

                    if (_currentNode == null) return null;//Если это был root и мы получили его родителя
                } while (!_currentNode.GetChilds().GetNext());
            }

            _parentForCurrentNode = _currentNode;//Запомним родителя для точки, которая будет возвращена

            _currentNode = _currentNode.GetChilds().GetCurrent();

            return _currentNode;
        }

        /// <summary>
        /// Получаем количество поколений в дереве до данного узла, что бы получить верную табуляцию при формировании файла
        /// </summary>
        /// <param name="curr"></param>
        /// <returns></returns>
        private int GetTabsValue(HierarchyNode curr)
        {
            int tabValue = 0;

            do
            {
                curr = curr.Parent;

                if (curr == null) return tabValue;

                tabValue++;
            } while (!curr.GetChilds().CanMooveNext());

            return tabValue;
        }
    }

    /// <summary>
    /// Возвращает кости скелета по порядку обхода
    /// </summary>
    public class HierarchyBoneEnumerator : IEnumerator<KeyValuePair<HierarchyNode, HierarchyNode>>
    {
        private readonly HierarchyNode _root;
        private readonly HierarchyEnumerator _hierarchyEnumerator;
        private HierarchyNode _currentPoint;
        private HierarchyNode _nextPoint;
        
        public HierarchyBoneEnumerator(HierarchyNode root)
        {
            _root = root;
            _hierarchyEnumerator = _root.GetEnumerator() as HierarchyEnumerator;
            _currentPoint = _root;


            if (_hierarchyEnumerator != null)
            {
                _hierarchyEnumerator.MoveNext();
                _currentPoint = _hierarchyEnumerator.Current;//Тут будет root, самый первый элемент иерархии

                if(_hierarchyEnumerator.MoveNext())
                    _nextPoint = _hierarchyEnumerator.Current;//Тут будет второй элемент иерархии
                else
                {
                    throw new Exception("HierarchyBoneEnumerator не приминим к иерархическим структурам с одним элементом");
                }
            }
            else
                throw new Exception("HierarchyBoneEnumerator не инициализирован");
        }

        public KeyValuePair<HierarchyNode, HierarchyNode> Current
        {
            get { return new KeyValuePair<HierarchyNode, HierarchyNode>(_currentPoint, _nextPoint); }
        }

        public void Dispose()
        {
        }

        object System.Collections.IEnumerator.Current
        {
            get { throw new NotImplementedException(); }
        }

        public bool MoveNext()
        {
            return GetNextBone();
        }

        private bool GetNextBone()
        {
            //Если поднятия по дереву не произошло
            //то текущий == следующий, следующий = _hierarchyEnumerator.GetNext()
            //
            //Если следующий - родитель (поднялись по дереву вверх),
            //Текущий == родитель, следующий == _hierarchyEnumerator.GetNext()

            //Признак поднятия - HierarchyEnumerator.IsMoovingUp

            //Если _hierarchyEnumerator.GetNext() == false - также выходим и возвращаем false
            if (!_hierarchyEnumerator.MoveNext())
                return false;

            if (_hierarchyEnumerator.IsMoovingUp)
            {
                //Произошел подьем
                //Текущий == родитель, следующий == _hierarchyEnumerator.GetNext() - от родителя
                _currentPoint = _hierarchyEnumerator.ParentForCurrentNode;
                _nextPoint = _hierarchyEnumerator.Current;
                
            }
            else
            {
                //Нет подьема
                //текущий == следующий, следующий = _hierarchyEnumerator.GetNext()
                _currentPoint = _nextPoint;
                _nextPoint = _hierarchyEnumerator.Current;
            }


            return true;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
