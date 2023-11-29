#nullable enable

using System;
using System.Collections.Generic;

namespace CS576.Janitor.Process
{
    public struct Container<T> where T : class
    {
        private ElementWithNumber<T>[] _elementsWithNumber;

        public Container(int len)
        {
            _elementsWithNumber = new ElementWithNumber<T>[len];
        }

        public void Increment(T element)
        {
            int? elementIndex = FindIndexOf(element);
            if (elementIndex != null)
            {
                int _elementIndex = (int) elementIndex;
                _elementsWithNumber[_elementIndex].number += 1;
                return;
            }

            int? nearestGapIndex = FindNearestGapIndex();
            if (nearestGapIndex != null)
            {
                int _nearestGapIndex = (int) nearestGapIndex;
                _elementsWithNumber[_nearestGapIndex].element = element;
                _elementsWithNumber[_nearestGapIndex].number = 1;
            }
        }

        private int? FindNearestGapIndex()
        {
            for (int i = 0; i < _elementsWithNumber.Length; i++)
            {
                if (_elementsWithNumber[i].element == null)
                {
                    return i;
                }
            }

            return null;
        }

        public void Decrement(T element)
        {
            int? elementIndex = FindIndexOf(element);
            if (elementIndex != null)
            {
                int _elementIndex = (int) elementIndex;
                _elementsWithNumber[_elementIndex].number -= 1;
                if (_elementsWithNumber[_elementIndex].number <= 0)
                {
                    _elementsWithNumber[_elementIndex].element = null;
                    _elementsWithNumber[_elementIndex].number = 0;
                }
            }
        }

        public int? FindIndexOf(T element)
        {
            if (element == null)
                return null;

            for (int i = 0; i < _elementsWithNumber.Length; i++)
            {
                if (_elementsWithNumber[i].element != null && 
                    element.Equals(_elementsWithNumber[i].element))
                {
                    return i;
                }
            }

            return null;
        }

        public T? GetElement(int index)
        {
            return _elementsWithNumber[index].element;
        }

        public int GetNumberOf(T element)
        {
            int? elementIndex = FindIndexOf(element);

            if (elementIndex != null)
            {
                return _elementsWithNumber[(int) elementIndex].number;
            }
            else
            {
                return 0;
            }
        }

        public ElementWithNumber<T>[] GetCopyOfElementsWithNumber()
        {
            ElementWithNumber<T>[] copy = new ElementWithNumber<T>[_elementsWithNumber.Length];

            for (int i = 0; i < _elementsWithNumber.Length; i++)
            {
                ElementWithNumber<T> copiedEn = new ElementWithNumber<T>();
                copiedEn.element = _elementsWithNumber[i].element;
                copiedEn.number = _elementsWithNumber[i].number;
                copy[i] = copiedEn;
            }

            return copy;
        }
    }
}
