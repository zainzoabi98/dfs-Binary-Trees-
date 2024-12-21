using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp19
{
    //Binary Trees // dfs
    // הגדרת מחלקת הצומת בעץ בינארי
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
    public class Solution
    {
        // פונקציה לחישוב עומק מקסימלי של עץ בינארי
        public int MaxDepth(TreeNode root)
        {
            // אם הגענו לצומת ריק (null), נחזיר 0
            if (root == null)
            {
                return 0;
            }

            // חישוב העומק של תתי-העצים השמאליים והימניים
            int leftDepth = MaxDepth(root.left);
            int rightDepth = MaxDepth(root.right);

            // מחזירים את העומק המקסימלי +1 (כולל את הצומת הנוכחי)
            return Math.Max(leftDepth, rightDepth) + 1;
        }
    }
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ///נתון לנו מספר מבצעים חיפוש בעץ 
    //כך נבדוק אם יש מסלול שסוכנים אותו שווה למספר הנתון
    public class Solution1
    {
        // פונקציה רקורסיבית לחיפוש דרך עם סכום מתאים
        public bool HasPathSum(TreeNode root, int target)
        {
            // אם הצומת ריק, אין דרך
            if (root == null)
            {
                return false;
            }

            // אם הגענו לעלה, בודקים אם הסכום נשאר 0
            if (root.left == null && root.right == null)
            {
                return root.val == target;
            }

            // חישוב הסכום החדש לאחר חיסור הערך הנוכחי
            int newTarget = target - root.val;

            // קריאה רקורסיבית לצמתים השמאליים והימניים
            return HasPathSum(root.left, newTarget) || HasPathSum(root.right, newTarget);
        }
    }


    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ///צריך לבצע חיפו בעץ ולהחזיר את המסלול בעל הסכום הגדול
    public class Solution2
    {
        // פונקציה ראשית שמבצעת את DFS
        public int GoodNodes(TreeNode root)
        {
            return DFS(root, root.val); // מתחילים את DFS עם הערך של השורש
        }

        // פונקציית עזר שעושה DFS
        private int DFS(TreeNode node, int maxVal)
        {
            if (node == null)
            {
                return 0;
            }

            // אם הצומת הנוכחי הוא "Good Node", אז נעדכן את הסכום
            int result = 0;
            if (node.val >= maxVal)
            {
                result = 1; // הצומת הזה הוא "Good Node"
            }

            // מעדכנים את הערך המרבי למסלול עד עכשיו
            maxVal = Math.Max(maxVal, node.val);

            // מבצעים קריאה רקורסיבית לשני הצמתים השמאליים והימניים
            result += DFS(node.left, maxVal);
            result += DFS(node.right, maxVal);

            return result;
        }
    }

    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////    
    //////צומת שמול קטן מהצומת הנוכחי
    //תומת שמול גדול מהצמות הנוכחי

    public class Solution3
    {
        // פונקציה ראשית שמבצעת את ה-DFS
        public bool IsValidBST(TreeNode root)
        {
            return DFS(root, long.MinValue, long.MaxValue);
        }

        // פונקציית עזר שמבצעת DFS ומבצע את הבדיקות
        private bool DFS(TreeNode node, long minVal, long maxVal)
        {
            if (node == null) return true;

            // אם הערך הנוכחי לא נמצא בטווח המותר, העץ לא תקין
            if (node.val <= minVal || node.val >= maxVal)
            {
                return false;
            }

            // מבצעים קריאה רקורסיבית על הצמתים השמאליים והימניים, ומעדכנים את הגבולות
            return DFS(node.left, minVal, node.val) && DFS(node.right, node.val, maxVal);
        }
    }


    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ///חישוב סכול של כול תת צומת בעץ

    public class Solution4
    {
        private int totalTilt = 0;

        // פונקציה שמבצעת את חישוב הטילט עבור כל הצמתים בעץ
        public int FindTilt(TreeNode root)
        {
            CalculateSum(root);
            return totalTilt;
        }

        // פונקציה שמבצעת DFS ומחזירה את סכום תתי-העץ של כל צומת
        private int CalculateSum(TreeNode node)
        {
            if (node == null) return 0;

            // חישוב סכום תתי-העץ השמאלי והימני
            int leftSum = CalculateSum(node.left);
            int rightSum = CalculateSum(node.right);

            // חישוב הטילט של הצומת הנוכחי
            int tilt = Math.Abs(leftSum - rightSum);
            totalTilt += tilt;

            // מחזירים את סכום תת-העץ הכולל של הצומת (כולל הצומת עצמו)
            return node.val + leftSum + rightSum;
        }
    }

    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    //     החזרת הגובה של תת-העץ
    //נבצע חישוב רקורסיבי של הגובה עבור כל תת-עץ של כל צומת
    public class Solution5
    {
        private int diameter = 0; // משתנה לשמירת הדימטר המקסימלי

        // פונקציה שמבצעת חישוב דימטר של העץ
        public int DiameterOfBinaryTree(TreeNode root)
        {
            CalculateHeight(root);  // התחלת החישוב
            return diameter;
        }

        // פונקציה שמבצעת חישוב DFS ומחזירה את הגובה של תת-העץ
        private int CalculateHeight(TreeNode node)
        {
            if (node == null) return 0; // אם הצומת ריק, גובהו 0

            // חישוב הגובה של תתי-העץ השמאלי והימני
            int leftHeight = CalculateHeight(node.left);
            int rightHeight = CalculateHeight(node.right);

            // חישוב הדימטר: סכום הגבהים של תתי-העץ השמאלי והימני
            diameter = Math.Max(diameter, leftHeight + rightHeight);

            // מחזירים את הגובה של הצומת הנוכחי
            return 1 + Math.Max(leftHeight, rightHeight);
        }
    }



    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ///חיפוש בתוך העץ
    //וצריך לבדוק כמה מסלולים של צומת ועלים ששוים לערך נתון
    public class Solution6
    {
        // פונקציה שתמצא את כל המסלולים שהסכום שלהם שווה ל- target
        public IList<IList<int>> PathSum(TreeNode root, int target)
        {
            List<IList<int>> result = new List<IList<int>>();  // רשימת התוצאות
            List<int> path = new List<int>();  // מסלול נוכחי
            FindPaths(root, target, path, result);  // קריאה לפונקציה רקורסיבית
            return result;
        }

        // פונקציה רקורסיבית לחיפוש המסלולים
        private void FindPaths(TreeNode node, int target, List<int> path, List<IList<int>> result)
        {
            if (node == null) return;  // אם הצומת ריק, נעצור את החיפוש

            path.Add(node.val);  // הוספת הערך הנוכחי למסלול

            // אם הגענו לצומת עלה, נבדוק אם הסכום שווה למטרה
            if (node.left == null && node.right == null && target == node.val)
            {
                result.Add(new List<int>(path));  // אם הסכום שווה, נשמור את המסלול בתוצאות
            }
            else
            {
                // חיפוש רקורסיבי בתת-העץ השמאלי והימני
                FindPaths(node.left, target - node.val, path, result);  // חיפוש בתת-העץ השמאלי
                FindPaths(node.right, target - node.val, path, result);  // חיפוש בתת-העץ הימני


            }

            path.RemoveAt(path.Count - 1);  // חזרה אחורה (backtrack) והסרת הצומת הנוכחי מהמסלול
        }
    }
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    //חיפוש בעץ את המסלול הכי רחוק 

    public class Solution7
    {
        private int maxLength = 0;

        public int LongestUnivaluePath(TreeNode root)
        {
            // נבצע חיפוש DFS מהשורש
            DFS(root);
            return maxLength;
        }

        private int DFS(TreeNode node)
        {
            // אם הצומת ריק, נחזור 0
            if (node == null) return 0;

            // חישוב נתיב בצד השמאלי והימני
            int leftPath = DFS(node.left);
            int rightPath = DFS(node.right);

            // אם הצומת השמאלי מתאים לערך של הצומת הנוכחי
            if (node.left != null && node.left.val == node.val)
            {
                leftPath++;
            }
            else
            {
                leftPath = 0; // אם לא מתאים, לא נוכל להוסיף את הצומת הזה
            }

            // אם הצומת הימני מתאים לערך של הצומת הנוכחי
            if (node.right != null && node.right.val == node.val)
            {
                rightPath++;
            }
            else
            {
                rightPath = 0; // אם לא מתאים, לא נוכל להוסיף את הצומת הזה
            }

            // עדכון אורך הנתיב המרבי
            maxLength = Math.Max(maxLength, leftPath + rightPath);

            // מחזירים את אורך הנתיב הארוך ביותר מצומת זה (כולל רק את אחד הצדדים)
            return Math.Max(leftPath, rightPath);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            //חישוב עומק העץ DFS
            //העומק הכי רחוק
            // יצירת עץ לדוגמה:
            TreeNode root = new TreeNode(4);
            root.left = new TreeNode(2);
            root.left.left = new TreeNode(1);
            root.right = new TreeNode(7);
            root.right.left = new TreeNode(6);
            root.right.right = new TreeNode(9);
            root.right.right.left = new TreeNode(8);

            Solution solution = new Solution();
            int depth = solution.MaxDepth(root);

            Console.WriteLine("Maximum depth of the binary tree: " + depth); // Output: 4


            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            ///נתון לנו מספר מבצעים חיפוש בעץ 
            //כך נבדוק אם יש מסלול שסוכנים אותו שווה למספר הנתון


            // יצירת עץ לדוגמה:
            TreeNode root1 = new TreeNode(4);
            root1.left = new TreeNode(2);
            root1.left.left = new TreeNode(1);
            root1.left.right = new TreeNode(3);
            root1.right = new TreeNode(7);
            root1.right.left = new TreeNode(6);
            root1.right.right = new TreeNode(9);

            Solution1 solution1 = new Solution1();

            int target1 = 17;
            Console.WriteLine("Has path sum (17): " + solution1.HasPathSum(root1, target1)); // Output: true

            int target2 = 13;
            Console.WriteLine("Has path sum (13): " + solution1.HasPathSum(root1, target2)); // Output: false







            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            ///צריך לבצע חיפו בעץ ולהחזיר את המסלול בעל הסכום הגדול

            // יצירת עץ לדוגמה:
            TreeNode root2 = new TreeNode(4);
            root2.left = new TreeNode(2);
            root2.left.left = new TreeNode(1);
            root2.left.right = new TreeNode(3);
            root2.right = new TreeNode(7);
            root2.right.left = new TreeNode(6);
            root2.right.right = new TreeNode(9);

            Solution2 solution2 = new Solution2();

            int result = solution2.GoodNodes(root2);
            Console.WriteLine("Number of good nodes: " + result); // Output: 3


            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            ///צומת שמול קטן מהצומת הנוכחי
            //תומת שמול גדול מהצמות הנוכחי


            // יצירת עץ לדוגמה:
            TreeNode root3 = new TreeNode(2);
            root3.left = new TreeNode(1);
            root3.right = new TreeNode(4);

            Solution3 solution3 = new Solution3();

            bool result3 = solution3.IsValidBST(root);
            Console.WriteLine("Is the tree a valid BST? " + result3); // Output: true







            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            ///חישוב סכול של כול תת צומת בעץ

            // יצירת עץ לדוגמה:
            TreeNode root4 = new TreeNode(4);
            root4.left = new TreeNode(2);
            root4.right = new TreeNode(7);
            root4.left.left = new TreeNode(1);
            root4.left.right = new TreeNode(3);
            root4.right.left = new TreeNode(6);
            root4.right.right = new TreeNode(9);

            Solution4 solution4 = new Solution4();

            int result4 = solution4.FindTilt(root4);
            Console.WriteLine("The sum of tilts in the tree is: " + result4); // Output: 21










            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            //     החזרת הגובה של תת-העץ
            //נבצע חישוב רקורסיבי של הגובה עבור כל תת-עץ של כל צומת

            TreeNode root5 = new TreeNode(3);
            root5.left = new TreeNode(9);
            root5.right = new TreeNode(2);
            root5.left.left = new TreeNode(1);
            root5.left.right = new TreeNode(4);
            root5.left.right.left = new TreeNode(5);

            Solution5 solution5 = new Solution5();
            int result5 = solution5.DiameterOfBinaryTree(root5);

            Console.WriteLine("The diameter of the tree is: " + result5); // Output: 4





            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            ///חיפוש בתוך העץ
            //וצריך לבדוק כמה מסלולים של צומת ועלים ששוים לערך נתון
            TreeNode root6 = new TreeNode(1);
            root6.left = new TreeNode(2);
            root6.right = new TreeNode(4);
            root6.left.left = new TreeNode(4);
            root6.left.right = new TreeNode(7);
            root6.right.left = new TreeNode(5);
            root6.right.right = new TreeNode(1);

            int target6 = 10;

            Solution6 solution6 = new Solution6();
            IList<IList<int>> result6 = solution6.PathSum(root6, target6);

            // הדפסת התוצאות
            foreach (var path in result6)
            {
                Console.WriteLine(string.Join(" -> ", path));
            }



            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            //חיפוש בעץ את המסלול הכי רחוק 
            // יצירת עץ לדוגמה
            TreeNode root7 = new TreeNode(5);
            root7.left = new TreeNode(4);
            root7.right = new TreeNode(5);
            root7.left.left = new TreeNode(4);
            root7.left.right = new TreeNode(4);
            root7.right.right = new TreeNode(5);

            // יצירת מופע של מחלקת Solution
            Solution7 solution7 = new Solution7();

            // קריאה לפונקציה LongestUnivaluePath
            int result7 = solution7.LongestUnivaluePath(root7);

            // הדפסת התוצאה
            Console.WriteLine("Longest Univalue Path: " + result7);// 5 --> 4 --> 4
                                                                   // 5 --> 4 --> 4 

            /*
      5
    /   \
    4    5
   / \    \
  4   4    5

    */





        }



    }
}
