using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ImplementationsLibrary
{
    public interface IHasRectangle
    {
        Rectangle Rectangle { get; }
    }
    public class QuadTree2<T> where T : IHasRectangle
    {
        private QuadTree2<T> this[Quadrant quadrant]
        {
            get
            {
                if (quadrant == Quadrant.Other)
                {
                    return this;
                }
                else
                {
                    return Children[(int)quadrant];
                }
            }
        }

        public Rectangle Bounds { get; private set; }

        public QuadTree2<T>[] Children;
        public List<T> Items { get; private set; }

        public int MaxNumberOfItemsPerQuadrant;

        public bool HasSplit { get; private set; } = false;

        public QuadTree2(Rectangle bounds, int maxNumberOfItemsPerQuadrant)
        {
            Bounds = bounds;
            Children = new QuadTree2<T>[4];
            Items = new List<T>();
            MaxNumberOfItemsPerQuadrant = maxNumberOfItemsPerQuadrant;
        }

        private Quadrant GetQuadrant(T item)
        {
            if (Children[0] == null)
            {
                return Quadrant.Other;
            }

            for (int i = 0; i < Children.Length; i++)
            {
                if (Children[i].Bounds.Contains(item.Rectangle))
                {
                    return (Quadrant)i;
                }
            }
            return Quadrant.Other;
        }

        private void Split()
        {
            Children[0] = new QuadTree2<T>(new Rectangle(Bounds.X + Bounds.Width / 2, Bounds.Y, Bounds.Width / 2, Bounds.Height / 2), MaxNumberOfItemsPerQuadrant);
            Children[1] = new QuadTree2<T>(new Rectangle(Bounds.X, Bounds.Y, Bounds.Width / 2, Bounds.Height / 2), MaxNumberOfItemsPerQuadrant);
            Children[2] = new QuadTree2<T>(new Rectangle(Bounds.X, Bounds.Y + Bounds.Height / 2, Bounds.Width / 2, Bounds.Height / 2), MaxNumberOfItemsPerQuadrant);
            Children[3] = new QuadTree2<T>(new Rectangle(Bounds.X + Bounds.Width / 2, Bounds.Y + Bounds.Height / 2, Bounds.Width / 2, Bounds.Height / 2), MaxNumberOfItemsPerQuadrant);

            HasSplit = true;
        }


        public void AddRange(List<T> items)
        {
            foreach(T item in  items)
            {
                Add(item);
            }
        }
        public void Add(T item)
        {
            Quadrant quadrant = GetQuadrant(item);

            if (quadrant != Quadrant.Other)
            {
                this[quadrant].Add(item);
                return;
            }

            Items.Add(item);

            if (Items.Count >= MaxNumberOfItemsPerQuadrant)
            {
                if (!HasSplit)
                {
                    Split();
                }

                for (int i = 0; i < Items.Count; i++)
                {
                    Quadrant quadrant2 = GetQuadrant(Items[i]);
                    if (quadrant2 != Quadrant.Other)
                    {
                        this[quadrant2].Add(Items[i]);

                        Items.RemoveAt(i);
                        i--;
                    }
                }

            }
        }

        private List<T> RetrieveHelper(T item, List<T> items)
        {
            Quadrant quadrant = GetQuadrant(item);
            if (quadrant != Quadrant.Other)
            {
                this[quadrant].RetrieveHelper(item, items);
            }

            items.AddRange(Items);

            return items;
        }


        public List<T> Retrieve(T item)
        {
            return RetrieveHelper(item, new List<T>());
        }

        //split
        //add
        //getindex
        //retrieve

        public void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(Pens.LightBlue, Bounds);
            if (HasSplit)
            {
                for (int i = 0; i < Children.Length; i++)
                {
                    Children[i].Draw(graphics);
                }
            }
        }
    }
}
