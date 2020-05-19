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
    class BinaryTree
    {
        private Node root;
        public List<string> container = new List<string>();

        public Node Root
        {
            get { return this.root; }
            set { this.root = value; }
        }
        public int getNodeHeight(Node N)
        {
            if (N == null)
            {
                return 0;
            }
            else
            {
                return N.Height;
            }
        }

        public int receiveBiggerNumber(int a, int b)
        {
            //if a>b return a ,else if a<=b return b
            return (a > b) ? a : b;
        }

        private Node minValueNode(Node node)
        {
            Node current = node;

            /* loop down to find the leftmost leaf */
            while (current.Left != null)
            {
                current = current.Left;
            }

            return current;
        }

        private Node rightRotate(Node parent)
        {
            Node leftChild = parent.Left;
            Node rightGrandChild = leftChild.Right;

            // Perform rotation 
            leftChild.Right = parent;
            parent.Left = rightGrandChild;

            // Update heights 
            parent.Height = receiveBiggerNumber(getNodeHeight(parent.Left), getNodeHeight(parent.Right)) + 1;
            leftChild.Height = receiveBiggerNumber(getNodeHeight(leftChild.Left), getNodeHeight(leftChild.Right)) + 1;

            // Return changed root
            return leftChild;
        }

        private Node leftRotate(Node parent)
        {
            Node rightChild = parent.Right;
            Node leftGrandChild = rightChild.Left;

            // rotate tree 
            rightChild.Left = parent;
            parent.Right = leftGrandChild;

            //  Update heights 
            parent.Height = receiveBiggerNumber(getNodeHeight(parent.Left), getNodeHeight(parent.Right)) + 1;
            rightChild.Height = receiveBiggerNumber(getNodeHeight(rightChild.Left), getNodeHeight(rightChild.Right)) + 1;

            // Return changed root
            return rightChild;
        }

        // Get Balance factor of node N 
        private int getBalanceFactor(Node N)
        {
            if (N == null)
            {
                return 0;
            }

            return getNodeHeight(N.Left) - getNodeHeight(N.Right);
        }

        private Node adjustToBalance(Node recursiveRoot, int balanceFactor)
        {
            // If node is unbalanced 
            // Left Left Case  
            if (balanceFactor > 1 && getBalanceFactor(recursiveRoot.Left) >= 0)
            {
                //return changed root
                return rightRotate(recursiveRoot);
            } // Left Right Case  
            else if (balanceFactor > 1 && getBalanceFactor(recursiveRoot.Left) < 0)
            {
                recursiveRoot.Left = leftRotate(recursiveRoot.Left);
                return rightRotate(recursiveRoot);
            } // Right Right Case  
            else if (balanceFactor < -1 && getBalanceFactor(recursiveRoot.Right) <= 0)
            {
                return leftRotate(recursiveRoot);
            } // Right Left Case  
            else if (balanceFactor < -1 && getBalanceFactor(recursiveRoot.Right) > 0)
            {
                recursiveRoot.Right = rightRotate(recursiveRoot.Right);
                return leftRotate(recursiveRoot);
            }

            //return unchanged root
            return recursiveRoot;
        }

        private void ClearContainer()
        {
            this.container.Clear();
        }
        private void StoreTreeToList(Node startFromRoot)
        {
            if (startFromRoot != null)
            {
                //print left children first
                
                StoreTreeToList(startFromRoot.Left);
                container.Add(startFromRoot.Name);
                StoreTreeToList(startFromRoot.Right);
            }
            else
            {
                return;
            }
        }

        public List<string> ReceiveTreeElements()
        {
            ClearContainer();
            StoreTreeToList(this.root);
            return this.container;
        }

        public Node insert(Node recursiveRoot, String target)
        {

            // if node is null create a new node
            if (recursiveRoot == null)
            {
                return (new Node(target));
            }

            // use recursion find right position
            if (recursiveRoot.Name.CompareTo(target) > 0)
            {
                recursiveRoot.Left = insert(recursiveRoot.Left, target);
            }
            else if (recursiveRoot.Name.CompareTo(target) < 0)
            {
                recursiveRoot.Right = insert(recursiveRoot.Right, target);

                //binary tree is not allow same value return function
            }
            else
            {
                return recursiveRoot;
            }

            //update inserted node height, 1+ subtree height
            recursiveRoot.Height = 1 + receiveBiggerNumber(getNodeHeight(recursiveRoot.Left),
                    getNodeHeight(recursiveRoot.Right));

            //get balancefactor of node
            int balanceFactor = getBalanceFactor(recursiveRoot);

            recursiveRoot = adjustToBalance(recursiveRoot, balanceFactor);

            return recursiveRoot;
        }

        public Node deleteNode(Node recursiveRoot, String target)
        {
            //if root is null return function
            if (recursiveRoot == null)
            {
                return recursiveRoot;
            }

            // If target value smaller than node, find node in left subtree
            if (recursiveRoot.Name.CompareTo(target) > 0)
            {
                recursiveRoot.Left = deleteNode(recursiveRoot.Left, target);
            } // If target value greater than node, find node in right subtree
            else if (recursiveRoot.Name.CompareTo(target) < 0)
            {
                recursiveRoot.Right = deleteNode(recursiveRoot.Right, target);
            } // find target, delete node  
            else
            {

                //  node with no child  
                if ((recursiveRoot.Left == null) && (recursiveRoot.Right == null))
                {
                    recursiveRoot = null;
                    //one right child
                }
                else if (recursiveRoot.Left == null)
                {
                    recursiveRoot = recursiveRoot.Right;
                    //one left child
                }
                else if (recursiveRoot.Right == null)
                {
                    recursiveRoot = recursiveRoot.Left;
                    //two child
                }
                else
                {
                    //get smallest child in right subtree
                    Node temp = minValueNode(recursiveRoot.Right);

                    // Copy the inorder successor's data to this node  
                    recursiveRoot.Name = temp.Name;

                    // Delete the inorder successor  
                    recursiveRoot.Right = deleteNode(recursiveRoot.Right, temp.Name);
                }
            }

            // If the tree had only one node then return  
            if (recursiveRoot == null)
            {
                return recursiveRoot;
            }

            //update height
            recursiveRoot.Height = receiveBiggerNumber(getNodeHeight(recursiveRoot.Left), getNodeHeight(recursiveRoot.Right)) + 1;

            // check balance factor  
            int balanceFactor = getBalanceFactor(recursiveRoot);

            recursiveRoot = adjustToBalance(recursiveRoot, balanceFactor);

            return recursiveRoot;
        }

        public Node containData(Node recursiveRoot, String targetString)
        {

            if (recursiveRoot == null)
            {
                return recursiveRoot;
            }

            if (recursiveRoot.Name.CompareTo(targetString) == 0)
            {
                //return target node
                return recursiveRoot;
            }
            else if (recursiveRoot.Name.CompareTo(targetString) < 0)
            {
                //use recursion to find target in subtree
                return containData(recursiveRoot.Right, targetString);
            }
            else
            {
                return containData(recursiveRoot.Left, targetString);
            }

        }
    }
}
