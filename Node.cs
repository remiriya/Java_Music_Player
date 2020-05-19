//30011120_cheng liang chen
//programming AT2
//2020/02

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagement
{
    class Node
    {
        private string name;
        private int height; //the height of node
        private Node left, right, parent;

        #region Constructors
        public Node() : this(null) { }
        public Node(string name) : this(name, null, null, null) { }
        public Node(string name, Node left, Node right, Node parent)
        {
            this.name = name;
            this.left = left;
            this.right = right;
            this.parent = parent;
        }


        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public Node Left
        {
            get { return this.left; }
            set { this.left = value; }
        }
        public Node Right
        {
            get { return this.right; }
            set { this.right = value; }
        }

        public Node Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }

        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }
        #endregion
    }
}
